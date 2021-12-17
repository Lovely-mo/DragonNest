using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Ionic.Crc;

namespace Ionic.Zlib
{
	// Token: 0x02000030 RID: 48
	public class ParallelDeflateOutputStream : Stream
	{
		// Token: 0x06000169 RID: 361 RVA: 0x0000D96C File Offset: 0x0000BB6C
		public ParallelDeflateOutputStream(Stream stream) : this(stream, CompressionLevel.Default, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000D97A File Offset: 0x0000BB7A
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level) : this(stream, level, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000D988 File Offset: 0x0000BB88
		public ParallelDeflateOutputStream(Stream stream, bool leaveOpen) : this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000D996 File Offset: 0x0000BB96
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, bool leaveOpen) : this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, CompressionStrategy strategy, bool leaveOpen)
		{
			this._outStream = stream;
			this._compressLevel = level;
			this.Strategy = strategy;
			this._leaveOpen = leaveOpen;
			this.MaxBufferPairs = 16;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000DA17 File Offset: 0x0000BC17
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000DA1F File Offset: 0x0000BC1F
		public CompressionStrategy Strategy { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000DA28 File Offset: 0x0000BC28
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000DA40 File Offset: 0x0000BC40
		public int MaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				bool flag = value < 4;
				if (flag)
				{
					throw new ArgumentException("MaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000DA70 File Offset: 0x0000BC70
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000DA88 File Offset: 0x0000BC88
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
			set
			{
				bool flag = value < 1024;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("BufferSize", "BufferSize must be greater than 1024 bytes");
				}
				this._bufferSize = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000DABC File Offset: 0x0000BCBC
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public long BytesProcessed
		{
			get
			{
				return this._totalBytesProcessed;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000DAEC File Offset: 0x0000BCEC
		private void _InitializePoolOfWorkItems()
		{
			this._toWrite = new Queue<int>();
			this._toFill = new Queue<int>();
			this._pool = new List<WorkItem>();
			int num = ParallelDeflateOutputStream.BufferPairsPerCore * Environment.ProcessorCount;
			num = Math.Min(num, this._maxBufferPairs);
			for (int i = 0; i < num; i++)
			{
				this._pool.Add(new WorkItem(this._bufferSize, this._compressLevel, this.Strategy, i));
				this._toFill.Enqueue(i);
			}
			this._newlyCompressedBlob = new AutoResetEvent(false);
			this._runningCrc = new CRC32();
			this._currentlyFilling = -1;
			this._lastFilled = -1;
			this._lastWritten = -1;
			this._latestCompressed = -1;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool mustWait = false;
			bool isClosed = this._isClosed;
			if (isClosed)
			{
				throw new InvalidOperationException();
			}
			bool flag = this._pendingException != null;
			if (flag)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			bool flag2 = count == 0;
			if (!flag2)
			{
				bool flag3 = !this._firstWriteDone;
				if (flag3)
				{
					this._InitializePoolOfWorkItems();
					this._firstWriteDone = true;
				}
				for (;;)
				{
					this.EmitPendingBuffers(false, mustWait);
					mustWait = false;
					bool flag4 = this._currentlyFilling >= 0;
					int num;
					if (flag4)
					{
						num = this._currentlyFilling;
						goto IL_D2;
					}
					bool flag5 = this._toFill.Count == 0;
					if (!flag5)
					{
						num = this._toFill.Dequeue();
						this._lastFilled++;
						goto IL_D2;
					}
					mustWait = true;
					IL_1A9:
					if (count <= 0)
					{
						return;
					}
					continue;
					IL_D2:
					WorkItem workItem = this._pool[num];
					int num2 = (workItem.buffer.Length - workItem.inputBytesAvailable > count) ? count : (workItem.buffer.Length - workItem.inputBytesAvailable);
					workItem.ordinal = this._lastFilled;
					Buffer.BlockCopy(buffer, offset, workItem.buffer, workItem.inputBytesAvailable, num2);
					count -= num2;
					offset += num2;
					workItem.inputBytesAvailable += num2;
					bool flag6 = workItem.inputBytesAvailable == workItem.buffer.Length;
					if (flag6)
					{
						bool flag7 = !ThreadPool.QueueUserWorkItem(new WaitCallback(this._DeflateOne), workItem);
						if (flag7)
						{
							break;
						}
						this._currentlyFilling = -1;
					}
					else
					{
						this._currentlyFilling = num;
					}
					bool flag8 = count > 0;
					if (flag8)
					{
					}
					goto IL_1A9;
				}
				throw new Exception("Cannot enqueue workitem");
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000DD74 File Offset: 0x0000BF74
		private void _FlushFinish()
		{
			byte[] array = new byte[128];
			ZlibCodec zlibCodec = new ZlibCodec();
			int num = zlibCodec.InitializeDeflate(this._compressLevel, false);
			zlibCodec.InputBuffer = null;
			zlibCodec.NextIn = 0;
			zlibCodec.AvailableBytesIn = 0;
			zlibCodec.OutputBuffer = array;
			zlibCodec.NextOut = 0;
			zlibCodec.AvailableBytesOut = array.Length;
			num = zlibCodec.Deflate(FlushType.Finish);
			bool flag = num != 1 && num != 0;
			if (flag)
			{
				throw new Exception("deflating: " + zlibCodec.Message);
			}
			bool flag2 = array.Length - zlibCodec.AvailableBytesOut > 0;
			if (flag2)
			{
				this._outStream.Write(array, 0, array.Length - zlibCodec.AvailableBytesOut);
			}
			zlibCodec.EndDeflate();
			this._Crc32 = this._runningCrc.Crc32Result;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000DE40 File Offset: 0x0000C040
		private void _Flush(bool lastInput)
		{
			bool isClosed = this._isClosed;
			if (isClosed)
			{
				throw new InvalidOperationException();
			}
			bool flag = this.emitting;
			if (!flag)
			{
				bool flag2 = this._currentlyFilling >= 0;
				if (flag2)
				{
					WorkItem wi = this._pool[this._currentlyFilling];
					this._DeflateOne(wi);
					this._currentlyFilling = -1;
				}
				if (lastInput)
				{
					this.EmitPendingBuffers(true, false);
					this._FlushFinish();
				}
				else
				{
					this.EmitPendingBuffers(false, false);
				}
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		public override void Flush()
		{
			bool flag = this._pendingException != null;
			if (flag)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			bool handlingException = this._handlingException;
			if (!handlingException)
			{
				this._Flush(false);
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000DF14 File Offset: 0x0000C114
		public override void Close()
		{
			bool flag = this._pendingException != null;
			if (flag)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			bool handlingException = this._handlingException;
			if (!handlingException)
			{
				bool isClosed = this._isClosed;
				if (!isClosed)
				{
					this._Flush(true);
					bool flag2 = !this._leaveOpen;
					if (flag2)
					{
						this._outStream.Close();
					}
					this._isClosed = true;
				}
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000DF8F File Offset: 0x0000C18F
		public new void Dispose()
		{
			this.Close();
			this._pool = null;
			this.Dispose(true);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		public void Reset(Stream stream)
		{
			bool flag = !this._firstWriteDone;
			if (!flag)
			{
				this._toWrite.Clear();
				this._toFill.Clear();
				foreach (WorkItem workItem in this._pool)
				{
					this._toFill.Enqueue(workItem.index);
					workItem.ordinal = -1;
				}
				this._firstWriteDone = false;
				this._totalBytesProcessed = 0L;
				this._runningCrc = new CRC32();
				this._isClosed = false;
				this._currentlyFilling = -1;
				this._lastFilled = -1;
				this._lastWritten = -1;
				this._latestCompressed = -1;
				this._outStream = stream;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000E08C File Offset: 0x0000C28C
		private void EmitPendingBuffers(bool doAll, bool mustWait)
		{
			bool flag = this.emitting;
			if (!flag)
			{
				this.emitting = true;
				bool flag2 = doAll || mustWait;
				if (flag2)
				{
					this._newlyCompressedBlob.WaitOne();
				}
				do
				{
					int num = -1;
					int num2 = doAll ? 200 : (mustWait ? -1 : 0);
					int num3 = -1;
					for (;;)
					{
						bool flag3 = Monitor.TryEnter(this._toWrite, num2);
						if (flag3)
						{
							num3 = -1;
							try
							{
								bool flag4 = this._toWrite.Count > 0;
								if (flag4)
								{
									num3 = this._toWrite.Dequeue();
								}
							}
							finally
							{
								Monitor.Exit(this._toWrite);
							}
							bool flag5 = num3 >= 0;
							if (flag5)
							{
								WorkItem workItem = this._pool[num3];
								bool flag6 = workItem.ordinal != this._lastWritten + 1;
								if (flag6)
								{
									Queue<int> toWrite = this._toWrite;
									lock (toWrite)
									{
										this._toWrite.Enqueue(num3);
									}
									bool flag7 = num == num3;
									if (flag7)
									{
										this._newlyCompressedBlob.WaitOne();
										num = -1;
									}
									else
									{
										bool flag8 = num == -1;
										if (flag8)
										{
											num = num3;
										}
									}
								}
								else
								{
									num = -1;
									this._outStream.Write(workItem.compressed, 0, workItem.compressedBytesAvailable);
									this._runningCrc.Combine(workItem.crc, workItem.inputBytesAvailable);
									this._totalBytesProcessed += (long)workItem.inputBytesAvailable;
									workItem.inputBytesAvailable = 0;
									this._lastWritten = workItem.ordinal;
									this._toFill.Enqueue(workItem.index);
									bool flag9 = num2 == -1;
									if (flag9)
									{
										num2 = 0;
									}
								}
							}
						}
						else
						{
							num3 = -1;
						}
						IL_1AE:
						if (num3 < 0)
						{
							break;
						}
						continue;
						goto IL_1AE;
					}
				}
				while (doAll && this._lastWritten != this._latestCompressed);
				this.emitting = false;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000E29C File Offset: 0x0000C49C
		private void _DeflateOne(object wi)
		{
			WorkItem workItem = (WorkItem)wi;
			try
			{
				int index = workItem.index;
				CRC32 crc = new CRC32();
				crc.SlurpBlock(workItem.buffer, 0, workItem.inputBytesAvailable);
				this.DeflateOneSegment(workItem);
				workItem.crc = crc.Crc32Result;
				object latestLock = this._latestLock;
				lock (latestLock)
				{
					bool flag = workItem.ordinal > this._latestCompressed;
					if (flag)
					{
						this._latestCompressed = workItem.ordinal;
					}
				}
				Queue<int> toWrite = this._toWrite;
				lock (toWrite)
				{
					this._toWrite.Enqueue(workItem.index);
				}
				this._newlyCompressedBlob.Set();
			}
			catch (Exception pendingException)
			{
				object eLock = this._eLock;
				lock (eLock)
				{
					bool flag2 = this._pendingException != null;
					if (flag2)
					{
						this._pendingException = pendingException;
					}
				}
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		private bool DeflateOneSegment(WorkItem workitem)
		{
			ZlibCodec compressor = workitem.compressor;
			compressor.ResetDeflate(true);
			compressor.NextIn = 0;
			compressor.AvailableBytesIn = workitem.inputBytesAvailable;
			compressor.NextOut = 0;
			compressor.AvailableBytesOut = workitem.compressed.Length;
			do
			{
				compressor.Deflate(FlushType.None);
			}
			while (compressor.AvailableBytesIn > 0 || compressor.AvailableBytesOut == 0);
			int num = compressor.Deflate(FlushType.Sync);
			workitem.compressedBytesAvailable = (int)compressor.TotalBytesOut;
			return true;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000E458 File Offset: 0x0000C658
		[Conditional("Trace")]
		private void TraceOutput(ParallelDeflateOutputStream.TraceBits bits, string format, params object[] varParams)
		{
			bool flag = (bits & this._DesiredTrace) > ParallelDeflateOutputStream.TraceBits.None;
			if (flag)
			{
				object outputLock = this._outputLock;
				lock (outputLock)
				{
					int hashCode = Thread.CurrentThread.GetHashCode();
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		public override bool CanWrite
		{
			get
			{
				return this._outStream.CanWrite;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override long Position
		{
			get
			{
				return this._outStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000171 RID: 369
		private static readonly int IO_BUFFER_SIZE_DEFAULT = 65536;

		// Token: 0x04000172 RID: 370
		private static readonly int BufferPairsPerCore = 4;

		// Token: 0x04000173 RID: 371
		private List<WorkItem> _pool;

		// Token: 0x04000174 RID: 372
		private bool _leaveOpen;

		// Token: 0x04000175 RID: 373
		private bool emitting;

		// Token: 0x04000176 RID: 374
		private Stream _outStream;

		// Token: 0x04000177 RID: 375
		private int _maxBufferPairs;

		// Token: 0x04000178 RID: 376
		private int _bufferSize = ParallelDeflateOutputStream.IO_BUFFER_SIZE_DEFAULT;

		// Token: 0x04000179 RID: 377
		private AutoResetEvent _newlyCompressedBlob;

		// Token: 0x0400017A RID: 378
		private object _outputLock = new object();

		// Token: 0x0400017B RID: 379
		private bool _isClosed;

		// Token: 0x0400017C RID: 380
		private bool _firstWriteDone;

		// Token: 0x0400017D RID: 381
		private int _currentlyFilling;

		// Token: 0x0400017E RID: 382
		private int _lastFilled;

		// Token: 0x0400017F RID: 383
		private int _lastWritten;

		// Token: 0x04000180 RID: 384
		private int _latestCompressed;

		// Token: 0x04000181 RID: 385
		private int _Crc32;

		// Token: 0x04000182 RID: 386
		private CRC32 _runningCrc;

		// Token: 0x04000183 RID: 387
		private object _latestLock = new object();

		// Token: 0x04000184 RID: 388
		private Queue<int> _toWrite;

		// Token: 0x04000185 RID: 389
		private Queue<int> _toFill;

		// Token: 0x04000186 RID: 390
		private long _totalBytesProcessed;

		// Token: 0x04000187 RID: 391
		private CompressionLevel _compressLevel;

		// Token: 0x04000188 RID: 392
		private volatile Exception _pendingException;

		// Token: 0x04000189 RID: 393
		private bool _handlingException;

		// Token: 0x0400018A RID: 394
		private object _eLock = new object();

		// Token: 0x0400018B RID: 395
		private ParallelDeflateOutputStream.TraceBits _DesiredTrace = ParallelDeflateOutputStream.TraceBits.EmitLock | ParallelDeflateOutputStream.TraceBits.EmitEnter | ParallelDeflateOutputStream.TraceBits.EmitBegin | ParallelDeflateOutputStream.TraceBits.EmitDone | ParallelDeflateOutputStream.TraceBits.EmitSkip | ParallelDeflateOutputStream.TraceBits.Session | ParallelDeflateOutputStream.TraceBits.Compress | ParallelDeflateOutputStream.TraceBits.WriteEnter | ParallelDeflateOutputStream.TraceBits.WriteTake;

		// Token: 0x02000295 RID: 661
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x0400086A RID: 2154
			None = 0U,
			// Token: 0x0400086B RID: 2155
			NotUsed1 = 1U,
			// Token: 0x0400086C RID: 2156
			EmitLock = 2U,
			// Token: 0x0400086D RID: 2157
			EmitEnter = 4U,
			// Token: 0x0400086E RID: 2158
			EmitBegin = 8U,
			// Token: 0x0400086F RID: 2159
			EmitDone = 16U,
			// Token: 0x04000870 RID: 2160
			EmitSkip = 32U,
			// Token: 0x04000871 RID: 2161
			EmitAll = 58U,
			// Token: 0x04000872 RID: 2162
			Flush = 64U,
			// Token: 0x04000873 RID: 2163
			Lifecycle = 128U,
			// Token: 0x04000874 RID: 2164
			Session = 256U,
			// Token: 0x04000875 RID: 2165
			Synch = 512U,
			// Token: 0x04000876 RID: 2166
			Instance = 1024U,
			// Token: 0x04000877 RID: 2167
			Compress = 2048U,
			// Token: 0x04000878 RID: 2168
			Write = 4096U,
			// Token: 0x04000879 RID: 2169
			WriteEnter = 8192U,
			// Token: 0x0400087A RID: 2170
			WriteTake = 16384U,
			// Token: 0x0400087B RID: 2171
			All = 4294967295U
		}
	}
}
