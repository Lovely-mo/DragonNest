using System;

namespace XUtliPoolLib
{
	public struct SmallBuffer<T>
	{
		public BufferBlock bufferBlock;

		private SmallBufferPool<T> poolRef;

		private T[] bufferRef;

		public string debugName;

		private T[] debugBuffer;

		public T this[int index]
		{
			get
			{
				return (bufferRef == null) ? default(T) : bufferRef[bufferBlock.offset + index];
			}
			set
			{
				if (bufferRef != null)
				{
					bufferRef[bufferBlock.offset + index] = value;
				}
				if (debugBuffer != null)
				{
					debugBuffer[index] = value;
				}
			}
		}

		public T this[uint index]
		{
			get
			{
				return bufferRef[bufferBlock.offset + index];
			}
			set
			{
				bufferRef[bufferBlock.offset + index] = value;
				if (debugBuffer != null)
				{
					debugBuffer[index] = value;
				}
			}
		}

		public int Count
		{
			get
			{
				return (bufferRef != null) ? bufferBlock.size : 0;
			}
		}

		public bool IsInit
		{
			get
			{
				return bufferRef != null;
			}
		}

		public T[] OriginalBuff
		{
			get
			{
				return bufferRef;
			}
		}

		public int StartOffset
		{
			get
			{
				return bufferBlock.offset;
			}
		}

		private void Expand(int size)
		{
			T[] sourceArray = bufferRef;
			int offset = bufferBlock.offset;
			int size2 = bufferBlock.size;
			int capacity = bufferBlock.capacity;
			poolRef.ExpandBlock(ref this, size);
			int capacity2 = bufferBlock.capacity;
			Array.Copy(sourceArray, offset, bufferRef, bufferBlock.offset, capacity);
			bufferBlock.size = size2;
		}

		private void ReturnBlock()
		{
			if (poolRef != null)
			{
				poolRef.ReturnBlock(ref this);
			}
		}

		public void Init(BufferBlock bb, SmallBufferPool<T> pool)
		{
			ReturnBlock();
			bufferBlock = bb;
			poolRef = pool;
			bufferRef = pool.buffer;
			debugBuffer = new T[bufferBlock.capacity];
		}

		public void Init(BufferBlock bb, SmallBufferPool<T> pool, T[] buffer)
		{
			ReturnBlock();
			bufferBlock = bb;
			poolRef = pool;
			bufferRef = buffer;
			debugBuffer = new T[bufferBlock.capacity];
		}

		public void UnInit()
		{
			poolRef = null;
			bufferRef = null;
			debugBuffer = null;
		}

		public void Add(T value)
		{
			if (bufferRef != null)
			{
				if (bufferBlock.size == bufferBlock.capacity)
				{
					Expand(bufferBlock.capacity * 2);
				}
				this[bufferBlock.size++] = value;
			}
		}

		public bool Remove(T item)
		{
			if (bufferRef != null)
			{
				T value = default(T);
				for (int i = 0; i < bufferBlock.size; i++)
				{
					if (this[i].Equals(item))
					{
						bufferBlock.size--;
						this[i] = value;
						for (int j = i; j < bufferBlock.size; j++)
						{
							this[j] = this[j + 1];
						}
						return true;
					}
				}
			}
			return false;
		}

		public void RemoveAt(int index)
		{
			if (bufferRef != null && index < bufferBlock.size)
			{
				bufferBlock.size--;
				this[index] = default(T);
				for (int i = index; i < bufferBlock.size; i++)
				{
					this[i] = this[i + 1];
				}
			}
		}

		public bool Contains(T item)
		{
			if (bufferRef != null)
			{
				for (int i = 0; i < bufferBlock.size; i++)
				{
					if (this[i].Equals(item))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void Clear()
		{
			bufferBlock.size = 0;
		}

		public void DeepClear()
		{
			if (bufferRef != null)
			{
				Array.Clear(bufferRef, bufferBlock.offset, bufferBlock.capacity);
			}
		}

		public void Copy(T[] src, int startIndex, int desIndex, int count)
		{
			if (bufferRef != null)
			{
				count = ((count < bufferBlock.capacity) ? count : bufferBlock.capacity);
				Array.Copy(src, startIndex, bufferRef, bufferBlock.offset + desIndex, count);
				bufferBlock.size = count;
			}
		}

		public void SetInvalid()
		{
			bufferBlock.blockIndex = -1;
			poolRef = null;
			bufferRef = null;
		}
	}
}
