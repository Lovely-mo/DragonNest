using System.Collections.Generic;

namespace XUtliPoolLib
{
	public interface ILuaNetwork : IXInterface
	{
		void InitLua(int rpcCache);

		bool LuaRigsterPtc(uint type, bool copyBuffer);

		void LuaRegistDispacher(List<uint> types);

		void LuaRigsterRPC(uint _type, bool copyBuffer, DelLuaRespond _onRes, DelLuaError _onError);

		bool LuaSendPtc(uint _type, byte[] _reqBuff);

		bool LuaSendRPC(uint _type, byte[] _reqBuff, DelLuaRespond _onRes, DelLuaError _onError);
	}
}
