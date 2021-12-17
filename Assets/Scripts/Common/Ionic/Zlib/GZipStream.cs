using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000029 RID: 41
	public class GZipStream : Stream
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000938C File Offset: 0x0000758C
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000093A4 File Offset: 0x000075A4
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._Comment = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000093D0 File Offset: 0x000075D0
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000093E8 File Offset: 0x000075E8
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._FileName = value;
				bool flag = this._FileName == null;
				if (!flag)
				{
					bool flag2 = this._FileName.IndexOf("/") != -1;
					if (flag2)
					{
						this._FileName = this._FileName.Replace("/", "\\");
					}
					bool flag3 = this._FileName.EndsWith("\\");
					if (flag3)
					{
						throw new Exception("Illegal filename");
					}
					bool flag4 = this._FileName.IndexOf("\\") != -1;
					if (flag4)
					{
						this._FileName = Path.GetFileName(this._FileName);
					}
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000094AC File Offset: 0x000076AC
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000094C4 File Offset: 0x000076C4
		public GZipStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000094D2 File Offset: 0x000076D2
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000094E0 File Offset: 0x000076E0
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000094EE File Offset: 0x000076EE
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00009510 File Offset: 0x00007710
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00009530 File Offset: 0x00007730
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
					throw new ObjectDisposedException("GZipStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00009560 File Offset: 0x00007760
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00009580 File Offset: 0x00007780
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
					throw new ObjectDisposedException("GZipStream");
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

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000095F8 File Offset: 0x000077F8
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000961C File Offset: 0x0000781C
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00009640 File Offset: 0x00007840
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
						this._Crc32 = this._baseStream.Crc32;
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000096B8 File Offset: 0x000078B8
		public override bool CanRead
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000096F0 File Offset: 0x000078F0
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00009704 File Offset: 0x00007904
		public override bool CanWrite
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000973C File Offset: 0x0000793C
		public override void Flush()
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00009197 File Offset: 0x00007397
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000976C File Offset: 0x0000796C
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00009197 File Offset: 0x00007397
		public override long Position
		{
			get
			{
				bool flag = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer;
				long result;
				if (flag)
				{
					result = this._baseStream._z.TotalBytesOut + (long)this._headerByteCount;
				}
				else
				{
					bool flag2 = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader;
					if (flag2)
					{
						result = this._baseStream._z.TotalBytesIn + (long)this._baseStream._gzipHeaderByteCount;
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

		// Token: 0x0600013E RID: 318 RVA: 0x000097E0 File Offset: 0x000079E0
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int result = this._baseStream.Read(buffer, offset, count);
			bool flag = !this._firstReadDone;
			if (flag)
			{
				this._firstReadDone = true;
				this.FileName = this._baseStream._GzipFileName;
				this.Comment = this._baseStream._GzipComment;
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009197 File Offset: 0x00007397
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009197 File Offset: 0x00007397
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009854 File Offset: 0x00007A54
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			bool flag = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Undefined;
			if (flag)
			{
				bool wantCompress = this._baseStream._wantCompress;
				if (!wantCompress)
				{
					throw new InvalidOperationException();
				}
				this._headerByteCount = this.EmitHeader();
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000098C0 File Offset: 0x00007AC0
		private int EmitHeader()
		{
			byte[] array = (this.Comment == null) ? null : GZipStream.iso8859dash1.GetBytes(this.Comment);
			byte[] array2 = (this.FileName == null) ? null : GZipStream.iso8859dash1.GetBytes(this.FileName);
			int num = (this.Comment == null) ? 0 : (array.Length + 1);
			int num2 = (this.FileName == null) ? 0 : (array2.Length + 1);
			int num3 = 10 + num + num2;
			byte[] array3 = new byte[num3];
			int num4 = 0;
			array3[num4++] = 31;
			array3[num4++] = 139;
			array3[num4++] = 8;
			byte b = 0;
			bool flag = this.Comment != null;
			if (flag)
			{
				b ^= 16;
			}
			bool flag2 = this.FileName != null;
			if (flag2)
			{
				b ^= 8;
			}
			array3[num4++] = b;
			bool flag3 = this.LastModified == null;
			if (flag3)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			int value = (int)(this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds;
			Array.Copy(BitConverter.GetBytes(value), 0, array3, num4, 4);
			num4 += 4;
			array3[num4++] = 0;
			array3[num4++] = byte.MaxValue;
			bool flag4 = num2 != 0;
			if (flag4)
			{
				Array.Copy(array2, 0, array3, num4, num2 - 1);
				num4 += num2 - 1;
				array3[num4++] = 0;
			}
			bool flag5 = num != 0;
			if (flag5)
			{
				Array.Copy(array, 0, array3, num4, num - 1);
				num4 += num - 1;
				array3[num4++] = 0;
			}
			this._baseStream._stream.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009A94 File Offset: 0x00007C94
		public static byte[] CompressString(string s)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00009AE0 File Offset: 0x00007CE0
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009B2C File Offset: 0x00007D2C
		public static string UncompressString(byte[] compressed)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new GZipStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressString(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009B70 File Offset: 0x00007D70
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new GZipStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressBuffer(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x0400010B RID: 267
		public DateTime? LastModified;

		// Token: 0x0400010C RID: 268
		private int _headerByteCount;

		// Token: 0x0400010D RID: 269
		internal ZlibBaseStream _baseStream;

		// Token: 0x0400010E RID: 270
		private bool _disposed;

		// Token: 0x0400010F RID: 271
		private bool _firstReadDone;

		// Token: 0x04000110 RID: 272
		private string _FileName;

		// Token: 0x04000111 RID: 273
		private string _Comment;

		// Token: 0x04000112 RID: 274
		private int _Crc32;

		// Token: 0x04000113 RID: 275
		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000114 RID: 276
		internal static readonly Encoding iso8859dash1 = Encoding.GetEncoding("iso-8859-1");
	}
}
