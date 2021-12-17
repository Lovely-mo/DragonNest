namespace XUtliPoolLib
{
	public class SmallBufferPool<T>
	{
		public T[] buffer;

		public BufferBlock[] blocks;

		private BlockInfo[] blockInitRef;

		public int allocBlockCount = 0;

		public void Init(BlockInfo[] blockInit, int tSize)
		{
			blockInitRef = blockInit;
			int num = 0;
			int num2 = 0;
			int i = 0;
			for (int num3 = blockInit.Length; i < num3; i++)
			{
				BlockInfo blockInfo = blockInit[i];
				num += blockInfo.count;
				num2 += blockInfo.size * blockInfo.count;
			}
			buffer = new T[num2];
			blocks = new BufferBlock[num];
			BufferPoolMgr.TotalCount += num2 * tSize + num * 17;
			int num4 = 0;
			int num5 = 0;
			int j = 0;
			for (int num6 = blockInit.Length; j < num6; j++)
			{
				BlockInfo blockInfo2 = blockInit[j];
				int k = 0;
				for (int count = blockInfo2.count; k < count; k++)
				{
					BufferBlock bufferBlock = blocks[num4];
					bufferBlock.offset = num5;
					bufferBlock.size = 0;
					bufferBlock.capacity = blockInfo2.size;
					bufferBlock.blockIndex = num4;
					bufferBlock.inUse = false;
					blocks[num4] = bufferBlock;
					num5 += blockInfo2.size;
					num4++;
				}
			}
		}

		private bool InnerGetBlock(ref BufferBlock block, int size, int initSize)
		{
			int num = 0;
			int i = 0;
			for (int num2 = blockInitRef.Length; i < num2; i++)
			{
				BlockInfo blockInfo = blockInitRef[i];
				if (blockInfo.size >= size)
				{
					int j = num;
					for (int num3 = num + blockInfo.count; j < num3; j++)
					{
						BufferBlock bufferBlock = blocks[j];
						if (!bufferBlock.inUse)
						{
							bufferBlock.size = ((initSize < bufferBlock.capacity) ? initSize : bufferBlock.capacity);
							bufferBlock.inUse = true;
							blocks[j] = bufferBlock;
							block = bufferBlock;
							allocBlockCount++;
							return true;
						}
					}
				}
				else
				{
					num += blockInfo.count;
				}
			}
			block.blockIndex = -1;
			block.capacity = size;
			block.size = ((initSize < size) ? initSize : size);
			block.inUse = true;
			return false;
		}

		private void InnerGetBlock(ref SmallBuffer<T> sb, int size, int initSize)
		{
			BufferBlock block = default(BufferBlock);
			if (InnerGetBlock(ref block, size, initSize))
			{
				sb.Init(block, this);
				sb.DeepClear();
			}
			else
			{
				sb.Init(block, this, new T[block.capacity]);
				XSingleton<XDebug>.singleton.AddWarningLog2("not enough buff size:{0}", block.capacity);
				BufferPoolMgr.AllocSize += block.capacity;
			}
		}

		public void GetBlock(ref SmallBuffer<T> sb, int size, int initSize = 0)
		{
			if (!sb.IsInit)
			{
				InnerGetBlock(ref sb, size, initSize);
			}
		}

		public void ExpandBlock(ref SmallBuffer<T> sb, int size)
		{
			InnerGetBlock(ref sb, size, 0);
		}

		public void ReturnBlock(ref SmallBuffer<T> sb)
		{
			BufferBlock bufferBlock = sb.bufferBlock;
			if (bufferBlock.inUse)
			{
				bufferBlock.size = 0;
				bufferBlock.inUse = false;
				if (bufferBlock.blockIndex >= 0)
				{
					blocks[bufferBlock.blockIndex] = bufferBlock;
					allocBlockCount--;
				}
				sb.debugName = "";
				sb.UnInit();
			}
		}
	}
}
