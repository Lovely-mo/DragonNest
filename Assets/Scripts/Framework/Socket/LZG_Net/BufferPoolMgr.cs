namespace XUtliPoolLib
{
	public class BufferPoolMgr : XSingleton<BufferPoolMgr>
	{
		private SmallBufferPool<uint> m_UIntSmallBufferPool = new SmallBufferPool<uint>();

		private SmallBufferPool<object> m_ObjSmallBufferPool = new SmallBufferPool<object>();

		public static int TotalCount = 0;

		public static int AllocSize = 0;

		public BlockInfo[] uintBlockInit = new BlockInfo[7]
		{
			new BlockInfo(4, 512),
			new BlockInfo(8, 512),
			new BlockInfo(16, 512),
			new BlockInfo(32, 512),
			new BlockInfo(64, 512),
			new BlockInfo(256, 128),
			new BlockInfo(512, 128)
		};

		public BlockInfo[] objBlockInit = new BlockInfo[6]
		{
			new BlockInfo(8, 512),
			new BlockInfo(16, 512),
			new BlockInfo(32, 512),
			new BlockInfo(64, 512),
			new BlockInfo(256, 128),
			new BlockInfo(512, 128)
		};

		public override bool Init()
		{
			m_UIntSmallBufferPool.Init(uintBlockInit, 4);
			m_ObjSmallBufferPool.Init(objBlockInit, 4);
			return true;
		}

		public void GetSmallBuffer(ref SmallBuffer<uint> sb, int size, int initSize = 0)
		{
			m_UIntSmallBufferPool.GetBlock(ref sb, size);
		}

		public void ReturnSmallBuffer(ref SmallBuffer<uint> sb)
		{
			m_UIntSmallBufferPool.ReturnBlock(ref sb);
		}

		public void GetSmallBuffer(ref SmallBuffer<object> sb, int size, int initSize = 0)
		{
			m_ObjSmallBufferPool.GetBlock(ref sb, size);
		}

		public void ReturnSmallBuffer(ref SmallBuffer<object> sb)
		{
			m_ObjSmallBufferPool.ReturnBlock(ref sb);
		}
	}
}
