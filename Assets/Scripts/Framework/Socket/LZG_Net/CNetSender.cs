using Networks;
using System;
using System.IO;
using XUtliPoolLib;

namespace XMainClient
{
    public class CNetSender : INetSender, ILuaNetSender
    {
        private CNetwork m_oNetwork;
        private HjTcpNetwork tcpNetwork; //LMT
        private MemoryStream m_oSendStream;

        public CNetSender(CNetwork network)
        {
            m_oNetwork = network;
            m_oSendStream = new MemoryStream();
        }

        /// <summary>
        /// LMT
        /// </summary>
        /// <param name="network"></param>
        public CNetSender(HjTcpNetwork network)
        {
            tcpNetwork = network;
            m_oSendStream = new MemoryStream();
        }


        public bool Send(Protocol protocol)
        {
            m_oSendStream.SetLength(0L);
            m_oSendStream.Position = 0L;
            protocol.SerializeWithHead(m_oSendStream);
            if (XSingleton<XDebug>.singleton.EnableRecord())
            {
                XSingleton<XDebug>.singleton.AddPoint(protocol.GetProtoType(), protocol.GetType().Name, m_oSendStream.Length, 1, XDebug.RecordChannel.ENetwork);
            }
            if (m_oSendStream.Length > 1024)
            {
                XSingleton<XDebug>.singleton.AddWarningLog2("Send Ptc:{0} to long:{1}b", protocol.GetProtoType(), m_oSendStream.Length);
            }
            return m_oNetwork.Send(m_oSendStream.GetBuffer(), 0, (int)m_oSendStream.Length);
        }

        public bool Send(uint _type, bool isRpc, uint tagID, byte[] _reqBuff)
        {
            m_oSendStream.SetLength(0L);
            m_oSendStream.Position = 0L;
            long num = 0L;

            ProtocolHead sharedHead = ProtocolHead.SharedHead;
            sharedHead.Reset();
            sharedHead.tagID = tagID;
            sharedHead.type = _type;
            sharedHead.flag = (isRpc ? 3u : 0u);
            sharedHead.Serialize(m_oSendStream);

            m_oSendStream.Write(_reqBuff, 0, _reqBuff.Length);
            long position = m_oSendStream.Position;
            uint value = (uint)(position - num - 4);
            m_oSendStream.Position = num;
            m_oSendStream.Write(BitConverter.GetBytes(value), 0, 4);
            m_oSendStream.Position = position;
            byte[] buffer = m_oSendStream.GetBuffer();
            int num2 = (int)m_oSendStream.Length;
            //	return m_oNetwork.Send(m_oSendStream.GetBuffer(), 0, (int)m_oSendStream.Length);
            byte[] data = new byte[num2];
            Buffer.BlockCopy(buffer, 0, data, 0, num2);
            tcpNetwork.Send(data);

            return true;
        }

        public bool Send(Rpc rpc)
        {
            rpc.SocketID = m_oNetwork.GetSocketID();
            rpc.BeforeSend();
            m_oSendStream.SetLength(0L);
            m_oSendStream.Position = 0L;
            rpc.SerializeWithHead(m_oSendStream);
            if (XSingleton<XDebug>.singleton.EnableRecord())
            {
                XSingleton<XDebug>.singleton.AddPoint(rpc.GetRpcType(), rpc.GetType().Name, m_oSendStream.Length, 1, XDebug.RecordChannel.ENetwork);
            }
            if (m_oSendStream.Length > 1024)
            {
                XSingleton<XDebug>.singleton.AddWarningLog2("Send Rpc:{0} to long:{1}b", rpc.GetRpcType(), m_oSendStream.Length);
            }
            bool flag = m_oNetwork.Send(m_oSendStream.GetBuffer(), 0, (int)m_oSendStream.Length);
            if (flag)
            {
                rpc.AfterSend();
            }
            return flag;
        }
    }
}
