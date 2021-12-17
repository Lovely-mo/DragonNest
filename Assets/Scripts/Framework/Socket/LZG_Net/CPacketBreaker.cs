using System;

namespace XMainClient
{
	public class CPacketBreaker : IPacketBreaker
	{
		public int BreakPacket(byte[] data, int index, int len)
		{
			if (len < 4)
			{
				return 0;
			}
			int num = BitConverter.ToInt32(data, index);
			if (len < 4 + num)
			{
				return 0;
			}
			return num + 4;
		}
	}
}
