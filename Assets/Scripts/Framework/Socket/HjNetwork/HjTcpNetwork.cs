//#define LOG_SEND_BYTES
//#define LOG_RECEIVE_BYTES
using System;
using System.Net.Sockets;
using CustomDataStruct;
using System.Threading;
using System.Collections.Generic;
using XLua;
using System.Text;
using System.Net;
using UnityEngine;
using XMainClient;
using XUtliPoolLib;

namespace Networks
{
    [Hotfix]
    public class HjTcpNetwork : HjNetworkBase
    {
        private Thread mSendThread = null;
        private volatile bool mSendWork = false;
        private HjSemaphore mSendSemaphore = null;

        protected IMessageQueue mSendMsgQueue = null;

        #region 李满堂加
        private CNetSender mSender = null;
        private INetProcess m_oProcess;
        private IPacketBreaker m_oBreaker = null;
        private static SmallBufferPool<byte> m_BufferPool = null;

        private static BlockInfo[] blockInit = new BlockInfo[10]
        {
            new BlockInfo(32, 128),
            new BlockInfo(64, 128),
            new BlockInfo(128, 64),
            new BlockInfo(256, 32),
            new BlockInfo(512, 16),
            new BlockInfo(1024, 4),
            new BlockInfo(2048, 4),
            new BlockInfo(4096, 4),
            new BlockInfo(8192, 4),
            new BlockInfo(65536, 4)
        };


        private Queue<NetEvent> m_oDataQueue;

        private int m_iMaxCountPerFrame = 50;
        #endregion

        public HjTcpNetwork(int maxBytesOnceSent = 1024 * 100, int maxReceiveBuffer = 1024 * 512) : base(maxBytesOnceSent, maxReceiveBuffer)
        {
            mSendSemaphore = new HjSemaphore();
            mSendMsgQueue = new MessageQueue();

            #region 李满堂加
            mSender = new CNetSender(this);
            m_oProcess = new CNetProcessor(this);
            m_oBreaker = new CPacketBreaker();
            if (m_BufferPool == null)
            {
                m_BufferPool = new SmallBufferPool<byte>();
                m_BufferPool.Init(blockInit, 1);
            }
            m_oDataQueue = new Queue<NetEvent>();
            #endregion
        }

        public override void Dispose()
        {
            mSendMsgQueue.Dispose();
            base.Dispose();
        }

        protected override void DoConnect()
        {
            AddressFamily newAddressFamily = AddressFamily.InterNetwork;
            IPv6SupportMidleware.getIPType(mIp, mPort.ToString(), out newAddressFamily);

            mClientSocket = new Socket(newAddressFamily, SocketType.Stream, ProtocolType.Tcp);
            mClientSocket.BeginConnect(mIp, mPort, (IAsyncResult ia) =>
            {
                try
                {
                    mClientSocket.EndConnect(ia);
                    OnConnected();
                }
                catch (Exception e)
                {
                    ReportSocketClosed(ESocketError.ERROR_2, e.Message);
                }

            }, null);
            mStatus = SOCKSTAT.CONNECTING;
        }

        protected override void DoClose()
        {
            // 关闭socket，Tcp下要等待现有数据发送、接受完成
            // https://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.shutdown(v=vs.90).aspx
            // mClientSocket.Shutdown(SocketShutdown.Both);
            base.DoClose();
        }

        public override void StartAllThread()
        {
            base.StartAllThread();

            if (mSendThread == null)
            {
                mSendThread = new Thread(SendThread);
                mSendWork = true;
                mSendThread.Start(null);
            }
        }

        public override void StopAllThread()
        {
            base.StopAllThread();
            //先把队列清掉
            mSendMsgQueue.Dispose();

            if (mSendThread != null)
            {
                mSendWork = false;
                mSendSemaphore.ProduceResrouce();// 唤醒线程
                mSendThread.Join();// 等待子线程退出
                mSendThread = null;
            }
        }

        /// <summary>
        /// 协议加密
        /// </summary>
     //   byte[] keyBytes = Encoding.UTF8.GetBytes("Glv8PzQQ4fa8P2017");

