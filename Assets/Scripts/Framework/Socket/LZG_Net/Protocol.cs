using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using XUtliPoolLib;

namespace XMainClient
{
	public abstract class Protocol
	{
		public class ProtocolFactry
		{
			public Protocol protocol = null;

			public Queue<Protocol> queue;

			public ProtocolFactry(Protocol p)
			{
				protocol = p;
				queue = new Queue<Protocol>();
			}

			public Protocol Create()
			{
				if (protocol != null)
				{
					return Activator.CreateInstance(protocol.GetType()) as Protocol;
				}
				return null;
			}

			public Protocol Get()
			{
				if (queue.Count > 0)
				{
					return queue.Dequeue();
				}
				return Create();
			}

			public void Return(Protocol protocol)
			{
				queue.Enqueue(protocol);
			}
		}

		public static Dictionary<uint, ProtocolFactry> sm_RegistProtocolFactory = new Dictionary<uint, ProtocolFactry>(120);

		protected EProtocolErrCode m_threadErrCode = EProtocolErrCode.ENoErr;

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

		public virtual uint GetProtoType()
		{
			return 0u;
		}

		public void SerializeWithHead(MemoryStream stream)
		{
			long position = stream.Position;
			ProtocolHead sharedHead = ProtocolHead.SharedHead;
			sharedHead.Reset();
			sharedHead.type = GetProtoType();
			sharedHead.flag = 0u;
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

		public static Protocol GetProtocolThread(uint dwType)
		{
			Protocol result = null;
			Monitor.Enter(sm_RegistProtocolFactory);
			ProtocolFactry value = null;
			if (sm_RegistProtocolFactory.TryGetValue(dwType, out value))
			{
				result = value.Get();
			}
			Monitor.Exit(sm_RegistProtocolFactory);
			return result;
		}

		public static void ReturnProtocolThread(Protocol protocol)
		{
			if (sm_RegistProtocolFactory != null && protocol != null)
			{
				Monitor.Enter(sm_RegistProtocolFactory);
				ProtocolFactry value = null;
				if (sm_RegistProtocolFactory.TryGetValue(protocol.GetProtoType(), out value))
				{
					value.Return(protocol);
				}
				Monitor.Exit(sm_RegistProtocolFactory);
			}
		}

		public static bool RegistProtocol(Protocol protocol)
		{
			if (sm_RegistProtocolFactory.ContainsKey(protocol.GetProtoType()))
			{
				return false;
			}
			sm_RegistProtocolFactory.Add(protocol.GetProtoType(), new ProtocolFactry(protocol));
			return true;
		}

		public static void ManualReturn()
		{
			CNetProcessor.ManualReturnProtocol();
		}

		public virtual bool CheckPValid()
		{
			if (m_threadErrCode == EProtocolErrCode.EDeSerializeErr)
			{
				XSingleton<XDebug>.singleton.AddErrorLog("Ptc EDeSerializeErr Type:", GetProtoType().ToString());
				return false;
			}
			return true;
		}

		public virtual void Process()
		{
		}
	}
}
