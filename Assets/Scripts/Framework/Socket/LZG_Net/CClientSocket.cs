using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using XUtliPoolLib;

namespace XMainClient
{
    public class CClientSocket
	{
		private class SendState
		{
			public int start;

			public int len;
		}

		private Socket m_oSocket;

		private SocketState m_nState;

		private static byte[] m_oSendBuff;

		private static byte[] m_oRecvBuff;

		private volatile int m_nStartPos;

		private volatile int m_nEndPos;

		private int m_nCurrRecvLen;

		private SendState m_oSendState;

		public static int TotalSendBytes;

		public static int TotalRecvBytes;

		private CNetwork m_oNetwork;

		private IPacketBreaker m_oBreaker;

		private int m_nUID;

		private static int SOCKET_UID = 0;

		private AsyncCallback m_RecvCb = null;

		private AsyncCallback m_ConnectCb = null;

		private AsyncCallback m_SendCb = null;

		public bool m_bRecvMsg = true;

		public bool m_bPause = false;

		public int m_nPauseRecvLen = 0;

		public static int PAUSE_RECV_MAX_LEN = 10240;

		public static int MaxSendSize = 1024;

		public static int SendBufferSize = 32768;

		private AddressFamily m_NetworkType = AddressFamily.InterNetwork;

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

		public int ID
		{
			get
			{
				return m_nUID;
			}
		}

		public CClientSocket()
		{
			m_oSocket = null;
			m_nState = SocketState.State_Closed;
			m_oSendBuff = null;
			m_oRecvBuff = null;
			m_nStartPos = 0;
			m_nEndPos = 0;
			m_nCurrRecvLen = 0;
			m_oSendBuff = null;
			m_oBreaker = null;
			m_nUID = ++SOCKET_UID;
			m_RecvCb = RecvCallback;
			m_ConnectCb = OnConnect;
			m_SendCb = OnSendCallback;

		}

		private void GetNetworkType()
		{
			//try
			//{
			//	string hostNameOrAddress = XSingleton<XUpdater.XUpdater>.singleton.XPlatform.GetLoginServer("QQ").Substring(0, XSingleton<XUpdater.XUpdater>.singleton.XPlatform.GetLoginServer("QQ").LastIndexOf(':'));
			//	IPAddress[] hostAddresses = Dns.GetHostAddresses(hostNameOrAddress);
			//	m_NetworkType = hostAddresses[0].AddressFamily;
			//}
			//catch (Exception ex)
			//{
			//	XSingleton<XDebug>.singleton.AddWarningLog(ex.Message);
			//}
		}

		public bool Init(uint dwSendBuffSize, uint dwRecvBuffSize, CNetwork oNetwork, IPacketBreaker oBreaker)
		{
			GetNetworkType();
			try
			{
				m_nState = SocketState.State_Closed;
				m_oSocket = new Socket(m_NetworkType, SocketType.Stream, ProtocolType.Tcp);
				m_oSocket.NoDelay = true;
				if (m_oSendBuff == null)
				{
					m_oSendBuff = new byte[dwSendBuffSize];
				}
				if (m_oRecvBuff == null)
				{
					m_oRecvBuff = new byte[dwRecvBuffSize];
				}
				m_oSocket.SendBufferSize = SendBufferSize;
				m_oSendState = new SendState();
				m_nStartPos = 0;
				m_nEndPos = 0;
				m_nCurrRecvLen = 0;
				m_oNetwork = oNetwork;
				m_oBreaker = oBreaker;
				m_oSendState.start = 0;
				m_oSendState.len = 0;
			}
			catch (Exception ex)
			{
				XSingleton<XDebug>.singleton.AddWarningLog(ex.Message, "new Socket Error!");
				return false;
			}
			return true;
		}

		public void UnInit()
		{
			Close();
		}

		private void OnConnect(IAsyncResult iar)
		{
			try
			{
				if (m_nState != 0)
				{
					Socket socket = (Socket)iar.AsyncState;
					socket.EndConnect(iar);
					SetState(SocketState.State_Connected);
					GetNetwork().PushConnectEvent(true);
					socket.BeginReceive(m_oRecvBuff, m_nCurrRecvLen, m_oRecvBuff.Length - m_nCurrRecvLen, SocketFlags.None, m_RecvCb, socket);
					FindSomethingToSend();
				}
			}
			catch (Exception ex)
			{
				XSingleton<XDebug>.singleton.AddWarningLog(ex.Message);
				SetState(SocketState.State_Closed);
				GetNetwork().PushConnectEvent(false);
				Close();
			}
		}