        private void SendThread(object o)
        {
            List<byte[]> workList = new List<byte[]>(10);

            while (mSendWork)
            {
                if (!mSendWork)
                {
                    break;
                }

                if (mClientSocket == null || !mClientSocket.Connected)
                {
                    continue;
                }

                mSendSemaphore.WaitResource();
                if (mSendMsgQueue.Empty())
                {
                    continue;
                }

                mSendMsgQueue.MoveTo(workList);
                try
                {
                    for (int k = 0; k < workList.Count; ++k)
                    {
                        var msgObj = workList[k];
                        if (mSendWork)
                        {
                            mClientSocket.Send(msgObj, msgObj.Length, SocketFlags.None);
                        }
                    }
                }
                catch (ObjectDisposedException e)
                {
                    ReportSocketClosed(ESocketError.ERROR_1, e.Message);
                    break;
                }
                catch (Exception e)
                {
                    ReportSocketClosed(ESocketError.ERROR_2, e.Message);
                    break;
                }
                finally
                {
                    for (int k = 0; k < workList.Count; ++k)
                    {
                        var msgObj = workList[k];
                        StreamBufferPool.RecycleBuffer(msgObj);
                    }
                    workList.Clear();
                }
            }

            if (mStatus == SOCKSTAT.CONNECTED)
            {
                mStatus = SOCKSTAT.CLOSED;
            }
        }

        /// <summary>
        /// /==========================================================李满堂加==========
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="size"></param>
        public static void GetBuffer(ref SmallBuffer<byte> sb, int size)
        {
            lock (m_BufferPool)
            {
                m_BufferPool.GetBlock(ref sb, size);
            }
        }
        protected override void DetectPacket(byte[] m_oRecvBuff, ref int m_nCurrRecvLen)
        {
            int num = 0;
            while (m_nCurrRecvLen > 0)
            {
                int num2 = m_oBreaker.BreakPacket(m_oRecvBuff, num, m_nCurrRecvLen);
                if (num2 == 0)
                {
                    break;
                }

                SmallBuffer<byte> sb = default(SmallBuffer<byte>);
                GetBuffer(ref sb, num2);
                sb.Copy(m_oRecvBuff, num, 0, num2);
                ///////
                PushReceiveEvent(ref sb, num2);

                num += num2;
                m_nCurrRecvLen -= num2;
            }
            if (num > 0 && m_nCurrRecvLen > 0)
            {
                Array.Copy(m_oRecvBuff, num, m_oRecvBuff, 0, m_nCurrRecvLen);
            }
        }
        public void PushReceiveEvent(ref SmallBuffer<byte> oData, int length)
        {
            NetEvent evt = XNetEventPool.GetEvent();
            evt.m_nEvtType = NetEvtType.Event_Receive;
            evt.m_oBuffer = oData;
            evt.m_nBufferLength = length;
            m_oProcess.OnPrePropress(evt);
            EnQueue(evt, false);
        }

