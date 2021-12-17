using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Crc
{
	// Token: 0x02000040 RID: 64
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000C")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class CRC32
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00010B70 File Offset: 0x0000ED70
		public long TotalBytesRead
		{
			get
			{
				return this._TotalBytesRead;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00010B88 File Offset: 0x0000ED88
		public int Crc32Result
		{
			get
			{
				return (int)(~(int)this._register);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		public int GetCrc32(Stream input)
		{
			return this.GetCrc32AndCopy(input, null);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00010BC0 File Offset: 0x0000EDC0
		public int GetCrc32AndCopy(Stream input, Stream output)
		{
			bool flag = input == null;
			if (flag)
			{
				throw new Exception("The input stream must not be null.");
			}
			byte[] array = new byte[8192];
			int count = 8192;
			this._TotalBytesRead = 0L;
			int i = input.Read(array, 0, count);
			bool flag2 = output != null;
			if (flag2)
			{
				output.Write(array, 0, i);
			}
			this._TotalBytesRead += (long)i;
			while (i > 0)
			{
				this.SlurpBlock(array, 0, i);
				i = input.Read(array, 0, count);
				bool flag3 = output != null;
				if (flag3)
				{
					output.Write(array, 0, i);
				}
				this._TotalBytesRead += (long)i;
			}
			return (int)(~(int)this._register);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00010C7C File Offset: 0x0000EE7C
		public int ComputeCrc32(int W, byte B)
		{
			return this._InternalComputeCrc32((uint)W, B);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00010C98 File Offset: 0x0000EE98
		internal int _InternalComputeCrc32(uint W, byte B)
		{
			return (int)(this.crc32Table[(int)((W ^ (uint)B) & 255U)] ^ W >> 8);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		public void SlurpBlock(byte[] block, int offset, int count)
		{
			bool flag = block == null;
			if (flag)
			{
				throw new Exception("The data buffer must not be null.");
			}
			for (int i = 0; i < count; i++)
			{
				int num = offset + i;
				byte b = block[num];
				bool flag2 = this.reverseBits;
				if (flag2)
				{
					uint num2 = this._register >> 24 ^ (uint)b;
					this._register = (this._register << 8 ^ this.crc32Table[(int)num2]);
				}
				else
				{
					uint num3 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8 ^ this.crc32Table[(int)num3]);
				}
			}
			this._TotalBytesRead += (long)count;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00010D6C File Offset: 0x0000EF6C
		public void UpdateCRC(byte b)
		{
			bool flag = this.reverseBits;
			if (flag)
			{
				uint num = this._register >> 24 ^ (uint)b;
				this._register = (this._register << 8 ^ this.crc32Table[(int)num]);
			}
			else
			{
				uint num2 = (this._register & 255U) ^ (uint)b;
				this._register = (this._register >> 8 ^ this.crc32Table[(int)num2]);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00010DD4 File Offset: 0x0000EFD4
		public void UpdateCRC(byte b, int n)
		{
			while (n-- > 0)
			{
				bool flag = this.reverseBits;
				if (flag)
				{
					uint num = this._register >> 24 ^ (uint)b;
					this._register = (this._register << 8 ^ this.crc32Table[(int)((num >= 0U) ? num : (num + 256U))]);
				}
				else
				{
					uint num2 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8 ^ this.crc32Table[(int)((num2 >= 0U) ? num2 : (num2 + 256U))]);
				}
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00010E6C File Offset: 0x0000F06C
		private static uint ReverseBits(uint data)
		{
			uint num = (data & 1431655765U) << 1 | (data >> 1 & 1431655765U);
			num = ((num & 858993459U) << 2 | (num >> 2 & 858993459U));
			num = ((num & 252645135U) << 4 | (num >> 4 & 252645135U));
			return num << 24 | (num & 65280U) << 8 | (num >> 8 & 65280U) | num >> 24;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00010EDC File Offset: 0x0000F0DC
		private static byte ReverseBits(byte data)
		{
			uint num = (uint)data * 131586U;
			uint num2 = 17055760U;
			uint num3 = num & num2;
			uint num4 = num << 2 & num2 << 1;
			return (byte)(16781313U * (num3 + num4) >> 24);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00010F18 File Offset: 0x0000F118
		private void GenerateLookupTable()
		{
			this.crc32Table = new uint[256];
			byte b = 0;
			do
			{
				uint num = (uint)b;
				for (byte b2 = 8; b2 > 0; b2 -= 1)
				{
					bool flag = (num & 1U) == 1U;
					if (flag)
					{
						num = (num >> 1 ^ this.dwPolynomial);
					}
					else
					{
						num >>= 1;
					}
				}
				bool flag2 = this.reverseBits;
				if (flag2)
				{
					this.crc32Table[(int)CRC32.ReverseBits(b)] = CRC32.ReverseBits(num);
				}
				else
				{
					this.crc32Table[(int)b] = num;
				}
				b += 1;
			}
			while (b > 0);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		private uint gf2_matrix_times(uint[] matrix, uint vec)
		{
			uint num = 0U;
			int num2 = 0;
			while (vec > 0U)
			{
				bool flag = (vec & 1U) == 1U;
				if (flag)
				{
					num ^= matrix[num2];
				}
				vec >>= 1;
				num2++;
			}
			return num;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		private void gf2_matrix_square(uint[] square, uint[] mat)
		{
			for (int i = 0; i < 32; i++)
			{
				square[i] = this.gf2_matrix_times(mat, mat[i]);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0001101C File Offset: 0x0000F21C
		public void Combine(int crc, int length)
		{
			uint[] array = new uint[32];
			uint[] array2 = new uint[32];
			bool flag = length == 0;
			if (!flag)
			{
				uint num = ~this._register;
				array2[0] = this.dwPolynomial;
				uint num2 = 1U;
				for (int i = 1; i < 32; i++)
				{
					array2[i] = num2;
					num2 <<= 1;
				}
				this.gf2_matrix_square(array, array2);
				this.gf2_matrix_square(array2, array);
				uint num3 = (uint)length;
				do
				{
					this.gf2_matrix_square(array, array2);
					bool flag2 = (num3 & 1U) == 1U;
					if (flag2)
					{
						num = this.gf2_matrix_times(array, num);
					}
					num3 >>= 1;
					bool flag3 = num3 == 0U;
					if (flag3)
					{
						break;
					}
					this.gf2_matrix_square(array2, array);
					bool flag4 = (num3 & 1U) == 1U;
					if (flag4)
					{
						num = this.gf2_matrix_times(array2, num);
					}
					num3 >>= 1;
				}
				while (num3 > 0U);
				num ^= (uint)crc;
				this._register = ~num;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0001110B File Offset: 0x0000F30B
		public CRC32() : this(false)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00011116 File Offset: 0x0000F316
		public CRC32(bool reverseBits) : this(-306674912, reverseBits)
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00011126 File Offset: 0x0000F326
		public CRC32(int polynomial, bool reverseBits)
		{
			this.reverseBits = reverseBits;
			this.dwPolynomial = (uint)polynomial;
			this.GenerateLookupTable();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0001114C File Offset: 0x0000F34C
		public void Reset()
		{
			this._register = uint.MaxValue;
		}

		// Token: 0x040001FD RID: 509
		private uint dwPolynomial;

		// Token: 0x040001FE RID: 510
		private long _TotalBytesRead;

		// Token: 0x040001FF RID: 511
		private bool reverseBits;

		// Token: 0x04000200 RID: 512
		private uint[] crc32Table;

		// Token: 0x04000201 RID: 513
		private const int BUFFER_SIZE = 8192;

		// Token: 0x04000202 RID: 514
		private uint _register = uint.MaxValue;
	}
}