		public bool Connect(string host, int port)
		{
			try
			{
				SetState(SocketState.State_Connecting);
				m_oSocket.BeginConnect(host, port, m_ConnectCb, m_oSocket);
				return true;
			}
			catch (Exception ex)
			{
				XSingleton<XDebug>.singleton.AddWarningLog(ex.Message);
				return false;
			}
		}

		public void Close()
		{
			XSingleton<XDebug>.singleton.AddLog("close socket ", m_nUID.ToString());
			Rpc.Close();
			m_nState = SocketState.State_Closed;
			if (m_oSocket != null)
			{
				try
				{
					m_oSocket.Close();
				}
				catch (Exception ex)
				{
					XSingleton<XDebug>.singleton.AddWarningLog(ex.Message);
				}
				m_oSocket = null;
			}
		}

		public bool Send(byte[] buffer)
		{
			return Send(buffer, 0, buffer.Length);
		}

		private void OnSendCallback(IAsyncResult iar)
		{
			try
			{
				if (m_nState != 0)
				{
					SendState sendState = (SendState)iar.AsyncState;
					m_oSocket.EndSend(iar);
					m_nStartPos = (sendState.start + sendState.len) % m_oSendBuff.Length;
					TotalSendBytes += sendState.len;
					FindSomethingToSend();
				}
			}
			catch (Exception ex)
			{
				XSingleton<XDebug>.singleton.AddWarningLog("OnSendCallback Send Failed: ", ex.Message);
				GetNetwork().PushClosedEvent(NetErrCode.Net_SysError, ID);
				Close();
			}
		}

		private void FindSomethingToSend()
		{
			try
			{
				while (m_nEndPos == m_nStartPos)
				{
					Thread.Sleep(1);
					if (m_oSocket == null)
					{
						return;
					}
				}
				if (m_nEndPos > m_nStartPos)
				{
					m_oSendState.start = m_nStartPos;
					m_oSendState.len = m_nEndPos - m_nStartPos;
					if (m_oSendState.len > MaxSendSize)
					{
						m_oSendState.len = MaxSendSize;
					}
					m_oSocket.BeginSend(m_oSendBuff, m_oSendState.start, m_oSendState.len, SocketFlags.None, m_SendCb, m_oSendState);
				}
				else if (m_nEndPos < m_nStartPos)
				{
					m_oSendState.start = m_nStartPos;
					m_oSendState.len = m_oSendBuff.Length - m_nStartPos;
					if (m_oSendState.len > MaxSendSize)
					{
						m_oSendState.len = MaxSendSize;
					}
					m_oSocket.BeginSend(m_oSendBuff, m_oSendState.start, m_oSendState.len, SocketFlags.None, m_SendCb, m_oSendState);
				}
				else
				{
					XSingleton<XDebug>.singleton.AddWarningLog("FindSomethingToSend Send Failed: No data to send error!");
				}
			}
			catch (Exception ex)
			{
				Close();
				GetNetwork().PushClosedEvent(NetErrCode.Net_SysError, ID);
				XSingleton<XDebug>.singleton.AddWarningLog("FindSomethingToSend Exception Send Failed: ", ex.Message);
			}
		}

		public bool Send(byte[] buffer, int start, int length)
		{
			if (GetState() != SocketState.State_Connected)
			{
				XSingleton<XDebug>.singleton.AddLog("state is not connected, can't send!");
				return false;
			}
			int num = m_oSendBuff.Length;
			int num2 = m_nEndPos + num - m_nStartPos;
			int num3 = (num2 >= num) ? (num2 - num) : num2;
			if (length + 1 + num3 > num)
			{
				XSingleton<XDebug>.singleton.AddWarningLog("send bytes out of buffer range!");
				Close();
				GetNetwork().PushClosedEvent(NetErrCode.Net_SendBuff_Overflow, ID);
				return false;
			}
			if (m_nEndPos + length >= num)
			{
				int num4 = num - m_nEndPos;
				int num5 = length - num4;
				Array.Copy(buffer, start, m_oSendBuff, m_nEndPos, num4);
				Array.Copy(buffer, start + num4, m_oSendBuff, 0, num5);
				m_nEndPos = num5;
			}
			else
			{
				Array.Copy(buffer, start, m_oSendBuff, m_nEndPos, length);
				m_nEndPos += length;
			}
			return true;
		}

