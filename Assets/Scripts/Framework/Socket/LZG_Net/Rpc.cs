using System;
using System.IO;
using System.Threading;
using UnityEngine;
using XUtliPoolLib;

namespace XMainClient
{
	public abstract class Rpc
	{
		private static readonly float _delayThreshold = 1f;

		private static readonly float _timeout_close_Threshold = 15f;

		private static bool _is_rpc_delay = false;

		private static bool _is_rpc_close_time_out = false;

		private static float _rpc_delayed_time = 0f;

		public static uint sMaxTagID = 1024u;

		private static Rpc[] sm_RpcWaitingReplyCache = new Rpc[sMaxTagID];

		private static uint sTagID = 0u;

		public static string delayRpcName = "";

		public int SocketID;

		private float sendTime;

		public long replyTick;

		private XTimerMgr.ElapsedEventHandler _timeCb = null;

		protected EProtocolErrCode m_threadErrCode = EProtocolErrCode.ENoErr;

		private uint tagID = 0u;

		private uint timerToken = 0u;

		private float timeout = _timeout_close_Threshold - 1f;

		public static bool OnRpcDelay
		{
			get
			{
				return _is_rpc_delay;
			}
		}

		public static bool OnRpcTimeOutClosed
		{
			get
			{
				return _is_rpc_close_time_out;
			}
		}

		public static float RpcDelayedTime
		{
			get
			{
				return _rpc_delayed_time;
			}
		}

		public static float DelayThreshold
		{
			get
			{
				return _delayThreshold;
			}
		}

		public EProtocolErrCode ThreadErrCode
		{
			get
			{
				return m_threadErrCode;
			}
			set
			{
				m_threadErrCode = value;
			}
		}

		public float Timeout
		{
			get
			{
				return timeout;
			}
			set
			{
				timeout = value;
			}
		}

		public uint TimerToken
		{
			get
			{
				return timerToken;
			}
		}

		public Rpc()
		{
			_timeCb = TimerCallback;
			switch (GetRpcType())
			{
			case 28358u:
				timeout = 5f;
				break;
			case 10091u:
				timeout = 5f;
				break;
			case 9179u:
				timeout = 5f;
				break;
			case 30514u:
			//	timeout = (float)XServerTimeMgr.SyncTimeOut / 1000f;
				break;
			case 25069u:
				timeout = 5f;
				break;
			}
		}

		public virtual uint GetRpcType()
		{
			return 0u;
		}

		public void SerializeWithHead(MemoryStream stream)
		{
			long position = stream.Position;
			ProtocolHead sharedHead = ProtocolHead.SharedHead;
			sharedHead.type = GetRpcType();
			sharedHead.flag = 3u;
			sharedHead.tagID = tagID;
			sharedHead.Serialize(stream);
			Serialize(stream);
			long position2 = stream.Position;
			uint value = (uint)(position2 - position - 4);
			stream.Position = position;
			stream.Write(BitConverter.GetBytes(value), 0, 4);
			stream.Position = position2;
		}

		public abstract void Serialize(MemoryStream stream);

		public abstract void DeSerialize(MemoryStream stream);

		public virtual bool CheckPValid()
		{
			if (m_threadErrCode == EProtocolErrCode.EDeSerializeErr)
			{
				XSingleton<XDebug>.singleton.AddErrorLog("Roc EDeSerializeErr Type:", GetRpcType().ToString());
				return false;
			}
			return true;
		}

		public void BeforeSend()
		{
			sTagID++;
			if (sTagID >= sMaxTagID)
			{
				sTagID = 0u;
			}
			tagID = sTagID;
		}

		public void AfterSend()
		{
			sendTime = Time.realtimeSinceStartup;
			Monitor.Enter(sm_RpcWaitingReplyCache);
			if (tagID < sm_RpcWaitingReplyCache.Length)
			{
				if (sm_RpcWaitingReplyCache[tagID] != null)
				{
					XSingleton<XDebug>.singleton.AddErrorLog("rpc not processed yet");
				}
				sm_RpcWaitingReplyCache[tagID] = this;
			}
			Monitor.Exit(sm_RpcWaitingReplyCache);
		}

