namespace XMainClient
{
	public enum NetErrCode
	{
		Net_NoError,
		Net_SysError,
		Net_ConnectError,
		Net_SrvNtfError,
		Net_ReconnectFailed,
		Net_Rpc_Delay,
		Net_PauseRecv_Overflow,
		Net_RecvBuff_Overflow,
		Net_SendBuff_Overflow,
		Net_Unknown_Exception
	}
}
