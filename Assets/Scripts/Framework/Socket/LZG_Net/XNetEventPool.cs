using System.Collections.Generic;
using System.Threading;

namespace XMainClient
{
	internal class XNetEventPool
	{
		private static Queue<NetEvent> _pool = new Queue<NetEvent>(128);

		public static NetEvent GetEvent()
		{
			NetEvent netEvent = null;
			Monitor.Enter(_pool);
			if (_pool.Count > 0)
			{
				netEvent = _pool.Dequeue();
			}
			Monitor.Exit(_pool);
			if (netEvent != null)
			{
				netEvent.Reset();
				return netEvent;
			}
			return new NetEvent();
		}

		public static void Recycle(NetEvent e)
		{
			Monitor.Enter(_pool);
			_pool.Enqueue(e);
			Monitor.Exit(_pool);
		}

		public static void RecycleNoLock(NetEvent e)
		{
			_pool.Enqueue(e);
		}

		public static void Clear()
		{
			Monitor.Enter(_pool);
			_pool.Clear();
			Monitor.Exit(_pool);
		}
	}
}
