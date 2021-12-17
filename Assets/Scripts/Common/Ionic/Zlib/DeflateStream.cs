using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x02000028 RID: 40
	public class DeflateStream : Stream
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00008EAD File Offset: 0x000070AD
		public DeflateStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008EBB File Offset: 0x000070BB
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00008EC9 File Offset: 0x000070C9
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008ED7 File Offset: 0x000070D7
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00008F00 File Offset: 0x00007100
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00008F20 File Offset: 0x00007120
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
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00008F50 File Offset: 0x00007150
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00008F70 File Offset: 0x00007170
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
					throw new ObjectDisposedException("DeflateStream");
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00008FE8 File Offset: 0x000071E8
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00009008 File Offset: 0x00007208
		public CompressionStrategy Strategy
		{
			get
			{
				return this._baseStream.Strategy;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream.Strategy = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00009038 File Offset: 0x00007238
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000905C File Offset: 0x0000725C
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009080 File Offset: 0x00007280
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000090E4 File Offset: 0x000072E4
		public override bool CanRead
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000911C File Offset: 0x0000731C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00009130 File Offset: 0x00007330
		public override bool CanWrite
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009168 File Offset: 0x00007368
		public override void Flush()
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00009197 File Offset: 0x00007397
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000091A0 File Offset: 0x000073A0
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00009197 File Offset: 0x00007397
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009200 File Offset: 0x00007400
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009197 File Offset: 0x00007397
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00009197 File Offset: 0x00007397
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009238 File Offset: 0x00007438
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000926C File Offset: 0x0000746C
		public static byte[] CompressString(string s)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000092B8 File Offset: 0x000074B8
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00009304 File Offset: 0x00007504
		public static string UncompressString(byte[] compressed)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new DeflateStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressString(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00009348 File Offset: 0x00007548
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new DeflateStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressBuffer(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x04000108 RID: 264
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000109 RID: 265
		internal Stream _innerStream;

		// Token: 0x0400010A RID: 266
		private bool _disposed;
	}
}
