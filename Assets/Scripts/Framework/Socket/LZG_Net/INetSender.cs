namespace XMainClient
{
	public interface INetSender
	{
		bool Send(Protocol protocol);

		bool Send(Rpc rpc);
	}
}