        public void EnQueue(NetEvent evt, bool propress)
        {
            if (evt == null)
            {
                XSingleton<XDebug>.singleton.AddErrorLog("null event EnQueue");
            }
            else if (propress)
            {
                //Monitor.Enter(m_oPreProcessQueue);
                //m_oPreProcessQueue.Enqueue(evt);
                //Monitor.Exit(m_oPreProcessQueue);
            }
            else
            {
                Monitor.Enter(m_oDataQueue);
                m_oDataQueue.Enqueue(evt);
                Monitor.Exit(m_oDataQueue);
            }
        }
        private NetEvent DeQueue()
        {
            NetEvent result = null;
            Monitor.Enter(m_oDataQueue);
            if (m_oDataQueue.Count > 0)
            {
                result = m_oDataQueue.Dequeue();
            }
            Monitor.Exit(m_oDataQueue);
            return result;
        }
        public int ProcessMsg()
        {
            if (m_oProcess == null)
            {
                return 0;
            }
            int num = 0;
            for (NetEvent netEvent = DeQueue(); netEvent != null; netEvent = DeQueue())
            {
                switch (netEvent.m_nEvtType)
                {
                    case NetEvtType.Event_Connect:
                        m_oProcess.OnConnect(netEvent.m_bSuccess);
                        break;
                    case NetEvtType.Event_Closed:
                        m_oProcess.OnClosed(netEvent.m_nErrCode);
                        XSingleton<XDebug>.singleton.AddGreenLog("close socket ", netEvent.m_SocketID.ToString(), " event is processed");
                        break;
                    case NetEvtType.Event_Receive:

                        Debug.Log("C#中向Lua派发前，  msgid = " + netEvent.msgType + " tagID =  " + netEvent.tagID + "  isPtc=" + netEvent.IsPtc);
                        this.ReceivePkgHandle?.Invoke(netEvent.msgType, netEvent.tagID, netEvent.pbData, netEvent.IsPtc);
                        break;
                    default:
                        XSingleton<XDebug>.singleton.AddErrorLog("null event");
                        break;
                }
                CClientSocket.ReturnBuffer(ref netEvent.m_oBuffer);
                XNetEventPool.Recycle(netEvent);
                num++;
                if (num >= m_iMaxCountPerFrame)
                {
                    break;
                }
            }
            return num;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tagID">当isPtc为false时，即RPC时，tagid传不传都行。因为不往包里写</param>
        /// <param name="msgObj"></param>
        /// <param name="isPtc"></param>
        public override void SendMessage(int id, uint tagID, byte[] msgObj, bool isPtc)
        {
            Debug.Log("msgObj len=" + msgObj.Length);
            if (isPtc)
            {
                mSender.Send((uint)id, false, 0, msgObj);
            }
            else
            {
                mSender.Send((uint)id, true, tagID, msgObj);
            }


        }

        /// <summary>
        /// LMT
        /// </summary>
        /// <param name="arr"></param>
        public void Send(byte[] arr)
        {
            Debug.Log("实际发送长度=" + arr.Length);
            mSendMsgQueue.Add(arr);
            mSendSemaphore.ProduceResrouce();

        }

        /// <summary>
        /// 加密发送的消息
        /// </summary>
        /// <param name="buff">发送消息字节数组</param>
        /// <param name="mID">消息id</param>
        /// <param name="keyBytes">加密的key字节数组</param>
        /// <returns></returns>
        public byte[] EncryptionSocket(byte[] buff, int mID, byte[] keyBytes)
        {
            int num = 4;
            byte[] bytes = BitConverter.GetBytes(IPAddress.NetworkToHostOrder(mID));
            byte[] src = null;
            if (buff == null)
            {
                src = new byte[0];
            }
            else
            {
                byte[] buffer3 = new byte[keyBytes.Length + buff.Length];
                Buffer.BlockCopy(keyBytes, 0, buffer3, 0, keyBytes.Length);
                Buffer.BlockCopy(buff, 0, buffer3, keyBytes.Length, buff.Length);
                for (int i = 0; i < buffer3.Length; i += 5)
                {
                    if ((i + 3) > (buffer3.Length - 1))
                    {
                        break;
                    }
                    byte num3 = (byte)~buffer3[i + 2];
                    buffer3[i + 2] = buffer3[i + 3];
                    buffer3[i + 3] = num3;
                }
                src = buffer3;
            }
            int n = bytes.Length + src.Length;
            byte[] dst = new byte[num + n];
            Buffer.BlockCopy(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(n)), 0, dst, 0, 4);
            Buffer.BlockCopy(bytes, 0, dst, 4, 4);
            Buffer.BlockCopy(src, 0, dst, 8, src.Length);
            UnityEngine.Debug.Log("发送消息编号id=：" + mID + "；发送字节长度=：" + dst.Length);

            return dst;
        }

        public override void UpdateNetwork()
        {
            base.UpdateNetwork();
            ProcessMsg();
        }
    }

#if UNITY_EDITOR
    public static class HjTcpNetworkExporter
    {
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp = new List<Type>()
        {
            typeof(HjTcpNetwork),
            typeof(HjNetworkBase),

        };

        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>()
        {
            typeof(Action<object, int, string>),
            typeof(Action<int,byte[]>),
             typeof(Action<uint,uint,byte[],bool>)
        };
    }
#endif
}