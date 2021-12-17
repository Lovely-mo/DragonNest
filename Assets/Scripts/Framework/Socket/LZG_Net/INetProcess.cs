namespace XMainClient
{
	public interface INetProcess
	{
		void OnConnect(bool bSuccess);

		void OnClosed(NetErrCode nErrCode);

		void OnPrePropress(NetEvent data);

		void OnProcess(NetEvent data);

		void OnPostProcess(NetEvent data);
	}
}
