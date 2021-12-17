namespace XMainClient
{
	public interface ILuaNetProcess
	{
		void OnLuaProcessBuffer(NetEvent evt);

		void OnLuaProcess(NetEvent evt);
	}
}
