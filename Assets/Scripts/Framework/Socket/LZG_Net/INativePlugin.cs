namespace XUtliPoolLib
{
	public interface INativePlugin
	{
		void InitializeHooks();

		void CloseHooks();

		void Init();

		void Update();

		void InputData(short tableID, short tableType, byte[] buffer, int length);
	}
}
