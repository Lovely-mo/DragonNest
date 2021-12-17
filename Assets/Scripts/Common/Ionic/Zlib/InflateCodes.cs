using System;

namespace Ionic.Zlib
{
	// Token: 0x0200002C RID: 44
	internal sealed class InflateCodes
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0000B08F File Offset: 0x0000928F
		internal InflateCodes()
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000B0A0 File Offset: 0x000092A0
		internal void Init(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index)
		{
			this.mode = 0;
			this.lbits = (byte)bl;
			this.dbits = (byte)bd;
			this.ltree = tl;
			this.ltree_index = tl_index;
			this.dtree = td;
			this.dtree_index = td_index;
			this.tree = null;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000B0E0 File Offset: 0x000092E0
		internal int Process(InflateBlocks blocks, int r)
		{
			ZlibCodec codec = blocks._codec;
			int num = codec.NextIn;
			int num2 = codec.AvailableBytesIn;
			int num3 = blocks.bitb;
			int i = blocks.bitk;
			int num4 = blocks.writeAt;
			int num5 = (num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4);
			for (;;)
			{
				int num6;
				switch (this.mode)
				{
				case 0:
				{
					bool flag = num5 >= 258 && num2 >= 10;
					if (flag)
					{
						blocks.bitb = num3;
						blocks.bitk = i;
						codec.AvailableBytesIn = num2;
						codec.TotalBytesIn += (long)(num - codec.NextIn);
						codec.NextIn = num;
						blocks.writeAt = num4;
						r = this.InflateFast((int)this.lbits, (int)this.dbits, this.ltree, this.ltree_index, this.dtree, this.dtree_index, blocks, codec);
						num = codec.NextIn;
						num2 = codec.AvailableBytesIn;
						num3 = blocks.bitb;
						i = blocks.bitk;
						num4 = blocks.writeAt;
						num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
						bool flag2 = r != 0;
						if (flag2)
						{
							this.mode = ((r == 1) ? 7 : 9);
							continue;
						}
					}
					this.need = (int)this.lbits;
					this.tree = this.ltree;
					this.tree_index = this.ltree_index;
					this.mode = 1;
					goto IL_1C6;
				}
				case 1:
					goto IL_1C6;
				case 2:
					num6 = this.bitsToGet;
					while (i < num6)
					{
						bool flag3 = num2 != 0;
						if (!flag3)
						{
							goto IL_3D0;
						}
						r = 0;
						num2--;
						num3 |= (int)(codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					this.len += (num3 & InternalInflateConstants.InflateMask[num6]);
					num3 >>= num6;
					i -= num6;
					this.need = (int)this.dbits;
					this.tree = this.dtree;
					this.tree_index = this.dtree_index;
					this.mode = 3;
					goto IL_4AA;
				case 3:
					goto IL_4AA;
				case 4:
					num6 = this.bitsToGet;
					while (i < num6)
					{
						bool flag4 = num2 != 0;
						if (!flag4)
						{
							goto IL_673;
						}
						r = 0;
						num2--;
						num3 |= (int)(codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					this.dist += (num3 & InternalInflateConstants.InflateMask[num6]);
					num3 >>= num6;
					i -= num6;
					this.mode = 5;
					goto IL_729;
				case 5:
					goto IL_729;
				case 6:
				{
					bool flag5 = num5 == 0;
					if (flag5)
					{
						bool flag6 = num4 == blocks.end && blocks.readAt != 0;
						if (flag6)
						{
							num4 = 0;
							num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
						}
						bool flag7 = num5 == 0;
						if (flag7)
						{
							blocks.writeAt = num4;
							r = blocks.Flush(r);
							num4 = blocks.writeAt;
							num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
							bool flag8 = num4 == blocks.end && blocks.readAt != 0;
							if (flag8)
							{
								num4 = 0;
								num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
							}
							bool flag9 = num5 == 0;
							if (flag9)
							{
								goto Block_44;
							}
						}
					}
					r = 0;
					blocks.window[num4++] = (byte)this.lit;
					num5--;
					this.mode = 0;
					continue;
				}
				case 7:
					goto IL_A5A;
				case 8:
					goto IL_B25;
				case 9:
					goto IL_B78;
				}
				break;
				continue;
				IL_1C6:
				num6 = this.need;
				while (i < num6)
				{
					bool flag10 = num2 != 0;
					if (!flag10)
					{
						goto IL_1E3;
					}
					r = 0;
					num2--;
					num3 |= (int)(codec.InputBuffer[num++] & byte.MaxValue) << i;
					i += 8;
				}
				int num7 = (this.tree_index + (num3 & InternalInflateConstants.InflateMask[num6])) * 3;
				num3 >>= this.tree[num7 + 1];
				i -= this.tree[num7 + 1];
				int num8 = this.tree[num7];
				bool flag11 = num8 == 0;
				if (flag11)
				{
					this.lit = this.tree[num7 + 2];
					this.mode = 6;
					continue;
				}
				bool flag12 = (num8 & 16) != 0;
				if (flag12)
				{
					this.bitsToGet = (num8 & 15);
					this.len = this.tree[num7 + 2];
					this.mode = 2;
					continue;
				}
				bool flag13 = (num8 & 64) == 0;
				if (flag13)
				{
					this.need = num8;
					this.tree_index = num7 / 3 + this.tree[num7 + 2];
					continue;
				}
				bool flag14 = (num8 & 32) != 0;
				if (flag14)
				{
					this.mode = 7;
					continue;
				}
				goto IL_34B;
				IL_4AA:
				num6 = this.need;
				while (i < num6)
				{
					bool flag15 = num2 != 0;
					if (!flag15)
					{
						goto IL_4C7;
					}
					r = 0;
					num2--;
					num3 |= (int)(codec.InputBuffer[num++] & byte.MaxValue) << i;
					i += 8;
				}
				num7 = (this.tree_index + (num3 & InternalInflateConstants.InflateMask[num6])) * 3;
				num3 >>= this.tree[num7 + 1];
				i -= this.tree[num7 + 1];
				num8 = this.tree[num7];
				bool flag16 = (num8 & 16) != 0;
				if (flag16)
				{
					this.bitsToGet = (num8 & 15);
					this.dist = this.tree[num7 + 2];
					this.mode = 4;
					continue;
				}
				bool flag17 = (num8 & 64) == 0;
				if (flag17)
				{
					this.need = num8;
					this.tree_index = num7 / 3 + this.tree[num7 + 2];
					continue;
				}
				goto IL_5EE;
				IL_729:
				int j;
				for (j = num4 - this.dist; j < 0; j += blocks.end)
				{
				}
				while (this.len != 0)
				{
					bool flag18 = num5 == 0;
					if (flag18)
					{
						bool flag19 = num4 == blocks.end && blocks.readAt != 0;
						if (flag19)
						{
							num4 = 0;
							num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
						}
						bool flag20 = num5 == 0;
						if (flag20)
						{
							blocks.writeAt = num4;
							r = blocks.Flush(r);
							num4 = blocks.writeAt;
							num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
							bool flag21 = num4 == blocks.end && blocks.readAt != 0;
							if (flag21)
							{
								num4 = 0;
								num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
							}
							bool flag22 = num5 == 0;
							if (flag22)
							{
								goto Block_32;
							}
						}
					}
					blocks.window[num4++] = blocks.window[j++];
					num5--;
					bool flag23 = j == blocks.end;
					if (flag23)
					{
						j = 0;
					}
					this.len--;
				}
				this.mode = 0;
			}
			r = -2;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_1E3:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_34B:
			this.mode = 9;
			codec.Message = "invalid literal/length code";
			r = -3;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_3D0:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_4C7:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_5EE:
			this.mode = 9;
			codec.Message = "invalid distance code";
			r = -3;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_673:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			Block_32:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			Block_44:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_A5A:
			bool flag24 = i > 7;
			if (flag24)
			{
				i -= 8;
				num2++;
				num--;
			}
			blocks.writeAt = num4;
			r = blocks.Flush(r);
			num4 = blocks.writeAt;
			int num9 = (num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4);
			bool flag25 = blocks.readAt != blocks.writeAt;
			if (flag25)
			{
				blocks.bitb = num3;
				blocks.bitk = i;
				codec.AvailableBytesIn = num2;
				codec.TotalBytesIn += (long)(num - codec.NextIn);
				codec.NextIn = num;
				blocks.writeAt = num4;
				return blocks.Flush(r);
			}
			this.mode = 8;
			IL_B25:
			r = 1;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_B78:
			r = -3;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000BD14 File Offset: 0x00009F14
		internal int InflateFast(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, InflateBlocks s, ZlibCodec z)
		{
			int num = z.NextIn;
			int num2 = z.AvailableBytesIn;
			int num3 = s.bitb;
			int i = s.bitk;
			int num4 = s.writeAt;
			int num5 = (num4 < s.readAt) ? (s.readAt - num4 - 1) : (s.end - num4);
			int num6 = InternalInflateConstants.InflateMask[bl];
			int num7 = InternalInflateConstants.InflateMask[bd];
			int num10;
			int num11;
			for (;;)
			{
				while (i < 20)
				{
					num2--;
					num3 |= (int)(z.InputBuffer[num++] & byte.MaxValue) << i;
					i += 8;
				}
				int num8 = num3 & num6;
				int num9 = (tl_index + num8) * 3;
				bool flag = (num10 = tl[num9]) == 0;
				if (flag)
				{
					num3 >>= tl[num9 + 1];
					i -= tl[num9 + 1];
					s.window[num4++] = (byte)tl[num9 + 2];
					num5--;
				}
				else
				{
					for (;;)
					{
						num3 >>= tl[num9 + 1];
						i -= tl[num9 + 1];
						bool flag2 = (num10 & 16) != 0;
						if (flag2)
						{
							goto Block_4;
						}
						bool flag3 = (num10 & 64) == 0;
						if (!flag3)
						{
							goto IL_56B;
						}
						num8 += tl[num9 + 2];
						num8 += (num3 & InternalInflateConstants.InflateMask[num10]);
						num9 = (tl_index + num8) * 3;
						bool flag4 = (num10 = tl[num9]) == 0;
						if (flag4)
						{
							goto Block_20;
						}
					}
					IL_699:
					goto IL_69A;
					Block_20:
					num3 >>= tl[num9 + 1];
					i -= tl[num9 + 1];
					s.window[num4++] = (byte)tl[num9 + 2];
					num5--;
					goto IL_699;
					Block_4:
					num10 &= 15;
					num11 = tl[num9 + 2] + (num3 & InternalInflateConstants.InflateMask[num10]);
					num3 >>= num10;
					for (i -= num10; i < 15; i += 8)
					{
						num2--;
						num3 |= (int)(z.InputBuffer[num++] & byte.MaxValue) << i;
					}
					num8 = (num3 & num7);
					num9 = (td_index + num8) * 3;
					num10 = td[num9];
					for (;;)
					{
						num3 >>= td[num9 + 1];
						i -= td[num9 + 1];
						bool flag5 = (num10 & 16) != 0;
						if (flag5)
						{
							break;
						}
						bool flag6 = (num10 & 64) == 0;
						if (!flag6)
						{
							goto IL_451;
						}
						num8 += td[num9 + 2];
						num8 += (num3 & InternalInflateConstants.InflateMask[num10]);
						num9 = (td_index + num8) * 3;
						num10 = td[num9];
					}
					num10 &= 15;
					while (i < num10)
					{
						num2--;
						num3 |= (int)(z.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					int num12 = td[num9 + 2] + (num3 & InternalInflateConstants.InflateMask[num10]);
					num3 >>= num10;
					i -= num10;
					num5 -= num11;
					bool flag7 = num4 >= num12;
					int num13;
					if (flag7)
					{
						num13 = num4 - num12;
						bool flag8 = num4 - num13 > 0 && 2 > num4 - num13;
						if (flag8)
						{
							s.window[num4++] = s.window[num13++];
							s.window[num4++] = s.window[num13++];
							num11 -= 2;
						}
						else
						{
							Array.Copy(s.window, num13, s.window, num4, 2);
							num4 += 2;
							num13 += 2;
							num11 -= 2;
						}
					}
					else
					{
						num13 = num4 - num12;
						do
						{
							num13 += s.end;
						}
						while (num13 < 0);
						num10 = s.end - num13;
						bool flag9 = num11 > num10;
						if (flag9)
						{
							num11 -= num10;
							bool flag10 = num4 - num13 > 0 && num10 > num4 - num13;
							if (flag10)
							{
								do
								{
									s.window[num4++] = s.window[num13++];
								}
								while (--num10 != 0);
							}
							else
							{
								Array.Copy(s.window, num13, s.window, num4, num10);
								num4 += num10;
								num13 += num10;
							}
							num13 = 0;
						}
					}
					bool flag11 = num4 - num13 > 0 && num11 > num4 - num13;
					if (flag11)
					{
						do
						{
							s.window[num4++] = s.window[num13++];
						}
						while (--num11 != 0);
					}
					else
					{
						Array.Copy(s.window, num13, s.window, num4, num11);
						num4 += num11;
						num13 += num11;
					}
				}
				IL_69A:
				if (num5 < 258 || num2 < 10)
				{
					goto Block_25;
				}
			}
			IL_451:
			z.Message = "invalid distance code";
			num11 = z.AvailableBytesIn - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.AvailableBytesIn = num2;
			z.TotalBytesIn += (long)(num - z.NextIn);
			z.NextIn = num;
			s.writeAt = num4;
			return -3;
			IL_56B:
			bool flag12 = (num10 & 32) != 0;
			if (flag12)
			{
				num11 = z.AvailableBytesIn - num2;
				num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
				num2 += num11;
				num -= num11;
				i -= num11 << 3;
				s.bitb = num3;
				s.bitk = i;
				z.AvailableBytesIn = num2;
				z.TotalBytesIn += (long)(num - z.NextIn);
				z.NextIn = num;
				s.writeAt = num4;
				return 1;
			}
			z.Message = "invalid literal/length code";
			num11 = z.AvailableBytesIn - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.AvailableBytesIn = num2;
			z.TotalBytesIn += (long)(num - z.NextIn);
			z.NextIn = num;
			s.writeAt = num4;
			return -3;
			Block_25:
			num11 = z.AvailableBytesIn - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.AvailableBytesIn = num2;
			z.TotalBytesIn += (long)(num - z.NextIn);
			z.NextIn = num;
			s.writeAt = num4;
			return 0;
		}

		// Token: 0x0400012C RID: 300
		private const int START = 0;

		// Token: 0x0400012D RID: 301
		private const int LEN = 1;

		// Token: 0x0400012E RID: 302
		private const int LENEXT = 2;

		// Token: 0x0400012F RID: 303
		private const int DIST = 3;

		// Token: 0x04000130 RID: 304
		private const int DISTEXT = 4;

		// Token: 0x04000131 RID: 305
		private const int COPY = 5;

		// Token: 0x04000132 RID: 306
		private const int LIT = 6;

		// Token: 0x04000133 RID: 307
		private const int WASH = 7;

		// Token: 0x04000134 RID: 308
		private const int END = 8;

		// Token: 0x04000135 RID: 309
		private const int BADCODE = 9;

		// Token: 0x04000136 RID: 310
		internal int mode;

		// Token: 0x04000137 RID: 311
		internal int len;

		// Token: 0x04000138 RID: 312
		internal int[] tree;

		// Token: 0x04000139 RID: 313
		internal int tree_index = 0;

		// Token: 0x0400013A RID: 314
		internal int need;

		// Token: 0x0400013B RID: 315
		internal int lit;

		// Token: 0x0400013C RID: 316
		internal int bitsToGet;

		// Token: 0x0400013D RID: 317
		internal int dist;

		// Token: 0x0400013E RID: 318
		internal byte lbits;

		// Token: 0x0400013F RID: 319
		internal byte dbits;

		// Token: 0x04000140 RID: 320
		internal int[] ltree;

		// Token: 0x04000141 RID: 321
		internal int ltree_index;

		// Token: 0x04000142 RID: 322
		internal int[] dtree;

		// Token: 0x04000143 RID: 323
		internal int dtree_index;
	}
}
