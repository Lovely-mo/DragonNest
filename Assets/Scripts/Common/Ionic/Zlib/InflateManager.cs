using System;

namespace Ionic.Zlib
{
	// Token: 0x0200002D RID: 45
	internal sealed class InflateManager
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000C45C File Offset: 0x0000A65C
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000C474 File Offset: 0x0000A674
		internal bool HandleRfc1950HeaderBytes
		{
			get
			{
				return this._handleRfc1950HeaderBytes;
			}
			set
			{
				this._handleRfc1950HeaderBytes = value;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000C47E File Offset: 0x0000A67E
		public InflateManager()
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000C48F File Offset: 0x0000A68F
		public InflateManager(bool expectRfc1950HeaderBytes)
		{
			this._handleRfc1950HeaderBytes = expectRfc1950HeaderBytes;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		public int Reset()
		{
			this._codec.TotalBytesIn = (this._codec.TotalBytesOut = 0L);
			this._codec.Message = null;
			this.mode = (this.HandleRfc1950HeaderBytes ? InflateManager.InflateManagerMode.METHOD : InflateManager.InflateManagerMode.BLOCKS);
			this.blocks.Reset();
			return 0;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000C500 File Offset: 0x0000A700
		internal int End()
		{
			bool flag = this.blocks != null;
			if (flag)
			{
				this.blocks.Free();
			}
			this.blocks = null;
			return 0;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000C534 File Offset: 0x0000A734
		internal int Initialize(ZlibCodec codec, int w)
		{
			this._codec = codec;
			this._codec.Message = null;
			this.blocks = null;
			bool flag = w < 8 || w > 15;
			if (flag)
			{
				this.End();
				throw new ZlibException("Bad window size.");
			}
			this.wbits = w;
			this.blocks = new InflateBlocks(codec, this.HandleRfc1950HeaderBytes ? this : null, 1 << w);
			this.Reset();
			return 0;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		internal int Inflate(FlushType flush)
		{
			bool flag = this._codec.InputBuffer == null;
			if (flag)
			{
				throw new ZlibException("InputBuffer is null. ");
			}
			int num = 0;
			int num2 = -5;
			int nextIn;
			for (;;)
			{
				switch (this.mode)
				{
				case InflateManager.InflateManagerMode.METHOD:
				{
					bool flag2 = this._codec.AvailableBytesIn == 0;
					if (flag2)
					{
						goto Block_3;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					byte[] inputBuffer = this._codec.InputBuffer;
					ZlibCodec codec = this._codec;
					nextIn = codec.NextIn;
					codec.NextIn = nextIn + 1;
					bool flag3 = ((this.method = inputBuffer[nextIn]) & 15) != 8;
					if (flag3)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = string.Format("unknown compression method (0x{0:X2})", this.method);
						this.marker = 5;
						continue;
					}
					bool flag4 = (this.method >> 4) + 8 > this.wbits;
					if (flag4)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = string.Format("invalid window size ({0})", (this.method >> 4) + 8);
						this.marker = 5;
						continue;
					}
					this.mode = InflateManager.InflateManagerMode.FLAG;
					continue;
				}
				case InflateManager.InflateManagerMode.FLAG:
				{
					bool flag5 = this._codec.AvailableBytesIn == 0;
					if (flag5)
					{
						goto Block_6;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					byte[] inputBuffer2 = this._codec.InputBuffer;
					ZlibCodec codec2 = this._codec;
					nextIn = codec2.NextIn;
					codec2.NextIn = nextIn + 1;
					int num3 = inputBuffer2[nextIn] & 255;
					bool flag6 = ((this.method << 8) + num3) % 31 != 0;
					if (flag6)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = "incorrect header check";
						this.marker = 5;
						continue;
					}
					this.mode = (((num3 & 32) == 0) ? InflateManager.InflateManagerMode.BLOCKS : InflateManager.InflateManagerMode.DICT4);
					continue;
				}
				case InflateManager.InflateManagerMode.DICT4:
				{
					bool flag7 = this._codec.AvailableBytesIn == 0;
					if (flag7)
					{
						goto Block_9;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					byte[] inputBuffer3 = this._codec.InputBuffer;
					ZlibCodec codec3 = this._codec;
					nextIn = codec3.NextIn;
					codec3.NextIn = nextIn + 1;
					this.expectedCheck = (uint)(inputBuffer3[nextIn] << 24 & (long)(-16777216));
					this.mode = InflateManager.InflateManagerMode.DICT3;
					continue;
				}
				case InflateManager.InflateManagerMode.DICT3:
				{
					bool flag8 = this._codec.AvailableBytesIn == 0;
					if (flag8)
					{
						goto Block_10;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num4 = this.expectedCheck;
					byte[] inputBuffer4 = this._codec.InputBuffer;
					ZlibCodec codec4 = this._codec;
					nextIn = codec4.NextIn;
					codec4.NextIn = nextIn + 1;
					this.expectedCheck = num4 + (uint)(inputBuffer4[nextIn] << 16 & 16711680U);
					this.mode = InflateManager.InflateManagerMode.DICT2;
					continue;
				}
				case InflateManager.InflateManagerMode.DICT2:
				{
					bool flag9 = this._codec.AvailableBytesIn == 0;
					if (flag9)
					{
						goto Block_11;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num5 = this.expectedCheck;
					byte[] inputBuffer5 = this._codec.InputBuffer;
					ZlibCodec codec5 = this._codec;
					nextIn = codec5.NextIn;
					codec5.NextIn = nextIn + 1;
					this.expectedCheck = num5 + (uint)(inputBuffer5[nextIn] << 8 & 65280U);
					this.mode = InflateManager.InflateManagerMode.DICT1;
					continue;
				}
				case InflateManager.InflateManagerMode.DICT1:
					goto IL_3EB;
				case InflateManager.InflateManagerMode.DICT0:
					goto IL_488;
				case InflateManager.InflateManagerMode.BLOCKS:
				{
					num2 = this.blocks.Process(num2);
					bool flag10 = num2 == -3;
					if (flag10)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this.marker = 0;
						continue;
					}
					bool flag11 = num2 == 0;
					if (flag11)
					{
						num2 = num;
					}
					bool flag12 = num2 != 1;
					if (flag12)
					{
						goto Block_15;
					}
					num2 = num;
					this.computedCheck = this.blocks.Reset();
					bool flag13 = !this.HandleRfc1950HeaderBytes;
					if (flag13)
					{
						goto Block_16;
					}
					this.mode = InflateManager.InflateManagerMode.CHECK4;
					continue;
				}
				case InflateManager.InflateManagerMode.CHECK4:
				{
					bool flag14 = this._codec.AvailableBytesIn == 0;
					if (flag14)
					{
						goto Block_17;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					byte[] inputBuffer6 = this._codec.InputBuffer;
					ZlibCodec codec6 = this._codec;
					nextIn = codec6.NextIn;
					codec6.NextIn = nextIn + 1;
					this.expectedCheck = (uint)(inputBuffer6[nextIn] << 24 & (long)(-16777216));
					this.mode = InflateManager.InflateManagerMode.CHECK3;
					continue;
				}
				case InflateManager.InflateManagerMode.CHECK3:
				{
					bool flag15 = this._codec.AvailableBytesIn == 0;
					if (flag15)
					{
						goto Block_18;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num6 = this.expectedCheck;
					byte[] inputBuffer7 = this._codec.InputBuffer;
					ZlibCodec codec7 = this._codec;
					nextIn = codec7.NextIn;
					codec7.NextIn = nextIn + 1;
					this.expectedCheck = num6 + (uint)(inputBuffer7[nextIn] << 16 & 16711680U);
					this.mode = InflateManager.InflateManagerMode.CHECK2;
					continue;
				}
				case InflateManager.InflateManagerMode.CHECK2:
				{
					bool flag16 = this._codec.AvailableBytesIn == 0;
					if (flag16)
					{
						goto Block_19;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num7 = this.expectedCheck;
					byte[] inputBuffer8 = this._codec.InputBuffer;
					ZlibCodec codec8 = this._codec;
					nextIn = codec8.NextIn;
					codec8.NextIn = nextIn + 1;
					this.expectedCheck = num7 + (uint)(inputBuffer8[nextIn] << 8 & 65280U);
					this.mode = InflateManager.InflateManagerMode.CHECK1;
					continue;
				}
				case InflateManager.InflateManagerMode.CHECK1:
				{
					bool flag17 = this._codec.AvailableBytesIn == 0;
					if (flag17)
					{
						goto Block_20;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num8 = this.expectedCheck;
					byte[] inputBuffer9 = this._codec.InputBuffer;
					ZlibCodec codec9 = this._codec;
					nextIn = codec9.NextIn;
					codec9.NextIn = nextIn + 1;
					this.expectedCheck = num8 + (inputBuffer9[nextIn] & 255U);
					bool flag18 = this.computedCheck != this.expectedCheck;
					if (flag18)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = "incorrect data check";
						this.marker = 5;
						continue;
					}
					goto IL_795;
				}
				case InflateManager.InflateManagerMode.DONE:
					goto IL_7A2;
				case InflateManager.InflateManagerMode.BAD:
					goto IL_7A7;
				}
				break;
			}
			throw new ZlibException("Stream error.");
			Block_3:
			return num2;
			Block_6:
			return num2;
			Block_9:
			return num2;
			Block_10:
			return num2;
			Block_11:
			return num2;
			IL_3EB:
			bool flag19 = this._codec.AvailableBytesIn == 0;
			if (flag19)
			{
				return num2;
			}
			this._codec.AvailableBytesIn--;
			this._codec.TotalBytesIn += 1L;
			uint num9 = this.expectedCheck;
			byte[] inputBuffer10 = this._codec.InputBuffer;
			ZlibCodec codec10 = this._codec;
			nextIn = codec10.NextIn;
			codec10.NextIn = nextIn + 1;
			this.expectedCheck = num9 + (inputBuffer10[nextIn] & 255U);
			this._codec._Adler32 = this.expectedCheck;
			this.mode = InflateManager.InflateManagerMode.DICT0;
			return 2;
			IL_488:
			this.mode = InflateManager.InflateManagerMode.BAD;
			this._codec.Message = "need dictionary";
			this.marker = 0;
			return -2;
			Block_15:
			return num2;
			Block_16:
			this.mode = InflateManager.InflateManagerMode.DONE;
			return 1;
			Block_17:
			return num2;
			Block_18:
			return num2;
			Block_19:
			return num2;
			Block_20:
			return num2;
			IL_795:
			this.mode = InflateManager.InflateManagerMode.DONE;
			return 1;
			IL_7A2:
			return 1;
			IL_7A7:
			throw new ZlibException(string.Format("Bad state ({0})", this._codec.Message));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000CD98 File Offset: 0x0000AF98
		internal int SetDictionary(byte[] dictionary)
		{
			int start = 0;
			int num = dictionary.Length;
			bool flag = this.mode != InflateManager.InflateManagerMode.DICT0;
			if (flag)
			{
				throw new ZlibException("Stream error.");
			}
			bool flag2 = Adler.Adler32(1U, dictionary, 0, dictionary.Length) != this._codec._Adler32;
			int result;
			if (flag2)
			{
				result = -3;
			}
			else
			{
				this._codec._Adler32 = Adler.Adler32(0U, null, 0, 0);
				bool flag3 = num >= 1 << this.wbits;
				if (flag3)
				{
					num = (1 << this.wbits) - 1;
					start = dictionary.Length - num;
				}
				this.blocks.SetDictionary(dictionary, start, num);
				this.mode = InflateManager.InflateManagerMode.BLOCKS;
				result = 0;
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000CE4C File Offset: 0x0000B04C
		internal int Sync()
		{
			bool flag = this.mode != InflateManager.InflateManagerMode.BAD;
			if (flag)
			{
				this.mode = InflateManager.InflateManagerMode.BAD;
				this.marker = 0;
			}
			int num;
			bool flag2 = (num = this._codec.AvailableBytesIn) == 0;
			int result;
			if (flag2)
			{
				result = -5;
			}
			else
			{
				int num2 = this._codec.NextIn;
				int num3 = this.marker;
				while (num != 0 && num3 < 4)
				{
					bool flag3 = this._codec.InputBuffer[num2] == InflateManager.mark[num3];
					if (flag3)
					{
						num3++;
					}
					else
					{
						bool flag4 = this._codec.InputBuffer[num2] > 0;
						if (flag4)
						{
							num3 = 0;
						}
						else
						{
							num3 = 4 - num3;
						}
					}
					num2++;
					num--;
				}
				this._codec.TotalBytesIn += (long)(num2 - this._codec.NextIn);
				this._codec.NextIn = num2;
				this._codec.AvailableBytesIn = num;
				this.marker = num3;
				bool flag5 = num3 != 4;
				if (flag5)
				{
					result = -3;
				}
				else
				{
					long totalBytesIn = this._codec.TotalBytesIn;
					long totalBytesOut = this._codec.TotalBytesOut;
					this.Reset();
					this._codec.TotalBytesIn = totalBytesIn;
					this._codec.TotalBytesOut = totalBytesOut;
					this.mode = InflateManager.InflateManagerMode.BLOCKS;
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		internal int SyncPoint(ZlibCodec z)
		{
			return this.blocks.SyncPoint();
		}

		// Token: 0x04000144 RID: 324
		private const int PRESET_DICT = 32;

		// Token: 0x04000145 RID: 325
		private const int Z_DEFLATED = 8;

		// Token: 0x04000146 RID: 326
		private InflateManager.InflateManagerMode mode;

		// Token: 0x04000147 RID: 327
		internal ZlibCodec _codec;

		// Token: 0x04000148 RID: 328
		internal int method;

		// Token: 0x04000149 RID: 329
		internal uint computedCheck;

		// Token: 0x0400014A RID: 330
		internal uint expectedCheck;

		// Token: 0x0400014B RID: 331
		internal int marker;

		// Token: 0x0400014C RID: 332
		private bool _handleRfc1950HeaderBytes = true;

		// Token: 0x0400014D RID: 333
		internal int wbits;

		// Token: 0x0400014E RID: 334
		internal InflateBlocks blocks;

		// Token: 0x0400014F RID: 335
		private static readonly byte[] mark = new byte[]
		{
			0,
			0,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x02000294 RID: 660
		private enum InflateManagerMode
		{
			// Token: 0x0400085B RID: 2139
			METHOD,
			// Token: 0x0400085C RID: 2140
			FLAG,
			// Token: 0x0400085D RID: 2141
			DICT4,
			// Token: 0x0400085E RID: 2142
			DICT3,
			// Token: 0x0400085F RID: 2143
			DICT2,
			// Token: 0x04000860 RID: 2144
			DICT1,
			// Token: 0x04000861 RID: 2145
			DICT0,
			// Token: 0x04000862 RID: 2146
			BLOCKS,
			// Token: 0x04000863 RID: 2147
			CHECK4,
			// Token: 0x04000864 RID: 2148
			CHECK3,
			// Token: 0x04000865 RID: 2149
			CHECK2,
			// Token: 0x04000866 RID: 2150
			CHECK1,
			// Token: 0x04000867 RID: 2151
			DONE,
			// Token: 0x04000868 RID: 2152
			BAD
		}
	}
}
