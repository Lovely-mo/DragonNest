using System;
using XUtliPoolLib;

namespace XMainClient
{
	public class LuaNetNode
	{
		public bool isRpc = false;

		public uint type;

		public uint tagID;

		public bool isOnlyLua = false;

		public byte[] buffer;

		public int length;

		public bool copyBuffer = true;

		public DelLuaRespond resp;

		public DelLuaError err;

		public void SetBuff(byte[] buf, int length)
		{
			if (buf != null && length > 0 && buffer != null)
			{
				for (int i = 0; i < buffer.Length; i++)
				{
					buffer[i] = 0;
				}
				if (buf != null)
				{
					Array.Copy(buf, buffer, length);
				}
				this.length = length;
			}
		}

		public void Reset()
		{
			isRpc = false;
			type = 0u;
			tagID = 0u;
			length = 0;
			resp = null;
			err = null;
		}
	}
}