		public void SetTimeOut()
		{
			if (_timeCb == null)
			{
				_timeCb = TimerCallback;
			}
			timerToken = XSingleton<XTimerMgr>.singleton.SetGlobalTimer(timeout, _timeCb, null);
		}

		public void CallTimeOut()
		{
			XSingleton<XTimerMgr>.singleton.KillTimer(timerToken);
			OnTimeout(null);
		}

		private void TimerCallback(object args)
		{
			//RemoveRpcByTag(tagID);
			//OnTimeout(args);
			//if (GetRpcType() != 30514 && GetRpcType() != 39595 && XSingleton<XClientNetwork>.singleton.IsConnected())
			//{
			//	_is_rpc_close_time_out = true;
			//	delayRpcName = ToString();
			//}
			//XSingleton<XDebug>.singleton.AddWarningLog("RPC TimeOut: ", ToString());
		}

		public virtual void Process()
		{
			XSingleton<XTimerMgr>.singleton.KillTimer(timerToken);
		}

		public void RemoveTimer()
		{
			XSingleton<XTimerMgr>.singleton.KillTimer(timerToken);
		}

		public abstract void OnTimeout(object args);

		public static void RemoveRpcByTag(uint dwTag)
		{
			Monitor.Enter(sm_RpcWaitingReplyCache);
			if (dwTag < sm_RpcWaitingReplyCache.Length)
			{
				sm_RpcWaitingReplyCache[dwTag] = null;
			}
			Monitor.Exit(sm_RpcWaitingReplyCache);
		}

		public static Rpc GetRemoveRpcByTag(uint dwTag)
		{
			Rpc result = null;
			Monitor.Enter(sm_RpcWaitingReplyCache);
			if (dwTag < sm_RpcWaitingReplyCache.Length)
			{
				result = sm_RpcWaitingReplyCache[dwTag];
				sm_RpcWaitingReplyCache[dwTag] = null;
			}
			Monitor.Exit(sm_RpcWaitingReplyCache);
			return result;
		}

		public static void CheckDelay()
		{
			_is_rpc_delay = false;
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			_rpc_delayed_time = 0f;
			Monitor.Enter(sm_RpcWaitingReplyCache);
			for (int i = 0; i < sm_RpcWaitingReplyCache.Length; i++)
			{
				Rpc rpc = sm_RpcWaitingReplyCache[i];
				if (rpc != null && rpc.GetRpcType() != 30514 && rpc.GetRpcType() != 28358 && rpc.GetRpcType() != 45201 && rpc.GetRpcType() != 39595)
				{
					float num = realtimeSinceStartup - rpc.sendTime;
					if (_rpc_delayed_time < num)
					{
						_rpc_delayed_time = num;
					}
					if (num > _delayThreshold)
					{
						_is_rpc_delay = true;
						delayRpcName = rpc.ToString();
						break;
					}
				}
			}
			Monitor.Exit(sm_RpcWaitingReplyCache);
		}

		public static void Close()
		{
			Monitor.Enter(sm_RpcWaitingReplyCache);
			for (int i = 0; i < sm_RpcWaitingReplyCache.Length; i++)
			{
				Rpc rpc = sm_RpcWaitingReplyCache[i];
				if (rpc != null)
				{
					XSingleton<XTimerMgr>.singleton.KillTimer(rpc.TimerToken);
					rpc.OnTimeout(null);
				}
				sm_RpcWaitingReplyCache[i] = null;
			}
			Monitor.Exit(sm_RpcWaitingReplyCache);
			_is_rpc_delay = false;
			_is_rpc_close_time_out = false;
		}
	}
}
