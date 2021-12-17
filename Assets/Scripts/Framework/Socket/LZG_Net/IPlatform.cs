using UnityEngine;

namespace XUtliPoolLib
{
	public interface IPlatform : IXInterface
	{
		XPlatformType Platfrom();

		bool IsEdior();

		void SetNoBackupFlag(string fullpath);

		void OnPlatformLogin();

		void OnQQLogin();

		void OnWeChatLogin();

		void OnGuestLogin();

		void LogOut();

		void ResgiterSDONotification(uint serverid, string rolename);

		string GetPFToken();

		string GetVersionServer();

		string GetHostUrl();

		string GetLoginServer(string loginType);

		bool IsPublish();

		bool IsTestMode();

		void SendGameExData(string type, string json);

		void SetPushStatus(bool status);

		string GetHostWithHttpDns(string url);

		bool CheckStatus(string type, string json);

		string GetSDKConfig(string type, string json);

		bool CheckWeChatInstalled();

		string GetChannelID();

		string GetBatteryLevel();

		void SendUserInfo(uint serverID, ulong roleID);

		int GetQualityLevel();

		void MarkLoadlevel(string scene_name);

		void InitApm();

		void MarkLoadlevelCompleted();

		void MarkLevelEnd();

		void SetApmUserID(string userID);

		void SetScreenLightness(int percentage);

		void ResetScreenLightness();

		object CreateClass(EClassType type);

		void ReturnClass(EClassType type, object obj);

		void ClearClass();

		string GetPayBill();

		void Pay(int price, string orderID, string paramID, ulong role, uint serverID);

		void SendExtDara(string key, string param);

		void CreateWXGroup(string param);

		void JoinWXGroup(string param);

		void ShareWithWXGroup(string param);

		void QueryWXGroup(string param);

		string GetMD5(string plainText);

		string UserMd5(string str);

		int GetDensity();

		string GetSim();

		Component AddComponent(GameObject go, EComponentType type);

		void ReloadFMOD();

		INativePlugin GetNativePlugin();
	}
}
