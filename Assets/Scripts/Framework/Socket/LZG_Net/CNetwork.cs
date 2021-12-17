using System.Collections.Generic;
using System.Threading;
using XUtliPoolLib;

namespace XMainClient
{
	public class CNetwork : ILuaNetwork, IXInterface
	{
		private Queue<LuaNetNode> m_oLuaNodeCache = new Queue<LuaNetNode>();

		private Queue<byte[]> m_oLuaBufferCache = new Queue<byte[]>();

		private static LuaNetNode[] sm_RpcWaitingReplyCache = null;

		private static uint sm_luaTagID = Rpc.sMaxTagID;

		private List<uint> m_onlydispacherInLua = null;

		private Dictionary<uint, bool> m_registedPtcList = null;

		private ILuaNetSender luaSender = null;

		private CClientSocket m_oSocket;

		private Queue<NetEvent> m_oPreProcessQueue;

		private bool m_bUse3Thread = false;

		private int m_iMaxCountPerFrame = 50;

		private Thread m_oPreProcessThread;

		private Queue<NetEvent> m_oDataQueue;

		private INetSender m_oSender;

		private INetProcess m_oProcess;

		private IPacketBreaker m_oBreaker;

		private uint m_dwSendBuffSize;

		private uint m_dwRecvBuffSize;

		public bool Deprecated
		{
			get;
			set;
		}

		public int SendBytes
		{
			get
			{
				return CClientSocket.TotalSendBytes;
			}
		}

		public int RecvBytes
		{
			get
			{
				return CClientSocket.TotalRecvBytes;
			}
		}

		private bool LuaSend(uint _type, bool isRpc, uint tagID, byte[] _reqBuff)
		{
			if (GetSocketState() == SocketState.State_Connected)
			{
				if (luaSender == null)
				{
					luaSender = (m_oSender as ILuaNetSender);
				}
				if (luaSender != null)
				{
					return luaSender.Send(_type, isRpc, tagID, _reqBuff);
				}
			}
			return false;
		}

		public void InitLua(int rpcCache)
		{
			sm_RpcWaitingReplyCache = new LuaNetNode[rpcCache];
		}

		public bool LuaRigsterPtc(uint type, bool copyBuffer)
		{
			if (m_registedPtcList == null)
			{
				m_registedPtcList = new Dictionary<uint, bool>();
			}
			if (!m_registedPtcList.ContainsKey(type))
			{
				m_registedPtcList.Add(type, copyBuffer);
				return true;
			}
			return false;
		}

		public void LuaRegistDispacher(List<uint> types)
		{
			if (m_onlydispacherInLua == null)
			{
				m_onlydispacherInLua = new List<uint>();
			}
			m_onlydispacherInLua = types;
		}

		private void RegisterRPC(uint tag, uint _type, bool copyBuffer, DelLuaRespond _onRes, DelLuaError _onError)
		{
			if (sm_RpcWaitingReplyCache != null)
			{
				LuaNetNode node = GetNode(copyBuffer);
				node.Reset();
				node.isRpc = true;
				node.tagID = sm_luaTagID;
				node.copyBuffer = copyBuffer;
				node.resp = _onRes;
				node.err = _onError;
				Monitor.Enter(sm_RpcWaitingReplyCache);
				sm_RpcWaitingReplyCache[tag] = node;
				Monitor.Exit(sm_RpcWaitingReplyCache);
			}
		}

		public void LuaRigsterRPC(uint _type, bool copyBuffer, DelLuaRespond _onRes, DelLuaError _onError)
		{
			uint rPCTag = GetRPCTag();
			RegisterRPC(rPCTag, _type, copyBuffer, _onRes, _onError);
		}

		public bool ConatainPtc(uint type)
		{
			if (m_registedPtcList == null)
			{
				return false;
			}
			return m_registedPtcList.ContainsKey(type);
		}

		public bool ContainPtc(uint type, out bool copyBuffer)
		{
			if (m_registedPtcList == null)
			{
				m_registedPtcList = new Dictionary<uint, bool>();
			}
			return m_registedPtcList.TryGetValue(type, out copyBuffer);
		}

		public bool IsOnlyDispacherInLua(uint type)
		{
			if (m_onlydispacherInLua == null)
			{
				return false;
			}
			return m_onlydispacherInLua.Contains(type);
		}

		private uint GetRPCTag()
		{
			uint num = sm_luaTagID++ - Rpc.sMaxTagID;
			if (num >= sm_RpcWaitingReplyCache.Length)
			{
				sm_luaTagID = Rpc.sMaxTagID;
				num = 0u;
			}
			return num;
		}

		public LuaNetNode GetRemoveRpc(uint tagID)
		{
			tagID -= Rpc.sMaxTagID;
			LuaNetNode result = null;
			if (sm_RpcWaitingReplyCache != null)
			{
				Monitor.Enter(sm_RpcWaitingReplyCache);
				if (tagID >= 0 && tagID < sm_RpcWaitingReplyCache.Length)
				{
					result = sm_RpcWaitingReplyCache[tagID];
					sm_RpcWaitingReplyCache[tagID] = null;
				}
				Monitor.Exit(sm_RpcWaitingReplyCache);
			}
			return result;
		}

