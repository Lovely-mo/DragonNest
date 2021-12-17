namespace XMainClient
{
	public interface IPacketBreaker
	{
		int BreakPacket(byte[] data, int index, int len);
	}
}
