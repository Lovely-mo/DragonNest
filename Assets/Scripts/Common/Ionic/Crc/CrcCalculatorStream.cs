using System;
using System.IO;

namespace Ionic.Crc
{
	// Token: 0x02000041 RID: 65
	public class CrcCalculatorStream : Stream, IDisposable
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00011156 File Offset: 0x0000F356
		public CrcCalculatorStream(Stream stream) : this(true, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00011168 File Offset: 0x0000F368
		public CrcCalculatorStream(Stream stream, bool leaveOpen) : this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0001117C File Offset: 0x0000F37C
		public CrcCalculatorStream(Stream stream, long length) : this(true, length, stream, null)
		{
			bool flag = length < 0L;
			if (flag)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000111AC File Offset: 0x0000F3AC
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen) : this(leaveOpen, length, stream, null)
		{
			bool flag = length < 0L;
			if (flag)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000111DC File Offset: 0x0000F3DC
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32) : this(leaveOpen, length, stream, crc32)
		{
			bool flag = length < 0L;
			if (flag)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0001120A File Offset: 0x0000F40A
		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			this._innerStream = stream;
			this._Crc32 = (crc32 ?? new CRC32());
			this._lengthLimit = length;
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00011244 File Offset: 0x0000F444
		public long TotalBytesSlurped
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00011264 File Offset: 0x0000F464
		public int Crc
		{
			get
			{
				return this._Crc32.Crc32Result;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00011284 File Offset: 0x0000F484
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0001129C File Offset: 0x0000F49C
		public bool LeaveOpen
		{
			get
			{
				return this._leaveOpen;
			}
			set
			{
				this._leaveOpen = value;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000112A8 File Offset: 0x0000F4A8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int count2 = count;
			bool flag = this._lengthLimit != CrcCalculatorStream.UnsetLengthLimit;
			if (flag)
			{
				bool flag2 = this._Crc32.TotalBytesRead >= this._lengthLimit;
				if (flag2)
				{
					return 0;
				}
				long num = this._lengthLimit - this._Crc32.TotalBytesRead;
				bool flag3 = num < (long)count;
				if (flag3)
				{
					count2 = (int)num;
				}
			}
			int num2 = this._innerStream.Read(buffer, offset, count2);
			bool flag4 = num2 > 0;
			if (flag4)
			{
				this._Crc32.SlurpBlock(buffer, offset, num2);
			}
			return num2;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00011340 File Offset: 0x0000F540
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = count > 0;
			if (flag)
			{
				this._Crc32.SlurpBlock(buffer, offset, count);
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00011374 File Offset: 0x0000F574
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00011394 File Offset: 0x0000F594
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000113C5 File Offset: 0x0000F5C5
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public override long Length
		{
			get
			{
				bool flag = this._lengthLimit == CrcCalculatorStream.UnsetLengthLimit;
				long result;
				if (flag)
				{
					result = this._innerStream.Length;
				}
				else
				{
					result = this._lengthLimit;
				}
				return result;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0001140C File Offset: 0x0000F60C
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override long Position
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00011429 File Offset: 0x0000F629
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00011434 File Offset: 0x0000F634
		public override void Close()
		{
			base.Close();
			bool flag = !this._leaveOpen;
			if (flag)
			{
				this._innerStream.Close();
			}
		}

		// Token: 0x04000203 RID: 515
		private static readonly long UnsetLengthLimit = -99L;

		// Token: 0x04000204 RID: 516
		internal Stream _innerStream;

		// Token: 0x04000205 RID: 517
		private CRC32 _Crc32;

		// Token: 0x04000206 RID: 518
		private long _lengthLimit = -99L;

		// Token: 0x04000207 RID: 519
		private bool _leaveOpen;
	}
}