		public bool LuaSendPtc(uint _type, byte[] _reqBuff)
		{
			return LuaSend(_type, false, 0u, _reqBuff);
		}

		public bool LuaSendRPC(uint _type, byte[] _reqBuff, DelLuaRespond _onRes, DelLuaError _onError)
		{
			if (GetSocketState() == SocketState.State_Connected)
			{
				uint tagID = sm_luaTagID;
				uint rPCTag = GetRPCTag();
				if (LuaSend(_type, true, tagID, _reqBuff))
				{
					RegisterRPC(rPCTag, _type, true, _onRes, _onError);
					return true;
				}
			}
			return false;
		}

		private byte[] GetBuffer()
		{
			if (m_oLuaBufferCache.Count > 0)
			{
				return m_oLuaBufferCache.Dequeue();
			}
			return new byte[CNetProcessor.MaxBuffSize];
		}

		public LuaNetNode GetNode(bool allocBuffer)
		{
			LuaNetNode luaNetNode = null;
			Monitor.Enter(m_oLuaNodeCache);
			luaNetNode = ((m_oLuaNodeCache.Count <= 0) ? new LuaNetNode() : m_oLuaNodeCache.Dequeue());
			if (allocBuffer)
			{
				luaNetNode.buffer = GetBuffer();
			}
			Monitor.Exit(m_oLuaNodeCache);
			return luaNetNode;
		}

		public void ReturnNode(LuaNetNode node)
		{
			Monitor.Enter(m_oLuaNodeCache);
			m_oLuaNodeCache.Enqueue(node);
			if (node.buffer != null)
			{
				m_oLuaBufferCache.Enqueue(node.buffer);
				node.buffer = null;
			}
			Monitor.Exit(m_oLuaNodeCache);
		}

		public void LuaClear()
		{
			Monitor.Enter(m_oLuaNodeCache);
			m_oLuaNodeCache.Clear();
			m_oLuaBufferCache.Clear();
			Monitor.Exit(m_oLuaNodeCache);
		}

		public static void PrintBytes(byte[] bytes)
		{
			PrintBytes("LUA", bytes);
		}

		public static void PrintBytes(string tag, byte[] bytes)
		{
		}

		public CNetwork()
		{
			m_oSocket = null;
			m_oProcess = null;
			m_dwSendBuffSize = 0u;
			m_dwRecvBuffSize = 0u;
		}

		public bool Init(INetProcess oProc, INetSender oSender, IPacketBreaker oBreaker, uint dwSendBuffSize, uint dwRecvBuffSize)
		{
			m_oSender = oSender;
			m_oProcess = oProc;
			m_oBreaker = oBreaker;
			m_dwSendBuffSize = dwSendBuffSize;
			m_dwRecvBuffSize = dwRecvBuffSize;
			m_oDataQueue = new Queue<NetEvent>();
			m_oPreProcessQueue = new Queue<NetEvent>();
			if (m_bUse3Thread)
			{
				m_oPreProcessThread = new Thread(PreProcess);
				m_oPreProcessThread.Start();
			}
			return true;
		}

		public void UnInit()
		{
		}

		public int GetSocketID()
		{
			if (m_oSocket == null)
			{
				return 0;
			}
			return m_oSocket.ID;
		}

		public bool IsDisconnect()
		{
			return GetSocketState() == SocketState.State_Closed;
		}

		public bool IsConnecting()
		{
			return GetSocketState() == SocketState.State_Connecting;
		}

		public bool IsConnected()
		{
			return GetSocketState() == SocketState.State_Connected;
		}

		public SocketState GetSocketState()
		{
			if (m_oSocket == null)
			{
				return SocketState.State_Closed;
			}
			return m_oSocket.GetState();
		}

		public bool Send(Protocol protocol)
		{
			if (GetSocketState() == SocketState.State_Connected && m_oSender.Send(protocol))
			{
				return true;
			}
			return false;
		}

		public bool Send(Rpc rpc)
		{
			if (GetSocketState() == SocketState.State_Connected && m_oSender.Send(rpc))
			{
				return true;
			}
			return false;
		}

		public bool Connect(string host, int port)
		{
			XSingleton<XDebug>.singleton.AddLog("connect to ", host, ":", port.ToString());
			if (m_oSocket == null)
			{
				m_oSocket = new CClientSocket();
			}
			if (!m_oSocket.Init(m_dwSendBuffSize, m_dwRecvBuffSize, this, m_oBreaker))
			{
				m_oSocket.Close();
				m_oProcess.OnConnect(false);
				return false;
			}
			bool flag = m_oSocket.Connect(host, port);
			if (!flag)
			{
				m_oProcess.OnConnect(false);
			}
			return flag;
		}

