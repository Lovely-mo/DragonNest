using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000037 RID: 55
	internal class SharedUtils
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000EC38 File Offset: 0x0000CE38
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000EC50 File Offset: 0x0000CE50
		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			bool flag = target.Length == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				char[] array = new char[target.Length];
				int num = sourceTextReader.Read(array, start, count);
				bool flag2 = num == 0;
				if (flag2)
				{
					result = -1;
				}
				else
				{
					for (int i = start; i < start + num; i++)
					{
						target[i] = (byte)array[i];
					}
					result = num;
				}
			}
			return result;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}
	}
}
