using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Crc;

namespace Ionic.Zlib
{
	// Token: 0x0200003C RID: 60
	internal class ZlibBaseStream : Stream
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		internal int Crc32
		{
			get
			{
				bool flag = this.crc == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.crc.Crc32Result;
				}
				return result;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000F020 File Offset: 0x0000D220
		public ZlibBaseStream(Stream stream, CompressionMode compressionMode, CompressionLevel level, ZlibStreamFlavor flavor, bool leaveOpen)
		{
			this._flushMode = FlushType.None;
			this._stream = stream;
			this._leaveOpen = leaveOpen;
			this._compressionMode = compressionMode;
			this._flavor = flavor;
			this._level = level;
			bool flag = flavor == ZlibStreamFlavor.GZIP;
			if (flag)
			{
				this.crc = new CRC32();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		protected internal bool _wantCompress
		{
			get
			{
				return this._compressionMode == CompressionMode.Compress;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		private ZlibCodec z
		{
			get
			{
				bool flag = this._z == null;
				if (flag)
				{
					bool flag2 = this._flavor == ZlibStreamFlavor.ZLIB;
					this._z = new ZlibCodec();
					bool flag3 = this._compressionMode == CompressionMode.Decompress;
					if (flag3)
					{
						this._z.InitializeInflate(flag2);
					}
					else
					{
						this._z.Strategy = this.Strategy;
						this._z.InitializeDeflate(this._level, flag2);
					}
				}
				return this._z;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000F150 File Offset: 0x0000D350
		private byte[] workingBuffer
		{
			get
			{
				bool flag = this._workingBuffer == null;
				if (flag)
				{
					this._workingBuffer = new byte[this._bufferSize];
				}
				return this._workingBuffer;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000F188 File Offset: 0x0000D388
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = this.crc != null;
			if (flag)
			{
				this.crc.SlurpBlock(buffer, offset, count);
			}
			bool flag2 = this._streamMode == ZlibBaseStream.StreamMode.Undefined;
			if (flag2)
			{
				this._streamMode = ZlibBaseStream.StreamMode.Writer;
			}
			else
			{
				bool flag3 = this._streamMode > ZlibBaseStream.StreamMode.Writer;
				if (flag3)
				{
					throw new ZlibException("Cannot Write after Reading.");
				}
			}
			bool flag4 = count == 0;
			if (!flag4)
			{
				this.z.InputBuffer = buffer;
				this._z.Reset();
				this._z.NextIn = offset;
				this._z.AvailableBytesIn = count;
				for (;;)
				{
					this._z.OutputBuffer = this.workingBuffer;
					this._z.NextOut = 0;
					this._z.AvailableBytesOut = this._workingBuffer.Length;
					int num = this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode);
					bool flag5 = num != 0 && num != 1;
					if (flag5)
					{
						break;
					}
					this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
					bool flag6 = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
					bool flag7 = this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress;
					if (flag7)
					{
						flag6 = (this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0);
					}
					if (flag6)
					{
						return;
					}
				}
				throw new ZlibException((this._wantCompress ? "de" : "in") + "flating: " + this._z.Message);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F358 File Offset: 0x0000D558
		private void finish()
		{
			bool flag = this._z == null;
			if (!flag)
			{
				bool flag2 = this._streamMode == ZlibBaseStream.StreamMode.Writer;
				if (flag2)
				{
					int num;
					for (;;)
					{
						this._z.OutputBuffer = this.workingBuffer;
						this._z.NextOut = 0;
						this._z.AvailableBytesOut = this._workingBuffer.Length;
						num = (this._wantCompress ? this._z.Deflate(FlushType.Finish) : this._z.Inflate(FlushType.Finish));
						bool flag3 = num != 1 && num != 0;
						if (flag3)
						{
							break;
						}
						bool flag4 = this._workingBuffer.Length - this._z.AvailableBytesOut > 0;
						if (flag4)
						{
							this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
						}
						bool flag5 = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
						bool flag6 = this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress;
						if (flag6)
						{
							flag5 = (this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0);
						}
						if (flag5)
						{
							goto Block_12;
						}
					}
					string text = (this._wantCompress ? "de" : "in") + "flating";
					bool flag7 = this._z.Message == null;
					if (flag7)
					{
						throw new ZlibException(string.Format("{0}: (rc = {1})", text, num));
					}
					throw new ZlibException(text + ": " + this._z.Message);
					Block_12:
					this.Flush();
					bool flag8 = this._flavor == ZlibStreamFlavor.GZIP;
					if (flag8)
					{
						bool wantCompress = this._wantCompress;
						if (!wantCompress)
						{
							throw new ZlibException("Writing with decompression is not supported.");
						}
						int crc32Result = this.crc.Crc32Result;
						this._stream.Write(BitConverter.GetBytes(crc32Result), 0, 4);
						int value = (int)(this.crc.TotalBytesRead & (long)(-1));
						this._stream.Write(BitConverter.GetBytes(value), 0, 4);
					}
				}
				else
				{
					bool flag9 = this._streamMode == ZlibBaseStream.StreamMode.Reader;
					if (flag9)
					{
						bool flag10 = this._flavor == ZlibStreamFlavor.GZIP;
						if (flag10)
						{
							bool flag11 = !this._wantCompress;
							if (!flag11)
							{
								throw new ZlibException("Reading with compression is not supported.");
							}
							bool flag12 = this._z.TotalBytesOut == 0L;
							if (!flag12)
							{
								byte[] array = new byte[8];
								bool flag13 = this._z.AvailableBytesIn < 8;
								if (flag13)
								{
									Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, this._z.AvailableBytesIn);
									int num2 = 8 - this._z.AvailableBytesIn;
									int num3 = this._stream.Read(array, this._z.AvailableBytesIn, num2);
									bool flag14 = num2 != num3;
									if (flag14)
									{
										throw new ZlibException(string.Format("Missing or incomplete GZIP trailer. Expected 8 bytes, got {0}.", this._z.AvailableBytesIn + num3));
									}
								}
								else
								{
									Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, array.Length);
								}
								int num4 = BitConverter.ToInt32(array, 0);
								int crc32Result2 = this.crc.Crc32Result;
								int num5 = BitConverter.ToInt32(array, 4);
								int num6 = (int)(this._z.TotalBytesOut & (long)(-1));
								bool flag15 = crc32Result2 != num4;
								if (flag15)
								{
									throw new ZlibException(string.Format("Bad CRC32 in GZIP trailer. (actual({0:X8})!=expected({1:X8}))", crc32Result2, num4));
								}
								bool flag16 = num6 != num5;
								if (flag16)
								{
									throw new ZlibException(string.Format("Bad size in GZIP trailer. (actual({0})!=expected({1}))", num6, num5));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000F758 File Offset: 0x0000D958
		private void end()
		{
			bool flag = this.z == null;
			if (!flag)
			{
				bool wantCompress = this._wantCompress;
				if (wantCompress)
				{
					this._z.EndDeflate();
				}
				else
				{
					this._z.EndInflate();
				}
				this._z = null;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		public override void Close()
		{
			bool flag = this._stream == null;
			if (!flag)
			{
				try
				{
					this.finish();
				}
				finally
				{
					this.end();
					bool flag2 = !this._leaveOpen;
					if (flag2)
					{
						this._stream.Close();
					}
					this._stream = null;
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F808 File Offset: 0x0000DA08
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F818 File Offset: 0x0000DA18
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._stream.Seek(offset, origin);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F837 File Offset: 0x0000DA37
		public override void SetLength(long value)
		{
			this._stream.SetLength(value);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000F848 File Offset: 0x0000DA48
		private string ReadZeroTerminatedString()
		{
			List<byte> list = new List<byte>();
			bool flag = false;
			for (;;)
			{
				int num = this._stream.Read(this._buf1, 0, 1);
				bool flag2 = num != 1;
				if (flag2)
				{
					break;
				}
				bool flag3 = this._buf1[0] == 0;
				if (flag3)
				{
					flag = true;
				}
				else
				{
					list.Add(this._buf1[0]);
				}
				if (flag)
				{
					goto Block_3;
				}
			}
			throw new ZlibException("Unexpected EOF reading GZIP header.");
			Block_3:
			byte[] array = list.ToArray();
			return GZipStream.iso8859dash1.GetString(array, 0, array.Length);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		private int _ReadAndValidateGzipHeader()
		{
			int num = 0;
			byte[] array = new byte[10];
			int num2 = this._stream.Read(array, 0, array.Length);
			bool flag = num2 == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = num2 != 10;
				if (flag2)
				{
					throw new ZlibException("Not a valid GZIP stream.");
				}
				bool flag3 = array[0] != 31 || array[1] != 139 || array[2] != 8;
				if (flag3)
				{
					throw new ZlibException("Bad GZIP header.");
				}
				int num3 = BitConverter.ToInt32(array, 4);
				this._GzipMtime = GZipStream._unixEpoch.AddSeconds((double)num3);
				num += num2;
				bool flag4 = (array[3] & 4) == 4;
				if (flag4)
				{
					num2 = this._stream.Read(array, 0, 2);
					num += num2;
					short num4 = (short)((int)array[0] + (int)array[1] * 256);
					byte[] array2 = new byte[(int)num4];
					num2 = this._stream.Read(array2, 0, array2.Length);
					bool flag5 = num2 != (int)num4;
					if (flag5)
					{
						throw new ZlibException("Unexpected end-of-file reading GZIP header.");
					}
					num += num2;
				}
				bool flag6 = (array[3] & 8) == 8;
				if (flag6)
				{
					this._GzipFileName = this.ReadZeroTerminatedString();
				}
				bool flag7 = (array[3] & 16) == 16;
				if (flag7)
				{
					this._GzipComment = this.ReadZeroTerminatedString();
				}
				bool flag8 = (array[3] & 2) == 2;
				if (flag8)
				{
					this.Read(this._buf1, 0, 1);
				}
				result = num;
			}
			return result;
		}

        // Token: 0x060001AF RID: 431 RVA: 0x0000FA44 File Offset: 0x0000DC44
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this._streamMode == StreamMode.Undefined)
            {
                if (!this._stream.CanRead)
                {
                    throw new ZlibException("The stream is not readable.");
                }
                this._streamMode = StreamMode.Reader;
                this.z.AvailableBytesIn = 0;
                if (this._flavor == ZlibStreamFlavor.GZIP)
                {
                    this._gzipHeaderByteCount = this._ReadAndValidateGzipHeader();
                    if (this._gzipHeaderByteCount == 0)
                    {
                        return 0;
                    }
                }
            }
            if (this._streamMode != StreamMode.Reader)
            {
                throw new ZlibException("Cannot Read after Writing.");
            }
            if (count == 0)
            {
                return 0;
            }
            if (this.nomoreinput && this._wantCompress)
            {
                return 0;
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (offset < buffer.GetLowerBound(0))
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if ((offset + count) > buffer.GetLength(0))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            int num = 0;
            this._z.OutputBuffer = buffer;
            this._z.NextOut = offset;
            this._z.AvailableBytesOut = count;
            this._z.InputBuffer = this.workingBuffer;
            do
            {
                if ((this._z.AvailableBytesIn == 0) && !this.nomoreinput)
                {
                    this._z.NextIn = 0;
                    this._z.AvailableBytesIn = this._stream.Read(this._workingBuffer, 0, this._workingBuffer.Length);
                    if (this._z.AvailableBytesIn == 0)
                    {
                        this.nomoreinput = true;
                    }
                }
                num = this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode);
                if (this.nomoreinput && (num == -5))
                {
                    return 0;
                }
                if ((num != 0) && (num != 1))
                {
                    throw new ZlibException(string.Format("{0}flating:  rc={1}  msg={2}", this._wantCompress ? "de" : "in", num, this._z.Message));
                }
            }
            while (((!this.nomoreinput && (num != 1)) || (this._z.AvailableBytesOut != count)) && (((this._z.AvailableBytesOut > 0) && !this.nomoreinput) && (num == 0)));
            if (this._z.AvailableBytesOut > 0)
            {
                if ((num == 0) && (this._z.AvailableBytesIn == 0))
                {
                }
                if (this.nomoreinput && this._wantCompress)
                {
                    num = this._z.Deflate(FlushType.Finish);
                    if ((num != 0) && (num != 1))
                    {
                        throw new ZlibException(string.Format("Deflating:  rc={0}  msg={1}", num, this._z.Message));
                    }
                }
            }
            num = count - this._z.AvailableBytesOut;
            if (this.crc != null)
            {
                this.crc.SlurpBlock(buffer, offset, num);
            }
            return num;
        }




      




        // Token: 0x17000044 RID: 68
        // (get) Token: 0x060001B0 RID: 432 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
        public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000FE00 File Offset: 0x0000E000
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000FE20 File Offset: 0x0000E020
		public override long Length
		{
			get
			{
				return this._stream.Length;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00009197 File Offset: 0x00007397
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00009197 File Offset: 0x00007397
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000FE40 File Offset: 0x0000E040
		public static void CompressString(string s, Stream compressor)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			try
			{
				compressor.Write(bytes, 0, bytes.Length);
			}
			finally
			{
				if (compressor != null)
				{
					((IDisposable)compressor).Dispose();
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000FE88 File Offset: 0x0000E088
		public static void CompressBuffer(byte[] b, Stream compressor)
		{
			try
			{
				compressor.Write(b, 0, b.Length);
			}
			finally
			{
				if (compressor != null)
				{
					((IDisposable)compressor).Dispose();
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		public static string UncompressString(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			Encoding utf = Encoding.UTF8;
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int count;
					while ((count = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, count);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						((IDisposable)decompressor).Dispose();
					}
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				StreamReader streamReader = new StreamReader(memoryStream, utf);
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000FF6C File Offset: 0x0000E16C
		public static byte[] UncompressBuffer(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int count;
					while ((count = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, count);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						((IDisposable)decompressor).Dispose();
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x040001D0 RID: 464
		protected internal ZlibCodec _z = null;

		// Token: 0x040001D1 RID: 465
		protected internal ZlibBaseStream.StreamMode _streamMode = ZlibBaseStream.StreamMode.Undefined;

		// Token: 0x040001D2 RID: 466
		protected internal FlushType _flushMode;

		// Token: 0x040001D3 RID: 467
		protected internal ZlibStreamFlavor _flavor;

		// Token: 0x040001D4 RID: 468
		protected internal CompressionMode _compressionMode;

		// Token: 0x040001D5 RID: 469
		protected internal CompressionLevel _level;

		// Token: 0x040001D6 RID: 470
		protected internal bool _leaveOpen;

		// Token: 0x040001D7 RID: 471
		protected internal byte[] _workingBuffer;

		// Token: 0x040001D8 RID: 472
		protected internal int _bufferSize = 16384;

		// Token: 0x040001D9 RID: 473
		protected internal byte[] _buf1 = new byte[1];

		// Token: 0x040001DA RID: 474
		protected internal Stream _stream;

		// Token: 0x040001DB RID: 475
		protected internal CompressionStrategy Strategy = CompressionStrategy.Default;

		// Token: 0x040001DC RID: 476
		private CRC32 crc;

		// Token: 0x040001DD RID: 477
		protected internal string _GzipFileName;

		// Token: 0x040001DE RID: 478
		protected internal string _GzipComment;

		// Token: 0x040001DF RID: 479
		protected internal DateTime _GzipMtime;

		// Token: 0x040001E0 RID: 480
		protected internal int _gzipHeaderByteCount;

		// Token: 0x040001E1 RID: 481
		private bool nomoreinput = false;

		// Token: 0x02000296 RID: 662
		internal enum StreamMode
		{
			// Token: 0x0400087D RID: 2173
			Writer,
			// Token: 0x0400087E RID: 2174
			Reader,
			// Token: 0x0400087F RID: 2175
			Undefined
		}
	}
}
