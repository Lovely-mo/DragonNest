using System;
using XUtliPoolLib;

namespace XMainClient
{
	public class NetEvent : INetEventData, ILuaNetEventData
	{
		#region ¿Ó¬˙Ã√º”
		/// <summary>
		/// lmtº”
		/// </summary>
		public byte[] pbData = null;
		public uint msgType = 0;
		public uint tagID = 0;
		#endregion


		public NetEvtType m_nEvtType;

		public SmallBuffer<byte> m_oBuffer;

		public bool m_bSuccess;

		public NetErrCode m_nErrCode;

		public int m_nBufferLength;

		public int m_SocketID;

		public long m_oTime;

		public bool IsPtc = false;

		public bool IsOnlyLua = false;

		public Protocol protocol
		{
			get;
			set;
		}

		public Rpc rpc
		{
			get;
			set;
		}

		public LuaNetNode node
		{
			get;
			set;
		}

		public NetEvent()
		{
			Reset();
		}

		public void Reset()
		{
			m_nEvtType = NetEvtType.Event_Connect;
			m_oBuffer.SetInvalid();
			m_bSuccess = true;
			m_oTime = DateTime.Now.Ticks;
			m_nErrCode = NetErrCode.Net_NoError;
			m_nBufferLength = 0;
			m_SocketID = 0;
			IsOnlyLua = false;
			protocol = null;
			rpc = null;
			node = null;
			pbData = null;
			msgType = 0;
			tagID = 0;
		}

		public void ManualReturnProtocol()
		{
			protocol = null;
		}
	}
}
