using System;

namespace Ionic.Zlib
{
	// Token: 0x0200003A RID: 58
	public sealed class Adler
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000EE24 File Offset: 0x0000D024
		public static uint Adler32(uint adler, byte[] buf, int index, int len)
		{
			bool flag = buf == null;
			uint result;
			if (flag)
			{
				result = 1U;
			}
			else
			{
				uint num = adler & 65535U;
				uint num2 = adler >> 16 & 65535U;
				while (len > 0)
				{
					int i = (len < Adler.NMAX) ? len : Adler.NMAX;
					len -= i;
					while (i >= 16)
					{
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						num += (uint)buf[index++];
						num2 += num;
						i -= 16;
					}
					bool flag2 = i != 0;
					if (flag2)
					{
						do
						{
							num += (uint)buf[index++];
							num2 += num;
						}
						while (--i != 0);
					}
					num %= Adler.BASE;
					num2 %= Adler.BASE;
				}
				result = (num2 << 16 | num);
			}
			return result;
		}

		// Token: 0x040001CA RID: 458
		private static readonly uint BASE = 65521U;

		// Token: 0x040001CB RID: 459
		private static readonly int NMAX = 5552;
	}
}
