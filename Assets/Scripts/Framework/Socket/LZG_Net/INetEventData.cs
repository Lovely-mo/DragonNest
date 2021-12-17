namespace XMainClient
{
	public interface INetEventData
	{
		Protocol protocol
		{
			get;
			set;
		}

		Rpc rpc
		{
			get;
			set;
		}

		void ManualReturnProtocol();
	}
}
