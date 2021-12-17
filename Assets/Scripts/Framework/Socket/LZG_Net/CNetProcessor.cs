using Ionic.Zlib;
using Networks;
using System;
using System.IO;
using XUtliPoolLib;

namespace XMainClient
{
    public class CNetProcessor : INetProcess
    {
        private CNetwork m_oNetwork;

        private INetObserver m_oObserver;

        private System.Random r;

        public static int MaxBuffSize = 65536;

        private MemoryStream RecvStream = new MemoryStream(65536);

        private ProtocolHead head = new ProtocolHead();

        private ZlibStream zDecompress = null;

        private static NetEvent m_sCurrentEvent = null;

        private HjTcpNetwork tcpNetwork; //LMT


        public void OnLuaProcess(NetEvent evt)
        {
            LuaNetNode node = evt.node;
            if (node == null)
            {
                return;
            }
            if (node.isOnlyLua)
            {
                //XSingleton<XUpdater.XUpdater>.singleton.XLuaEngine.hotfixMgr.ProcessOveride(node.type, node.buffer, node.length);
            }
            else if (node.isRpc)
            {
                if (node.resp != null)
                {
                    node.resp(node.buffer, node.length);
                }
            }
            else
            {
                //XSingleton<XUpdater.XUpdater>.singleton.XLuaEngine.hotfixMgr.RegistedPtc(node.type, node.buffer, node.length);
            }
            m_oNetwork.ReturnNode(node);
        }

        public CNetProcessor(CNetwork network, INetObserver ob)
        {
            m_oNetwork = network;
            m_oObserver = ob;
            r = new System.Random(DateTime.Now.Millisecond);
            zDecompress = new ZlibStream(RecvStream, CompressionMode.Decompress, true);
        }

        /// <summary>
        /// LMT
        /// </summary>
        /// <param name="network"></param>
        /// <param name="ob"></param>
        public CNetProcessor(HjTcpNetwork network)
        {
            tcpNetwork = network;

            r = new System.Random(DateTime.Now.Millisecond);
            zDecompress = new ZlibStream(RecvStream, CompressionMode.Decompress, true);
        }


        public void OnConnect(bool bSuccess)
        {
            if (m_oObserver != null)
            {
                m_oObserver.OnConnect(bSuccess);
            }
        }

        public void OnClosed(NetErrCode nErrCode)
        {
            if (m_oObserver != null)
            {
                m_oObserver.OnClosed(nErrCode);
            }
        }

        public void OnPrePropress(NetEvent evt)
        {
            if ((long)evt.m_oBuffer.Count < 12L)
            {
                XSingleton<XDebug>.singleton.AddErrorLog("head file size error");
                return;
            }
            head.Reset();
            head.Deserialize(ref evt.m_oBuffer);
            int size = head.Size;  ///包头长度
            if (evt.m_oBuffer.IsInit)
            {
                if (head.IsCompressed)
                {
                    zDecompress.Seek(0L, SeekOrigin.Begin);
                    zDecompress.SetLength(0L);
                    zDecompress.Write(evt.m_oBuffer.OriginalBuff, evt.m_oBuffer.StartOffset + size, evt.m_nBufferLength - size);
                    zDecompress.Flush();

                    RecvStream.Seek(0L, SeekOrigin.Begin);
                }
                else
                {
                    RecvStream.Seek(0L, SeekOrigin.Begin);
                    RecvStream.SetLength(0L);
                    RecvStream.Write(evt.m_oBuffer.OriginalBuff, evt.m_oBuffer.StartOffset + size, evt.m_nBufferLength - size);
                    RecvStream.Seek(0L, SeekOrigin.Begin);
                }
            }

            evt.IsPtc = head.IsPtc;
            evt.pbData = RecvStream.ToArray();
            evt.msgType = head.type;
            evt.tagID = head.tagID;

        }

        public void OnProcess(NetEvent evt)
        {
           
        }

        public void OnPostProcess(NetEvent evt)
        {
    
        }

        public static void ManualReturnProtocol()
        {
            if (m_sCurrentEvent != null)
            {
                m_sCurrentEvent.ManualReturnProtocol();
            }
        }
    }
}
