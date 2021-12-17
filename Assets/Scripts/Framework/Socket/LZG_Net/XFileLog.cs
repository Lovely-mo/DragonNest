using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace XUtliPoolLib
{
    public class XFileLog : MonoBehaviour
	{
		private static Queue<string> CustomLogQueue = new Queue<string>();

		private const int QUEUE_SIZE = 20;

		public static string RoleName = "";

		public static uint RoleLevel = 0u;

		public static int RoleProf = 0;

		public static string ServerID = "";

		public static string OpenID = "";

		public static uint SceneID = 0u;

		private static Application.LogCallback callBack = null;

		private string _outpath;

		public bool _logOpen = true;

		private bool _firstWrite = true;

		private string _guiLog = "";

		private bool _showGuiLog = false;

		private GUIStyle fontStyle = null;

		public static string debugStr = "0";

		public static bool _OpenCustomBtn = false;

		public static bool _debugTrigger = true;

		private Vector2 scrollPosition;

		private bool _showCustomInfo = false;

		public static bool _logBundleOpen = false;

		public static string _customInfo = "";

		public static int _customInfoHeight = 0;

		private void Start()
		{
			_outpath = Application.persistentDataPath + string.Format("/{0}{1}{2}_{3}{4}{5}.log", DateTime.Now.Year.ToString().PadLeft(2, '0'), DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day.ToString().PadLeft(2, '0'), DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'), DateTime.Now.Second.ToString().PadLeft(2, '0'));
			string path = (Application.platform == RuntimePlatform.IPhonePlayer) ? ("/private" + Application.persistentDataPath) : Application.persistentDataPath;
			if (Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				FileInfo[] files = directoryInfo.GetFiles();
				if (files != null)
				{
					for (int i = 0; i < files.Length; i++)
					{
						string a = files[i].Name.Substring(files[i].Name.LastIndexOf(".") + 1);
						if (!(a != "log") && DateTime.Now.Subtract(files[i].CreationTime).TotalDays > 1.0)
						{
							try
							{
								files[i].Delete();
							}
							catch
							{
								XSingleton<XDebug>.singleton.AddErrorLog("Del Log File Error!!!");
							}
						}
					}
				}
			}
			callBack = HandleLog;
			Application.logMessageReceived += callBack;
			XSingleton<XDebug>.singleton.AddLog(_outpath);
		}

		public void HandleLog(string logString, string stackTrace, LogType type)
		{
			if (!_firstWrite)
			{
				return;
			}
			if (_logOpen)
			{
				WriterLog(logString);
			}
			if (type == LogType.Error || type == LogType.Exception)
			{
				_firstWrite = false;
				Log(logString);
				Log(stackTrace);
				string text = string.Format("{0}\n{1}\n", logString, stackTrace);
				while (CustomLogQueue.Count > 0)
				{
					text = string.Format("{0}\n{1}", text, CustomLogQueue.Dequeue());
				}
				SendBuglyReport(text);
				_guiLog = text;
				_showGuiLog = true;
			}
		}

		private void OnGUI()
		{
			//if (_logBundleOpen && XSingleton<XUpdater.XUpdater>.singleton.ABManager != null)
			//{
			//	GUI.TextArea(new Rect(0f, 30f, 100f, 30f), XSingleton<XUpdater.XUpdater>.singleton.ABManager.BundleCount.ToString());
			//}
			//if (fontStyle == null)
			//{
			//	fontStyle = new GUIStyle();
			//}
			//if (_showGuiLog)
			//{
			//	if (GUI.Button(new Rect(0f, 0f, 100f, 30f), "CrashLog"))
			//	{
			//		_showGuiLog = !_showGuiLog;
			//	}
			//	if (_showGuiLog)
			//	{
			//		fontStyle.normal.textColor = new Color(1f, 0f, 0f);
			//		fontStyle.fontSize = 14;
			//		fontStyle.normal.background = Texture2D.whiteTexture;
			//		GUI.TextArea(new Rect(0f, 40f, 1136f, 3200f), _guiLog, fontStyle);
			//	}
			//}
			//if (Input.GetKey(KeyCode.F5))
			//{
			//	_OpenCustomBtn = !_OpenCustomBtn;
			//}
			//if (_OpenCustomBtn && GUI.Button(new Rect(250f, 0f, 150f, 50f), "Info"))
			//{
			//	_showCustomInfo = !_showCustomInfo;
			//}
			//if (_showCustomInfo)
			//{
			//	fontStyle.normal.textColor = new Color(0f, 0f, 0f);
			//	fontStyle.fontSize = 16;
			//	fontStyle.normal.background = Texture2D.whiteTexture;
			//	scrollPosition = GUI.BeginScrollView(new Rect(0f, 30f, 1136f, 640f), scrollPosition, new Rect(0f, 30f, 1136f, _customInfoHeight * (fontStyle.fontSize + 2) + 100));
			//	GUI.Label(new Rect(0f, 30f, 1136f, _customInfoHeight * (fontStyle.fontSize + 2) + 30), _customInfo, fontStyle);
			//	GUI.EndScrollView();
			//}
		}

		public void WriterLog(string logString)
		{
			//using (StreamWriter streamWriter = new StreamWriter(_outpath, true, Encoding.UTF8))
			//{
			//	streamWriter.WriteLine(string.Format("[{0}]{1}", string.Format("{0}/{1}/{2} {3}:{4}:{5}.{6}", DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day.ToString().PadLeft(2, '0'), DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'), DateTime.Now.Second.ToString().PadLeft(2, '0'), DateTime.Now.Millisecond.ToString().PadLeft(3, '0')), logString));
			//	XSingleton<XUpdater.XUpdater>.singleton.XPlatform.SetNoBackupFlag(_outpath);
			//}
		}

		private void Update()
		{
		}

		public void Log(params object[] objs)
		{
			string text = "";
			for (int i = 0; i < objs.Length; i++)
			{
				text = ((i != 0) ? (text + ", " + objs[i].ToString()) : (text + objs[i].ToString()));
			}
			WriterLog(text);
		}

		public void SendBuglyReport(string logstring)
		{
			//if (Application.platform != RuntimePlatform.WindowsEditor && Application.platform != 0)
			//{
			//	IXBuglyMgr iXBuglyMgr = XUpdater.XUpdater.XGameRoot.GetComponent("XBuglyMgr") as IXBuglyMgr;
			//	iXBuglyMgr.ReportCrashToBugly(ServerID, RoleName, RoleLevel, RoleProf, OpenID, XSingleton<XUpdater.XUpdater>.singleton.Version, Time.realtimeSinceStartup.ToString(), "loaded", SceneID.ToString(), logstring);
			//}
		}

		public static void AddCustomLog(string customLog)
		{
			CustomLogQueue.Enqueue(customLog);
			while (CustomLogQueue.Count > 20)
			{
				CustomLogQueue.Dequeue();
			}
		}
	}
}