		public Socket GetSocket()
		{
			return m_oSocket;
		}

		public SocketState GetState()
		{
			return m_nState;
		}

		private void SetState(SocketState nState)
		{
			m_nState = nState;
		}

		private CNetwork GetNetwork()
		{
			return m_oNetwork;
		}

		public void RecvCallback(IAsyncResult ar)
		{
			try
			{
				if (m_nState != 0)
				{
					Socket socket = (Socket)ar.AsyncState;
					int num = socket.EndReceive(ar);
					if (num > 0)
					{
						TotalRecvBytes += num;
						m_nCurrRecvLen += num;
						DetectPacket();
						if (m_nCurrRecvLen == m_oRecvBuff.Length)
						{
							XSingleton<XDebug>.singleton.AddWarningLog("RecvCallback error ! m_nCurrRecvLen == m_oRecvBuff.Length");
						}
						socket.BeginReceive(m_oRecvBuff, m_nCurrRecvLen, m_oRecvBuff.Length - m_nCurrRecvLen, SocketFlags.None, m_RecvCb, socket);
					}
					else if (num == 0)
					{
						XSingleton<XDebug>.singleton.AddWarningLog("Close socket normally");
						Close();
						GetNetwork().PushClosedEvent(NetErrCode.Net_SysError, ID);
					}
					else
					{
						XSingleton<XDebug>.singleton.AddWarningLog("Close socket, recv error!");
						Close();
						GetNetwork().PushClosedEvent(NetErrCode.Net_SysError, ID);
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (SocketException ex2)
			{
				XSingleton<XDebug>.singleton.AddWarningLog(ex2.Message);
				Close();
				GetNetwork().PushClosedEvent(NetErrCode.Net_SysError, ID);
			}
			catch (Exception ex3)
			{
				XSingleton<XDebug>.singleton.AddWarningLog(ex3.Message);
				Close();
				GetNetwork().PushClosedEvent(NetErrCode.Net_SysError, ID);
			}
		}

		public void DetectPacket()
		{
			int num = 0;
			while (m_nCurrRecvLen > 0)
			{
               /// Debug.Log("DetectPacket" + m_nCurrRecvLen);
				int num2 = m_oBreaker.BreakPacket(m_oRecvBuff, num, m_nCurrRecvLen);
				if (num2 == 0)
				{
					break;
				}
				if (m_bRecvMsg)
				{
                    Debug.Log("DetectPacket" + m_nCurrRecvLen);
                    SmallBuffer<byte> sb = default(SmallBuffer<byte>);
					GetBuffer(ref sb, num2);
					sb.Copy(m_oRecvBuff, num, 0, num2);
					GetNetwork().PushReceiveEvent(ref sb, num2);
					if (m_bPause)
					{
						m_nPauseRecvLen += num2;
						if (m_nPauseRecvLen > PAUSE_RECV_MAX_LEN)
						{
							m_bRecvMsg = false;
						}
					}
				}
				num += num2;
				m_nCurrRecvLen -= num2;
			}
			if (num > 0 && m_nCurrRecvLen > 0)
			{
				Array.Copy(m_oRecvBuff, num, m_oRecvBuff, 0, m_nCurrRecvLen);
			}
		}

		public static void GetBuffer(ref SmallBuffer<byte> sb, int size)
		{
			lock (m_BufferPool)
			{
				m_BufferPool.GetBlock(ref sb, size);
			}
		}

		public static void ReturnBuffer(ref SmallBuffer<byte> sb)
		{
			if (m_BufferPool == null)
			{
				m_BufferPool = new SmallBufferPool<byte>();
				m_BufferPool.Init(blockInit, 1);
			}
			lock (m_BufferPool)
			{
				m_BufferPool.ReturnBlock(ref sb);
			}
		}
	}
}