		public void Close(NetErrCode err)
		{
			if (m_oSocket == null)
			{
				return;
			}
			int iD = m_oSocket.ID;
			m_oSocket.Close();
			m_oSocket = null;
			if (m_oDataQueue.Count > 0)
			{
				for (NetEvent netEvent = m_oDataQueue.Dequeue(); netEvent != null; netEvent = ((m_oDataQueue.Count <= 0) ? null : m_oDataQueue.Dequeue()))
				{
					CClientSocket.ReturnBuffer(ref netEvent.m_oBuffer);
					XNetEventPool.RecycleNoLock(netEvent);
				}
				m_oDataQueue.Clear();
			}
			if (m_oPreProcessQueue.Count > 0)
			{
				for (NetEvent netEvent2 = m_oPreProcessQueue.Dequeue(); netEvent2 != null; netEvent2 = ((m_oPreProcessQueue.Count <= 0) ? null : m_oPreProcessQueue.Dequeue()))
				{
					CClientSocket.ReturnBuffer(ref netEvent2.m_oBuffer);
					XNetEventPool.RecycleNoLock(netEvent2);
				}
				m_oPreProcessQueue.Clear();
			}
			PushClosedEvent(err, iD);
		}

		public bool Send(byte[] buffer)
		{
			if (GetSocketState() == SocketState.State_Connected)
			{
				return m_oSocket.Send(buffer);
			}
			return false;
		}

		public bool Send(byte[] buffer, int start, int length)
		{
			if (GetSocketState() == SocketState.State_Connected)
			{
				return m_oSocket.Send(buffer, start, length);
			}
			return false;
		}
 

		public void EnQueue(NetEvent evt, bool propress)
		{
			if (evt == null)
			{
				XSingleton<XDebug>.singleton.AddErrorLog("null event EnQueue");
			}
			else if (propress)
			{
				Monitor.Enter(m_oPreProcessQueue);
				m_oPreProcessQueue.Enqueue(evt);
				Monitor.Exit(m_oPreProcessQueue);
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

		public void PushConnectEvent(bool bSuccess)
		{
			NetEvent @event = XNetEventPool.GetEvent();
			@event.m_nEvtType = NetEvtType.Event_Connect;
			@event.m_bSuccess = bSuccess;
			EnQueue(@event, false);
		}

		public void PushClosedEvent(NetErrCode nErrCode, int sockid)
		{
			NetEvent @event = XNetEventPool.GetEvent();
			@event.m_nEvtType = NetEvtType.Event_Closed;
			@event.m_nErrCode = nErrCode;
			@event.m_SocketID = sockid;
			EnQueue(@event, false);
		}

		public void PushReceiveEvent(ref SmallBuffer<byte> oData, int length)
		{
			NetEvent @event = XNetEventPool.GetEvent();
			@event.m_nEvtType = NetEvtType.Event_Receive;
			@event.m_oBuffer = oData;
			@event.m_nBufferLength = length;
			m_oProcess.OnPrePropress(@event);
			EnQueue(@event, m_bUse3Thread);
		}

		private void InnerPreprocess()
		{
			NetEvent netEvent = null;
			int num = 0;
			while (num < m_iMaxCountPerFrame)
			{
				netEvent = null;
				bool flag = false;
				Monitor.Enter(m_oPreProcessQueue);
				if (m_oPreProcessQueue.Count > 0)
				{
					netEvent = m_oPreProcessQueue.Dequeue();
					flag = true;
				}
				Monitor.Exit(m_oPreProcessQueue);
				if (!flag)
				{
					break;
				}
				num++;
				if (netEvent == null)
				{
					XSingleton<XDebug>.singleton.AddErrorLog("null event InnerPreprocess");
				}
				else if (netEvent.m_nEvtType == NetEvtType.Event_Receive)
				{
					m_oProcess.OnPrePropress(netEvent);
					EnQueue(netEvent, false);
				}
				else
				{
					XSingleton<XDebug>.singleton.AddErrorLog("unknown event", netEvent.m_nEvtType.ToString());
				}
			}
		}

		private void PreProcess()
		{
			while (true)
			{
				InnerPreprocess();
				Thread.Sleep(1);
			}
		}

		public void OnGamePaused(bool pause)
		{
			if (m_oSocket == null)
			{
				return;
			}
			if (pause)
			{
				m_oSocket.m_bPause = true;
				m_oSocket.m_nPauseRecvLen = 0;
				return;
			}
			m_oSocket.m_bPause = false;
			XSingleton<XDebug>.singleton.AddLog("PauseRecvLen: " + m_oSocket.m_nPauseRecvLen + ",  Max:" + CClientSocket.PAUSE_RECV_MAX_LEN);
			if (m_oSocket.m_nPauseRecvLen > CClientSocket.PAUSE_RECV_MAX_LEN)
			{
				Close(NetErrCode.Net_PauseRecv_Overflow);
			}
		}

		public void Clear()
		{
			XNetEventPool.Clear();
			LuaClear();
		}
	}
}
