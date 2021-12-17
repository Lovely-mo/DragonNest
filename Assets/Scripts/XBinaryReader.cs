using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace XUtliPoolLib
{
	// Token: 0x020000CB RID: 203
	public class XBinaryReader
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x0001973C File Offset: 0x0001793C
		public XBinaryReader()
		{
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			this.m_decoder = utf8Encoding.GetDecoder();
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00019794 File Offset: 0x00017994
		public bool IsEof
		{
			get
			{
				return this.m_Position >= this.m_Length;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000197B8 File Offset: 0x000179B8
		public static void Init()
		{
			StringBuilder sb = XBinaryReader.StringBuilderCache.Acquire(16);
			XBinaryReader.StringBuilderCache.Release(sb);
		}

        // Token: 0x06000599 RID: 1433 RVA: 0x000197D8 File Offset: 0x000179D8
        //public static XBinaryReader Get()
        //{
        //	return CommonObjectPool<XBinaryReader>.Get();
        //}

        // Token: 0x0600059A RID: 1434 RVA: 0x000197F0 File Offset: 0x000179F0
        public static void Return(XBinaryReader reader, bool readShareResource = false)
        {
            bool flag = reader != null;
            if (flag)
            {
                reader.Close(readShareResource);
              //  CommonObjectPool<XBinaryReader>.Release(reader);
            }
        }

        // Token: 0x0600059B RID: 1435 RVA: 0x00019817 File Offset: 0x00017A17
        public void Init(TextAsset ta)
		{
			this.InitByte((ta != null) ? ta.bytes : null, 0, 0);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00019838 File Offset: 0x00017A38
		public void InitByte(byte[] buff, int offset = 0, int count = 0)
		{
			this.m_srcBuff = buff;
			this.m_StartOffset = offset;
			this.m_Position = 0;
			bool flag = count > 0;
			if (flag)
			{
				this.m_Length = ((this.m_srcBuff != null) ? ((count > this.m_srcBuff.Length) ? this.m_srcBuff.Length : count) : 0);
			}
			else
			{
				this.m_Length = ((this.m_srcBuff != null) ? this.m_srcBuff.Length : 0);
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000198AC File Offset: 0x00017AAC
		public byte[] GetBuffer()
		{
			return this.m_srcBuff;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000198C4 File Offset: 0x00017AC4
		public int Seek(int offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.m_Position = ((offset > this.m_Length) ? this.m_Length : offset);
				break;
			case SeekOrigin.Current:
				this.m_Position = ((this.m_Position + offset > this.m_Length) ? this.m_Length : (this.m_Position + offset));
				break;
			case SeekOrigin.End:
				this.m_Position = ((this.m_Length - offset > this.m_Length) ? this.m_Length : (this.m_Length - offset));
				break;
			}
			return this.m_Position;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001995C File Offset: 0x00017B5C
		public void Close(bool readShareResource)
		{
			this.Init(null);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00019968 File Offset: 0x00017B68
		public int GetPosition()
		{
			return this.m_Position;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00019980 File Offset: 0x00017B80
		public char ReadChar()
		{
			int num = 0;
			int num2 = 1;
			int num3 = num3 = 0;
			num3 = this.m_Position;
			bool flag = this.m_charBytes == null;
			if (flag)
			{
				this.m_charBytes = new byte[128];
			}
			bool flag2 = this.m_singleChar == null;
			if (flag2)
			{
				this.m_singleChar = new char[1];
			}
			while (num == 0)
			{
				int num4 = (int)this.ReadByte();
				this.m_charBytes[0] = (byte)num4;
				bool flag3 = num4 == -1;
				if (flag3)
				{
					num2 = 0;
				}
				bool flag4 = num2 == 0;
				if (flag4)
				{
					return '\0';
				}
				try
				{
					num = this.m_decoder.GetChars(this.m_charBytes, 0, num2, this.m_singleChar, 0);
				}
				catch
				{
					this.Seek(num3 - this.m_Position, SeekOrigin.Current);
					throw;
				}
				bool flag5 = num > 1;
				if (flag5)
				{
					XSingleton<XDebug>.singleton.AddErrorLog("XBinaryReader::ReadChar assumes it's reading one bytes only,UTF8.", null, null, null, null, null);
				}
			}
			bool flag6 = num == 0;
			if (flag6)
			{
				return '\0';
			}
			return this.m_singleChar[0];
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00019AA0 File Offset: 0x00017CA0
		public byte ReadByte()
		{
			bool flag = this.m_srcBuff == null || this.m_Position + 1 > this.m_Length;
			byte result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int position = this.m_Position;
				this.m_Position = position + 1;
				int @byte = (int)this.GetByte(position);
				result = (byte)@byte;
			}
			return result;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00019AF0 File Offset: 0x00017CF0
		public int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00019B08 File Offset: 0x00017D08
		public uint ReadUInt32()
		{
			bool flag = this.m_srcBuff == null;
			uint result;
			if (flag)
			{
				result = 0U;
			}
			else
			{
				int num = this.m_Position += 4;
				bool flag2 = num > this.m_Length;
				if (flag2)
				{
					this.m_Position = this.m_Length;
					result = 0U;
				}
				else
				{
					result = (uint)((int)this.GetByte(num - 4) | (int)this.GetByte(num - 3) << 8 | (int)this.GetByte(num - 2) << 16 | (int)this.GetByte(num - 1) << 24);
				}
			}
			return result;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00019B8C File Offset: 0x00017D8C
		public short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00019BA8 File Offset: 0x00017DA8
		public ushort ReadUInt16()
		{
			bool flag = this.m_srcBuff == null;
			ushort result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = this.m_Position += 2;
				bool flag2 = num > this.m_Length;
				if (flag2)
				{
					this.m_Position = this.m_Length;
					result = 0;
				}
				else
				{
					result = (ushort)((int)this.GetByte(num - 2) | (int)this.GetByte(num - 1) << 8);
				}
			}
			return result;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00019C14 File Offset: 0x00017E14
		public unsafe float ReadSingle()
		{
			bool flag = this.m_srcBuff == null;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				int num = this.m_Position += 4;
				bool flag2 = num > this.m_Length;
				if (flag2)
				{
					this.m_Position = this.m_Length;
					result = 0f;
				}
				else
				{
					uint num2 = (uint)((int)this.GetByte(num - 4) | (int)this.GetByte(num - 3) << 8 | (int)this.GetByte(num - 2) << 16 | (int)this.GetByte(num - 1) << 24);
					result = *(float*)(&num2);
				}
			}
			return result;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00019CA8 File Offset: 0x00017EA8
		public unsafe double ReadDouble()
		{
			bool flag = this.m_srcBuff == null;
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				int num = this.m_Position += 8;
				bool flag2 = num > this.m_Length;
				if (flag2)
				{
					this.m_Position = this.m_Length;
					result = 0.0;
				}
				else
				{
					uint num2 = (uint)((int)this.GetByte(num - 8) | (int)this.GetByte(num - 7) << 8 | (int)this.GetByte(num - 6) << 16 | (int)this.GetByte(num - 5) << 24);
					uint num3 = (uint)((int)this.GetByte(num - 4) | (int)this.GetByte(num - 3) << 8 | (int)this.GetByte(num - 2) << 16 | (int)this.GetByte(num - 1) << 24);
					ulong num4 = (ulong)num3 << 32 | (ulong)num2;
					result = *(double*)(&num4);
				}
			}
			return result;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00019D88 File Offset: 0x00017F88
		public long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00019DA0 File Offset: 0x00017FA0
		public ulong ReadUInt64()
		{
			bool flag = this.m_srcBuff == null;
			ulong result;
			if (flag)
			{
				result = 0UL;
			}
			else
			{
				int num = this.m_Position += 8;
				bool flag2 = num > this.m_Length;
				if (flag2)
				{
					this.m_Position = this.m_Length;
					result = 0UL;
				}
				else
				{
					uint num2 = (uint)((int)this.GetByte(num - 8) | (int)this.GetByte(num - 7) << 8 | (int)this.GetByte(num - 6) << 16 | (int)this.GetByte(num - 5) << 24);
					uint num3 = (uint)((int)this.GetByte(num - 4) | (int)this.GetByte(num - 3) << 8 | (int)this.GetByte(num - 2) << 16 | (int)this.GetByte(num - 1) << 24);
					result = ((ulong)num3 << 32 | (ulong)num2);
				}
			}
			return result;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00019E68 File Offset: 0x00018068
		public bool ReadBoolean()
		{
			bool flag = this.m_srcBuff == null || this.m_Position + 1 > this.m_Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int position = this.m_Position;
				this.m_Position = position + 1;
				result = (this.GetByte(position) > 0);
			}
			return result;
		}

        // Token: 0x060005AC RID: 1452 RVA: 0x00019EB8 File Offset: 0x000180B8
        public string ReadString(int length = -1)
        {
            if (this.m_srcBuff == null)
            {
                return string.Empty;
            }
            int num = 0;
            int capacity = (length < 0) ? this.Read7BitEncodedInt() : length;
            if (capacity < 0)
            {
                XSingleton<XDebug>.singleton.AddErrorLog("IO.IO_InvalidStringLen_Len", null, null, null, null, null);
                return string.Empty;
            }
            if (capacity == 0)
            {
                return string.Empty;
            }
            if (this.m_charBytes == null)
            {
                this.m_charBytes = new byte[0x80];
            }
            if (this.m_charBuffer == null)
            {
                this.m_charBuffer = new char[MaxCharsSize];
            }
            StringBuilder sb = null;
            do
            {
                int num3 = ((capacity - num) > 0x80) ? 0x80 : (capacity - num);
                int position = this.m_Position;
                int num6 = this.m_Position += num3;
                if (num6 > this.m_Length)
                {
                    this.m_Position = this.m_Length;
                    num3 = this.m_Position - position;
                }
                position += this.m_StartOffset;
                Array.Copy(this.m_srcBuff, position, this.m_charBytes, 0, num3);
                if (num3 == 0)
                {
                    XSingleton<XDebug>.singleton.AddErrorLog("Read String 0 length", null, null, null, null, null);
                    return string.Empty;
                }
                int num4 = this.m_decoder.GetChars(this.m_charBytes, 0, num3, this.m_charBuffer, 0);
                if ((num == 0) && (num3 == capacity))
                {
                    return new string(this.m_charBuffer, 0, num4);
                }
                if (sb == null)
                {
                    sb = StringBuilderCache.Acquire(capacity);
                }
                sb.Append(this.m_charBuffer, 0, num4);
                num += num3;
            }
            while (num < capacity);
            return StringBuilderCache.GetStringAndRelease(sb);
        }








        // Token: 0x060005AD RID: 1453 RVA: 0x0001A09C File Offset: 0x0001829C
        public void SkipString()
		{
			bool flag = this.m_srcBuff == null;
			if (!flag)
			{
				int num = 0;
				int num2 = this.Read7BitEncodedInt();
				bool flag2 = num2 < 0;
				if (flag2)
				{
					XSingleton<XDebug>.singleton.AddErrorLog("IO.IO_InvalidStringLen_Len", null, null, null, null, null);
				}
				else
				{
					bool flag3 = num2 == 0;
					if (!flag3)
					{
						for (;;)
						{
							int num3 = (num2 - num > 128) ? 128 : (num2 - num);
							int position = this.m_Position;
							int num4 = this.m_Position += num3;
							bool flag4 = num4 > this.m_Length;
							if (flag4)
							{
								this.m_Position = this.m_Length;
								num3 = this.m_Position - position;
							}
							bool flag5 = num3 == 0;
							if (flag5)
							{
								break;
							}
							bool flag6 = num == 0 && num3 <= num2;
							if (flag6)
							{
								goto Block_8;
							}
							num += num3;
							if (num >= num2)
							{
								return;
							}
						}
						XSingleton<XDebug>.singleton.AddErrorLog("Read String 0 length", null, null, null, null, null);
						Block_8:;
					}
				}
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001A1A0 File Offset: 0x000183A0
		private int Read7BitEncodedInt()
		{
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				bool flag = num2 == 35;
				if (flag)
				{
					break;
				}
				byte b = this.ReadByte();
				num |= (int)(b & 127) << num2;
				num2 += 7;
				if ((b & 128) <= 0)
				{
					goto Block_2;
				}
			}
			XSingleton<XDebug>.singleton.AddErrorLog("Format_Bad7BitInt32", null, null, null, null, null);
			return 0;
			Block_2:
			return num;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001A208 File Offset: 0x00018408
		private byte GetByte(int pos)
		{
			return this.m_srcBuff[pos + this.m_StartOffset];
		}

		// Token: 0x040002F1 RID: 753
		private byte[] m_srcBuff = null;

		// Token: 0x040002F2 RID: 754
		private int m_Length = 0;

		// Token: 0x040002F3 RID: 755
		private int m_Position = 0;

		// Token: 0x040002F4 RID: 756
		private int m_StartOffset = 0;

		// Token: 0x040002F5 RID: 757
		private byte[] m_charBytes = null;

		// Token: 0x040002F6 RID: 758
		private char[] m_charBuffer = null;

		// Token: 0x040002F7 RID: 759
		private char[] m_singleChar = null;

		// Token: 0x040002F8 RID: 760
		private const int MaxCharBytesSize = 128;

		// Token: 0x040002F9 RID: 761
		private static int MaxCharsSize = 128;

		// Token: 0x040002FA RID: 762
		private Decoder m_decoder;

		// Token: 0x020002C9 RID: 713
		private static class StringBuilderCache
		{
			// Token: 0x06000E3C RID: 3644 RVA: 0x0004A1FC File Offset: 0x000483FC
			public static StringBuilder Acquire(int capacity = 16)
			{
				bool flag = capacity <= 360;
				if (flag)
				{
					StringBuilder cachedInstance = XBinaryReader.StringBuilderCache.CachedInstance;
					bool flag2 = cachedInstance != null;
					if (flag2)
					{
						bool flag3 = capacity <= cachedInstance.Capacity;
						if (flag3)
						{
							XBinaryReader.StringBuilderCache.CachedInstance = null;
							cachedInstance.Length = 0;
							return cachedInstance;
						}
					}
				}
				return new StringBuilder(capacity);
			}

			// Token: 0x06000E3D RID: 3645 RVA: 0x0004A25C File Offset: 0x0004845C
			public static void Release(StringBuilder sb)
			{
				bool flag = sb.Capacity <= 360;
				if (flag)
				{
					XBinaryReader.StringBuilderCache.CachedInstance = sb;
				}
			}

			// Token: 0x06000E3E RID: 3646 RVA: 0x0004A288 File Offset: 0x00048488
			public static string GetStringAndRelease(StringBuilder sb)
			{
				string result = sb.ToString();
				XBinaryReader.StringBuilderCache.Release(sb);
				return result;
			}

			// Token: 0x040009B5 RID: 2485
			private const int MAX_BUILDER_SIZE = 360;

			// Token: 0x040009B6 RID: 2486
			[ThreadStatic]
			private static StringBuilder CachedInstance;
		}
	}
}
