using System;
using System.Collections.Generic;

namespace XUtliPoolLib
{
	internal class XHeap<T> where T : IComparable<T>, IHere
	{
		private List<T> _heap = null;

		private int _heapSize = 0;

		public int HeapSize
		{
			get
			{
				return _heapSize;
			}
		}

		public XHeap()
		{
			_heap = new List<T>();
			_heapSize = 0;
		}

		public void PushHeap(T item)
		{
			int count = _heap.Count;
			if (_heapSize < count)
			{
				_heap[_heapSize] = item;
			}
			else
			{
				_heap.Add(item);
			}
			item.Here = _heapSize;
			HeapifyUp(_heap, _heapSize);
			_heapSize++;
		}

		public T PopHeap()
		{
			T result = default(T);
			if (_heapSize > 0)
			{
				_heapSize--;
				Swap(_heap, 0, _heapSize);
				HeapifyDown(_heap, 0, _heapSize);
				result = _heap[_heapSize];
				result.Here = -1;
				_heap[_heapSize] = default(T);
			}
			return result;
		}

		public void PopHeapAt(int Idx)
		{
			if (_heapSize > 0 && Idx >= 0 && Idx < _heapSize)
			{
				_heapSize--;
				Swap(_heap, Idx, _heapSize);
				if (_heap[_heapSize].CompareTo(_heap[Idx]) < 0)
				{
					HeapifyDown(_heap, Idx, _heapSize);
				}
				else if (_heap[_heapSize].CompareTo(_heap[Idx]) > 0)
				{
					HeapifyUp(_heap, Idx);
				}
				T val = _heap[_heapSize];
				val.Here = -1;
				_heap[_heapSize] = default(T);
			}
		}

		public void Adjust(T item, bool downwords)
		{
			if (_heapSize > 1)
			{
				if (downwords)
				{
					HeapifyDown(_heap, item.Here, _heapSize);
				}
				else
				{
					HeapifyUp(_heap, item.Here);
				}
			}
		}

		public static void MakeHeap(List<T> list)
		{
			for (int num = list.Count / 2 - 1; num >= 0; num--)
			{
				HeapifyDown(list, num, list.Count);
			}
		}

		public static void HeapSort(List<T> list)
		{
			if (list.Count >= 2)
			{
				MakeHeap(list);
				for (int num = list.Count - 1; num > 0; num--)
				{
					Swap(list, 0, num);
					HeapifyDown(list, 0, num);
				}
			}
		}

		public T Peek()
		{
			if (_heapSize > 0)
			{
				return _heap[0];
			}
			return default(T);
		}

		public void Clear()
		{
			_heap.Clear();
			_heapSize = 0;
		}

		private static void HeapifyDown(List<T> heap, int curIdx, int heapSize)
		{
			while (curIdx < heapSize)
			{
				int num = 2 * curIdx + 1;
				int num2 = 2 * curIdx + 2;
				T other = heap[curIdx];
				int num3 = curIdx;
				if (num < heapSize && heap[num].CompareTo(other) < 0)
				{
					other = heap[num];
					num3 = num;
				}
				if (num2 < heapSize && heap[num2].CompareTo(other) < 0)
				{
					other = heap[num2];
					num3 = num2;
				}
				if (num3 != curIdx)
				{
					Swap(heap, curIdx, num3);
					curIdx = num3;
					continue;
				}
				break;
			}
		}

		private static void HeapifyUp(List<T> heap, int curIdx)
		{
			while (curIdx > 0)
			{
				int num = (curIdx - 1) / 2;
				T other = heap[num];
				int num2 = num;
				if (num >= 0 && heap[curIdx].CompareTo(other) < 0)
				{
					num2 = curIdx;
				}
				if (num2 != num)
				{
					Swap(heap, num, num2);
					curIdx = num;
					continue;
				}
				break;
			}
		}

		private static void Swap(List<T> heap, int x, int y)
		{
			T value = heap[x];
			heap[x] = heap[y];
			T val = heap[x];
			val.Here = x;
			heap[y] = value;
			val = heap[y];
			val.Here = y;
		}
	}
}
