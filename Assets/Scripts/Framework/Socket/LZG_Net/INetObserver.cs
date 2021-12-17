namespace XMainClient
{
	public interface INetObserver
	{
		void OnConnect(bool bSuccess);

		void OnClosed(NetErrCode nErrCode);

		void OnReceive(uint dwType, int nLen);
	}
}
