using System;

namespace Ionic.Zlib
{
	// Token: 0x0200002A RID: 42
	internal sealed class InflateBlocks
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00009BDC File Offset: 0x00007DDC
		internal InflateBlocks(ZlibCodec codec, object checkfn, int w)
		{
			this._codec = codec;
			this.hufts = new int[4320];
			this.window = new byte[w];
			this.end = w;
			this.checkfn = checkfn;
			this.mode = InflateBlocks.InflateBlockMode.TYPE;
			this.Reset();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009C60 File Offset: 0x00007E60
		internal uint Reset()
		{
			uint result = this.check;
			this.mode = InflateBlocks.InflateBlockMode.TYPE;
			this.bitk = 0;
			this.bitb = 0;
			this.readAt = (this.writeAt = 0);
			bool flag = this.checkfn != null;
			if (flag)
			{
				this._codec._Adler32 = (this.check = Adler.Adler32(0U, null, 0, 0));
			}
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009CCC File Offset: 0x00007ECC
		internal int Process(int r)
		{
			int num = this._codec.NextIn;
			int num2 = this._codec.AvailableBytesIn;
			int num3 = this.bitb;
			int i = this.bitk;
			int num4 = this.writeAt;
			int num5 = (num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4);
			int num6;
			for (;;)
			{
				switch (this.mode)
				{
				case InflateBlocks.InflateBlockMode.TYPE:
					while (i < 3)
					{
						bool flag = num2 != 0;
						if (!flag)
						{
							goto IL_AC;
						}
						r = 0;
						num2--;
						num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					num6 = (num3 & 7);
					this.last = (num6 & 1);
					switch ((uint)num6 >> 1)
					{
					case 0U:
						num3 >>= 3;
						i -= 3;
						num6 = (i & 7);
						num3 >>= num6;
						i -= num6;
						this.mode = InflateBlocks.InflateBlockMode.LENS;
						break;
					case 1U:
					{
						int[] array = new int[1];
						int[] array2 = new int[1];
						int[][] array3 = new int[1][];
						int[][] array4 = new int[1][];
						InfTree.inflate_trees_fixed(array, array2, array3, array4, this._codec);
						this.codes.Init(array[0], array2[0], array3[0], 0, array4[0], 0);
						num3 >>= 3;
						i -= 3;
						this.mode = InflateBlocks.InflateBlockMode.CODES;
						break;
					}
					case 2U:
						num3 >>= 3;
						i -= 3;
						this.mode = InflateBlocks.InflateBlockMode.TABLE;
						break;
					case 3U:
						goto IL_20C;
					}
					continue;
				case InflateBlocks.InflateBlockMode.LENS:
				{
					while (i < 32)
					{
						bool flag2 = num2 != 0;
						if (!flag2)
						{
							goto IL_2AA;
						}
						r = 0;
						num2--;
						num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					bool flag3 = (~num3 >> 16 & 65535) != (num3 & 65535);
					if (flag3)
					{
						goto Block_8;
					}
					this.left = (num3 & 65535);
					i = (num3 = 0);
					this.mode = ((this.left != 0) ? InflateBlocks.InflateBlockMode.STORED : ((this.last != 0) ? InflateBlocks.InflateBlockMode.DRY : InflateBlocks.InflateBlockMode.TYPE));
					continue;
				}
				case InflateBlocks.InflateBlockMode.STORED:
				{
					bool flag4 = num2 == 0;
					if (flag4)
					{
						goto Block_11;
					}
					bool flag5 = num5 == 0;
					if (flag5)
					{
						bool flag6 = num4 == this.end && this.readAt != 0;
						if (flag6)
						{
							num4 = 0;
							num5 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
						}
						bool flag7 = num5 == 0;
						if (flag7)
						{
							this.writeAt = num4;
							r = this.Flush(r);
							num4 = this.writeAt;
							num5 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
							bool flag8 = num4 == this.end && this.readAt != 0;
							if (flag8)
							{
								num4 = 0;
								num5 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
							}
							bool flag9 = num5 == 0;
							if (flag9)
							{
								goto Block_21;
							}
						}
					}
					r = 0;
					num6 = this.left;
					bool flag10 = num6 > num2;
					if (flag10)
					{
						num6 = num2;
					}
					bool flag11 = num6 > num5;
					if (flag11)
					{
						num6 = num5;
					}
					Array.Copy(this._codec.InputBuffer, num, this.window, num4, num6);
					num += num6;
					num2 -= num6;
					num4 += num6;
					num5 -= num6;
					bool flag12 = (this.left -= num6) != 0;
					if (flag12)
					{
						continue;
					}
					this.mode = ((this.last != 0) ? InflateBlocks.InflateBlockMode.DRY : InflateBlocks.InflateBlockMode.TYPE);
					continue;
				}
				case InflateBlocks.InflateBlockMode.TABLE:
				{
					while (i < 14)
					{
						bool flag13 = num2 != 0;
						if (!flag13)
						{
							goto IL_665;
						}
						r = 0;
						num2--;
						num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					num6 = (this.table = (num3 & 16383));
					bool flag14 = (num6 & 31) > 29 || (num6 >> 5 & 31) > 29;
					if (flag14)
					{
						goto Block_29;
					}
					num6 = 258 + (num6 & 31) + (num6 >> 5 & 31);
					bool flag15 = this.blens == null || this.blens.Length < num6;
					if (flag15)
					{
						this.blens = new int[num6];
					}
					else
					{
						Array.Clear(this.blens, 0, num6);
					}
					num3 >>= 14;
					i -= 14;
					this.index = 0;
					this.mode = InflateBlocks.InflateBlockMode.BTREE;
					goto IL_807;
				}
				case InflateBlocks.InflateBlockMode.BTREE:
					goto IL_807;
				case InflateBlocks.InflateBlockMode.DTREE:
					goto IL_A03;
				case InflateBlocks.InflateBlockMode.CODES:
					goto IL_E92;
				case InflateBlocks.InflateBlockMode.DRY:
					goto IL_F85;
				case InflateBlocks.InflateBlockMode.DONE:
					goto IL_103E;
				case InflateBlocks.InflateBlockMode.BAD:
					goto IL_109E;
				}
				break;
				continue;
				IL_E92:
				this.bitb = num3;
				this.bitk = i;
				this._codec.AvailableBytesIn = num2;
				this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
				this._codec.NextIn = num;
				this.writeAt = num4;
				r = this.codes.Process(this, r);
				bool flag16 = r != 1;
				if (flag16)
				{
					goto Block_53;
				}
				r = 0;
				num = this._codec.NextIn;
				num2 = this._codec.AvailableBytesIn;
				num3 = this.bitb;
				i = this.bitk;
				num4 = this.writeAt;
				num5 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
				bool flag17 = this.last == 0;
				if (flag17)
				{
					this.mode = InflateBlocks.InflateBlockMode.TYPE;
					continue;
				}
				goto IL_F7C;
				IL_A03:
				for (;;)
				{
					num6 = this.table;
					bool flag18 = this.index >= 258 + (num6 & 31) + (num6 >> 5 & 31);
					if (flag18)
					{
						break;
					}
					num6 = this.bb[0];
					while (i < num6)
					{
						bool flag19 = num2 != 0;
						if (!flag19)
						{
							goto IL_A59;
						}
						r = 0;
						num2--;
						num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					num6 = this.hufts[(this.tb[0] + (num3 & InternalInflateConstants.InflateMask[num6])) * 3 + 1];
					int num7 = this.hufts[(this.tb[0] + (num3 & InternalInflateConstants.InflateMask[num6])) * 3 + 2];
					bool flag20 = num7 < 16;
					if (flag20)
					{
						num3 >>= num6;
						i -= num6;
						int[] array5 = this.blens;
						int num8 = this.index;
						this.index = num8 + 1;
						array5[num8] = num7;
					}
					else
					{
						int num9 = (num7 == 18) ? 7 : (num7 - 14);
						int num10 = (num7 == 18) ? 11 : 3;
						while (i < num6 + num9)
						{
							bool flag21 = num2 != 0;
							if (!flag21)
							{
								goto IL_B9C;
							}
							r = 0;
							num2--;
							num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
							i += 8;
						}
						num3 >>= num6;
						i -= num6;
						num10 += (num3 & InternalInflateConstants.InflateMask[num9]);
						num3 >>= num9;
						i -= num9;
						num9 = this.index;
						num6 = this.table;
						bool flag22 = num9 + num10 > 258 + (num6 & 31) + (num6 >> 5 & 31) || (num7 == 16 && num9 < 1);
						if (flag22)
						{
							goto Block_48;
						}
						num7 = ((num7 == 16) ? this.blens[num9 - 1] : 0);
						do
						{
							this.blens[num9++] = num7;
						}
						while (--num10 != 0);
						this.index = num9;
					}
				}
				this.tb[0] = -1;
				int[] array6 = new int[]
				{
					9
				};
				int[] array7 = new int[]
				{
					6
				};
				int[] array8 = new int[1];
				int[] array9 = new int[1];
				num6 = this.table;
				num6 = this.inftree.inflate_trees_dynamic(257 + (num6 & 31), 1 + (num6 >> 5 & 31), this.blens, array6, array7, array8, array9, this.hufts, this._codec);
				bool flag23 = num6 != 0;
				if (flag23)
				{
					goto Block_51;
				}
				this.codes.Init(array6[0], array7[0], this.hufts, array8[0], this.hufts, array9[0]);
				this.mode = InflateBlocks.InflateBlockMode.CODES;
				goto IL_E92;
				IL_807:
				while (this.index < 4 + (this.table >> 10))
				{
					while (i < 3)
					{
						bool flag24 = num2 != 0;
						if (!flag24)
						{
							goto IL_825;
						}
						r = 0;
						num2--;
						num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					int[] array10 = this.blens;
					int[] array11 = InflateBlocks.border;
					int num8 = this.index;
					this.index = num8 + 1;
					array10[array11[num8]] = (num3 & 7);
					num3 >>= 3;
					i -= 3;
				}
				while (this.index < 19)
				{
					int[] array12 = this.blens;
					int[] array13 = InflateBlocks.border;
					int num8 = this.index;
					this.index = num8 + 1;
					array12[array13[num8]] = 0;
				}
				this.bb[0] = 7;
				num6 = this.inftree.inflate_trees_bits(this.blens, this.bb, this.tb, this.hufts, this._codec);
				bool flag25 = num6 != 0;
				if (flag25)
				{
					goto Block_36;
				}
				this.index = 0;
				this.mode = InflateBlocks.InflateBlockMode.DTREE;
				goto IL_A03;
			}
			r = -2;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			IL_AC:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			IL_20C:
			num3 >>= 3;
			i -= 3;
			this.mode = InflateBlocks.InflateBlockMode.BAD;
			this._codec.Message = "invalid block type";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			IL_2AA:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			Block_8:
			this.mode = InflateBlocks.InflateBlockMode.BAD;
			this._codec.Message = "invalid stored block lengths";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			Block_11:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			Block_21:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			IL_665:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			Block_29:
			this.mode = InflateBlocks.InflateBlockMode.BAD;
			this._codec.Message = "too many length or distance symbols";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			IL_825:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			Block_36:
			r = num6;
			bool flag26 = r == -3;
			if (flag26)
			{
				this.blens = null;
				this.mode = InflateBlocks.InflateBlockMode.BAD;
			}
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			IL_A59:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			IL_B9C:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			Block_48:
			this.blens = null;
			this.mode = InflateBlocks.InflateBlockMode.BAD;
			this._codec.Message = "invalid bit length repeat";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			Block_51:
			bool flag27 = num6 == -3;
			if (flag27)
			{
				this.blens = null;
				this.mode = InflateBlocks.InflateBlockMode.BAD;
			}
			r = num6;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			Block_53:
			return this.Flush(r);
			IL_F7C:
			this.mode = InflateBlocks.InflateBlockMode.DRY;
			IL_F85:
			this.writeAt = num4;
			r = this.Flush(r);
			num4 = this.writeAt;
			int num11 = (num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4);
			bool flag28 = this.readAt != this.writeAt;
			if (flag28)
			{
				this.bitb = num3;
				this.bitk = i;
				this._codec.AvailableBytesIn = num2;
				this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
				this._codec.NextIn = num;
				this.writeAt = num4;
				return this.Flush(r);
			}
			this.mode = InflateBlocks.InflateBlockMode.DONE;
			IL_103E:
			r = 1;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
			IL_109E:
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000AE3E File Offset: 0x0000903E
		internal void Free()
		{
			this.Reset();
			this.window = null;
			this.hufts = null;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000AE58 File Offset: 0x00009058
		internal void SetDictionary(byte[] d, int start, int n)
		{
			Array.Copy(d, start, this.window, 0, n);
			this.writeAt = n;
			this.readAt = n;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000AE88 File Offset: 0x00009088
		internal int SyncPoint()
		{
			return (this.mode == InflateBlocks.InflateBlockMode.LENS) ? 1 : 0;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000AEA8 File Offset: 0x000090A8
		internal int Flush(int r)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag = i == 0;
				int num;
				if (flag)
				{
					num = ((this.readAt <= this.writeAt) ? this.writeAt : this.end) - this.readAt;
				}
				else
				{
					num = this.writeAt - this.readAt;
				}
				bool flag2 = num == 0;
				if (flag2)
				{
					bool flag3 = r == -5;
					if (flag3)
					{
						r = 0;
					}
					return r;
				}
				bool flag4 = num > this._codec.AvailableBytesOut;
				if (flag4)
				{
					num = this._codec.AvailableBytesOut;
				}
				bool flag5 = num != 0 && r == -5;
				if (flag5)
				{
					r = 0;
				}
				this._codec.AvailableBytesOut -= num;
				this._codec.TotalBytesOut += (long)num;
				bool flag6 = this.checkfn != null;
				if (flag6)
				{
					this._codec._Adler32 = (this.check = Adler.Adler32(this.check, this.window, this.readAt, num));
				}
				Array.Copy(this.window, this.readAt, this._codec.OutputBuffer, this._codec.NextOut, num);
				this._codec.NextOut += num;
				this.readAt += num;
				bool flag7 = this.readAt == this.end && i == 0;
				if (flag7)
				{
					this.readAt = 0;
					bool flag8 = this.writeAt == this.end;
					if (flag8)
					{
						this.writeAt = 0;
					}
				}
				else
				{
					i++;
				}
			}
			return r;
		}

		// Token: 0x04000115 RID: 277
		private const int MANY = 1440;

		// Token: 0x04000116 RID: 278
		internal static readonly int[] border = new int[]
		{
			16,
			17,
			18,
			0,
			8,
			7,
			9,
			6,
			10,
			5,
			11,
			4,
			12,
			3,
			13,
			2,
			14,
			1,
			15
		};

		// Token: 0x04000117 RID: 279
		private InflateBlocks.InflateBlockMode mode;

		// Token: 0x04000118 RID: 280
		internal int left;

		// Token: 0x04000119 RID: 281
		internal int table;

		// Token: 0x0400011A RID: 282
		internal int index;

		// Token: 0x0400011B RID: 283
		internal int[] blens;

		// Token: 0x0400011C RID: 284
		internal int[] bb = new int[1];

		// Token: 0x0400011D RID: 285
		internal int[] tb = new int[1];

		// Token: 0x0400011E RID: 286
		internal InflateCodes codes = new InflateCodes();

		// Token: 0x0400011F RID: 287
		internal int last;

		// Token: 0x04000120 RID: 288
		internal ZlibCodec _codec;

		// Token: 0x04000121 RID: 289
		internal int bitk;

		// Token: 0x04000122 RID: 290
		internal int bitb;

		// Token: 0x04000123 RID: 291
		internal int[] hufts;

		// Token: 0x04000124 RID: 292
		internal byte[] window;

		// Token: 0x04000125 RID: 293
		internal int end;

		// Token: 0x04000126 RID: 294
		internal int readAt;

		// Token: 0x04000127 RID: 295
		internal int writeAt;

		// Token: 0x04000128 RID: 296
		internal object checkfn;

		// Token: 0x04000129 RID: 297
		internal uint check;

		// Token: 0x0400012A RID: 298
		internal InfTree inftree = new InfTree();

		// Token: 0x02000293 RID: 659
		private enum InflateBlockMode
		{
			// Token: 0x04000850 RID: 2128
			TYPE,
			// Token: 0x04000851 RID: 2129
			LENS,
			// Token: 0x04000852 RID: 2130
			STORED,
			// Token: 0x04000853 RID: 2131
			TABLE,
			// Token: 0x04000854 RID: 2132
			BTREE,
			// Token: 0x04000855 RID: 2133
			DTREE,
			// Token: 0x04000856 RID: 2134
			CODES,
			// Token: 0x04000857 RID: 2135
			DRY,
			// Token: 0x04000858 RID: 2136
			DONE,
			// Token: 0x04000859 RID: 2137
			BAD
		}
	}
}
