using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x0200003F RID: 63
	public class ZlibStream : Stream
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0001068D File Offset: 0x0000E88D
		public ZlibStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0001069B File Offset: 0x0000E89B
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000106A9 File Offset: 0x0000E8A9
		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000106B7 File Offset: 0x0000E8B7
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000106D8 File Offset: 0x0000E8D8
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x000106F8 File Offset: 0x0000E8F8
		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00010728 File Offset: 0x0000E928
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00010748 File Offset: 0x0000E948
		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				bool flag = this._baseStream._workingBuffer != null;
				if (flag)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				bool flag2 = value < 1024;
				if (flag2)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000107C0 File Offset: 0x0000E9C0
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00010808 File Offset: 0x0000EA08
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = !this._disposed;
				if (flag)
				{
					bool flag2 = disposing && this._baseStream != null;
					if (flag2)
					{
						this._baseStream.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0001086C File Offset: 0x0000EA6C
		public override bool CanRead
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000108A4 File Offset: 0x0000EAA4
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000108B8 File Offset: 0x0000EAB8
		public override bool CanWrite
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public override void Flush()
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00010920 File Offset: 0x0000EB20
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public override long Position
		{
			get
			{
				bool flag = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer;
				long result;
				if (flag)
				{
					result = this._baseStream._z.TotalBytesOut;
				}
				else
				{
					bool flag2 = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader;
					if (flag2)
					{
						result = this._baseStream._z.TotalBytesIn;
					}
					else
					{
						result = 0L;
					}
				}
				return result;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00010980 File Offset: 0x0000EB80
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000109B8 File Offset: 0x0000EBB8
		public override long Seek(long offset, SeekOrigin origin)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Seek(offset, origin);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000109EC File Offset: 0x0000EBEC
		public override void SetLength(long value)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.SetLength(value);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00010A1C File Offset: 0x0000EC1C
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00010A50 File Offset: 0x0000EC50
		public static byte[] CompressString(string s)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00010A9C File Offset: 0x0000EC9C
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00010AE8 File Offset: 0x0000ECE8
		public static string UncompressString(byte[] compressed)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new ZlibStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressString(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00010B2C File Offset: 0x0000ED2C
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new ZlibStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressBuffer(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x040001FB RID: 507
		internal ZlibBaseStream _baseStream;

		// Token: 0x040001FC RID: 508
		private bool _disposed;
	}
}
