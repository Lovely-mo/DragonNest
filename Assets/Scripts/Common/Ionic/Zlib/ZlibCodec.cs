using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x0200003D RID: 61
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000D")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public sealed class ZlibCodec
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
		public int Adler32
		{
			get
			{
				return (int)this._Adler32;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001000C File Offset: 0x0000E20C
		public ZlibCodec()
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001002C File Offset: 0x0000E22C
		public ZlibCodec(CompressionMode mode)
		{
			bool flag = mode == CompressionMode.Compress;
			if (flag)
			{
				int num = this.InitializeDeflate();
				bool flag2 = num != 0;
				if (flag2)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
			}
			else
			{
				bool flag3 = mode == CompressionMode.Decompress;
				if (!flag3)
				{
					throw new ZlibException("Invalid ZlibStreamFlavor.");
				}
				int num2 = this.InitializeInflate();
				bool flag4 = num2 != 0;
				if (flag4)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000100B4 File Offset: 0x0000E2B4
		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000100D4 File Offset: 0x0000E2D4
		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000100F4 File Offset: 0x0000E2F4
		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00010118 File Offset: 0x0000E318
		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			this.WindowBits = windowBits;
			bool flag = this.dstate != null;
			if (flag)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			this.istate = new InflateManager(expectRfc1950Header);
			return this.istate.Initialize(this, windowBits);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00010164 File Offset: 0x0000E364
		public int Inflate(FlushType flush)
		{
			bool flag = this.istate == null;
			if (flag)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0001019C File Offset: 0x0000E39C
		public void Reset()
		{
			bool flag = this.istate == null;
			if (flag)
			{
				throw new ZlibException("No Inflate State!");
			}
			this.istate.Reset();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000101D0 File Offset: 0x0000E3D0
		public int EndInflate()
		{
			bool flag = this.istate == null;
			if (flag)
			{
				throw new ZlibException("No Inflate State!");
			}
			int result = this.istate.End();
			this.istate = null;
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00010210 File Offset: 0x0000E410
		public int SyncInflate()
		{
			bool flag = this.istate == null;
			if (flag)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00010248 File Offset: 0x0000E448
		public int InitializeDeflate()
		{
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00010264 File Offset: 0x0000E464
		public int InitializeDeflate(CompressionLevel level)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00010284 File Offset: 0x0000E484
		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000102A4 File Offset: 0x0000E4A4
		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000102CC File Offset: 0x0000E4CC
		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000102F4 File Offset: 0x0000E4F4
		private int _InternalInitializeDeflate(bool wantRfc1950Header)
		{
			bool flag = this.istate != null;
			if (flag)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			this.dstate = new DeflateManager();
			this.dstate.WantRfc1950HeaderBytes = wantRfc1950Header;
			return this.dstate.Initialize(this, this.CompressLevel, this.WindowBits, this.Strategy);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00010354 File Offset: 0x0000E554
		public int Deflate(FlushType flush)
		{
			bool flag = this.dstate == null;
			if (flag)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.Deflate(flush);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0001038C File Offset: 0x0000E58C
		public int EndDeflate()
		{
			bool flag = this.dstate == null;
			if (flag)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate = null;
			return 0;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000103C0 File Offset: 0x0000E5C0
		public void ResetDeflate(bool setDeflater)
		{
			bool flag = this.dstate == null;
			if (flag)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate.Reset(setDeflater);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000103F4 File Offset: 0x0000E5F4
		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			bool flag = this.dstate == null;
			if (flag)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.SetParams(level, strategy);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0001042C File Offset: 0x0000E62C
		public int SetDictionary(byte[] dictionary)
		{
			bool flag = this.istate != null;
			int result;
			if (flag)
			{
				result = this.istate.SetDictionary(dictionary);
			}
			else
			{
				bool flag2 = this.dstate != null;
				if (!flag2)
				{
					throw new ZlibException("No Inflate or Deflate state!");
				}
				result = this.dstate.SetDictionary(dictionary);
			}
			return result;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00010480 File Offset: 0x0000E680
		internal void flush_pending()
		{
			int num = this.dstate.pendingCount;
			bool flag = num > this.AvailableBytesOut;
			if (flag)
			{
				num = this.AvailableBytesOut;
			}
			bool flag2 = num == 0;
			if (!flag2)
			{
				bool flag3 = this.dstate.pending.Length <= this.dstate.nextPending || this.OutputBuffer.Length <= this.NextOut || this.dstate.pending.Length < this.dstate.nextPending + num || this.OutputBuffer.Length < this.NextOut + num;
				if (flag3)
				{
					throw new ZlibException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", this.dstate.pending.Length, this.dstate.pendingCount));
				}
				Array.Copy(this.dstate.pending, this.dstate.nextPending, this.OutputBuffer, this.NextOut, num);
				this.NextOut += num;
				this.dstate.nextPending += num;
				this.TotalBytesOut += (long)num;
				this.AvailableBytesOut -= num;
				this.dstate.pendingCount -= num;
				bool flag4 = this.dstate.pendingCount == 0;
				if (flag4)
				{
					this.dstate.nextPending = 0;
				}
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000105EC File Offset: 0x0000E7EC
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.AvailableBytesIn;
			bool flag = num > size;
			if (flag)
			{
				num = size;
			}
			bool flag2 = num == 0;
			int result;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				this.AvailableBytesIn -= num;
				bool wantRfc1950HeaderBytes = this.dstate.WantRfc1950HeaderBytes;
				if (wantRfc1950HeaderBytes)
				{
					this._Adler32 = Adler.Adler32(this._Adler32, this.InputBuffer, this.NextIn, num);
				}
				Array.Copy(this.InputBuffer, this.NextIn, buf, start, num);
				this.NextIn += num;
				this.TotalBytesIn += (long)num;
				result = num;
			}
			return result;
		}

		// Token: 0x040001E2 RID: 482
		public byte[] InputBuffer;

		// Token: 0x040001E3 RID: 483
		public int NextIn;

		// Token: 0x040001E4 RID: 484
		public int AvailableBytesIn;

		// Token: 0x040001E5 RID: 485
		public long TotalBytesIn;

		// Token: 0x040001E6 RID: 486
		public byte[] OutputBuffer;

		// Token: 0x040001E7 RID: 487
		public int NextOut;

		// Token: 0x040001E8 RID: 488
		public int AvailableBytesOut;

		// Token: 0x040001E9 RID: 489
		public long TotalBytesOut;

		// Token: 0x040001EA RID: 490
		public string Message;

		// Token: 0x040001EB RID: 491
		internal DeflateManager dstate;

		// Token: 0x040001EC RID: 492
		internal InflateManager istate;

		// Token: 0x040001ED RID: 493
		internal uint _Adler32;

		// Token: 0x040001EE RID: 494
		public CompressionLevel CompressLevel = CompressionLevel.Default;

		// Token: 0x040001EF RID: 495
		public int WindowBits = 15;

		// Token: 0x040001F0 RID: 496
		public CompressionStrategy Strategy = CompressionStrategy.Default;
	}
}
