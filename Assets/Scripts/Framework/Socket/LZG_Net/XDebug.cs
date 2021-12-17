using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace XUtliPoolLib
{
    public class XDebug : XSingleton<XDebug>
	{
		public enum RecordChannel
		{
			ENone,
			ENetwork,
			EResourceLoad
		}

		public enum RecordLayer
		{
			ELayer0,
			ELayer1,
			ENum
		}

		public class SizeInfo
		{
			public string Name = "";

			public float Size = 0f;

			public int Count = 0;
		}

		public class LayerInfo
		{
			public string Name = "";

			public Dictionary<uint, SizeInfo> SizeInfoMap = new Dictionary<uint, SizeInfo>();

			public void Clear()
			{
				SizeInfoMap.Clear();
			}
		}

		private int _OutputChannels = 0;

		private IPlatform _platform = null;

		private bool _showTimeStick = true;

		private bool _showLog = true;

		private StringBuilder _buffer = new StringBuilder();

		private LayerInfo[] m_LayerInfo = new LayerInfo[2]
		{
			new LayerInfo(),
			new LayerInfo()
		};

		private int m_MaxLayer = 2;

		private bool m_record = false;

		private bool m_recordStart = false;

		private RecordChannel m_RecordChannel = RecordChannel.ENone;

		public XDebug()
		{
			_OutputChannels = 5;
		}

		public void Init(IPlatform platform, XFileLog log)
		{
			//_platform = XSingleton<XUpdater.XUpdater>.singleton.XPlatform;
		}

		public void AddLog(string log1, string log2 = null, string log3 = null, string log4 = null, string log5 = null, string log6 = null, XDebugColor color = XDebugColor.XDebug_None)
		{
			if (_platform != null && !_platform.IsPublish())
			{
				AddLog(XDebugChannel.XDebug_Default, log1, log2, log3, log4, log5, log6, color);
			}
		}

		public void AddLog2(string format, params object[] args)
		{
			if (_showLog && _platform != null && !_platform.IsPublish())
			{
				AddLog(string.Format(format, args));
			}
		}

		public void AddLog(XDebugChannel channel, string log1, string log2 = null, string log3 = null, string log4 = null, string log5 = null, string log6 = null, XDebugColor color = XDebugColor.XDebug_None)
		{
			//if (!_showLog || _platform == null || _platform.IsPublish() || (_OutputChannels & (int)channel) <= 0)
			//{
			//	return;
			//}
			//_buffer.Length = 0;
			//_buffer.Append(log1).Append(log2).Append(log3)
			//	.Append(log4)
			//	.Append(log5)
			//	.Append(log6);
			//if (color == XDebugColor.XDebug_Green)
			//{
			//	_buffer.Insert(0, "<color=green>");
			//	_buffer.Append("</color>");
			//}
			//if (_showTimeStick)
			//{
			//	if (Thread.CurrentThread.ManagedThreadId == XSingleton<XUpdater.XUpdater>.singleton.ManagedThreadId)
			//	{
			//		_buffer.Append(" (at Frame: ").Append(Time.frameCount).Append(" sec: ")
			//			.Append(Time.realtimeSinceStartup.ToString("F3"))
			//			.Append(')');
			//	}
			//	else if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
			//	{
			//		_buffer.Append(" (from anonymous thread").Append(" with id ").Append(Thread.CurrentThread.ManagedThreadId)
			//			.Append(")");
			//	}
			//	else
			//	{
			//		_buffer.Append(" (from thread ").Append(Thread.CurrentThread.Name).Append(" with id ")
			//			.Append(Thread.CurrentThread.ManagedThreadId)
			//			.Append(")");
			//	}
			//}
			//switch (color)
			//{
			//case XDebugColor.XDebug_Red:
			//	Debug.LogError(_buffer);
			//	break;
			//case XDebugColor.XDebug_Yellow:
			//	Debug.LogWarning(_buffer);
			//	break;
			//default:
			//	Debug.Log(_buffer);
			//	break;
			//}
		}

		public void AddGreenLog(string log1, string log2 = null, string log3 = null, string log4 = null, string log5 = null, string log6 = null)
		{
			if (_platform != null && !_platform.IsPublish())
			{
				AddLog(XDebugChannel.XDebug_Default, log1, log2, log3, log4, log5, log6, XDebugColor.XDebug_Green);
			}
		}

		public void AddWarningLog(string log1, string log2 = null, string log3 = null, string log4 = null, string log5 = null, string log6 = null)
		{
			if (_platform != null && !_platform.IsPublish())
			{
				AddLog(XDebugChannel.XDebug_Default, log1, log2, log3, log4, log5, log6, XDebugColor.XDebug_Yellow);
			}
		}

		public void AddWarningLog2(string format, params object[] args)
		{
			if (_showLog && _platform != null && !_platform.IsPublish())
			{
				AddWarningLog(string.Format(format, args));
			}
		}

		public void AddErrorLog2(string format, params object[] args)
		{
			if (_showLog && _platform != null && !_platform.IsPublish())
			{
				AddErrorLog(string.Format(format, args));
			}
		}

		public void AddErrorLog(string log1, string log2 = null, string log3 = null, string log4 = null, string log5 = null, string log6 = null)
		{
			//_buffer.Length = 0;
			//if (Thread.CurrentThread.ManagedThreadId == XSingleton<XUpdater.XUpdater>.singleton.ManagedThreadId)
			//{
			//	_buffer.Append(log1).Append(log2).Append(log3)
			//		.Append(log4)
			//		.Append(log5)
			//		.Append(log6)
			//		.Append(" (at Frame: ")
			//		.Append(Time.frameCount)
			//		.Append(" sec: ")
			//		.Append(Time.realtimeSinceStartup.ToString("F3"))
			//		.Append(')');
			//}
			//else
			//{
			//	_buffer.Append(log1).Append(log2).Append(log3)
			//		.Append(log4)
			//		.Append(log5)
			//		.Append(log6);
			//}
			//XFileLog.AddCustomLog("AddErrorLog:  " + _buffer);
			//AddLog(XDebugChannel.XDebug_Default, log1, log2, log3, log4, log5, log6, XDebugColor.XDebug_Red);
		}

		public override bool Init()
		{
			return true;
		}

		public override void Uninit()
		{
		}

		public void BeginProfile(string title)
		{
		}

		public void RegisterGroupProfile(string title)
		{
		}

		public void BeginGroupProfile(string title)
		{
		}

		public void EndProfile()
		{
		}

		public void EndGroupProfile(string title)
		{
		}

		public void InitProfiler()
		{
		}

		public void PrintProfiler()
		{
		}

		public void StartRecord(RecordChannel channel)
		{
			m_record = true;
			m_recordStart = false;
			m_RecordChannel = channel;
			switch (m_RecordChannel)
			{
			case RecordChannel.ENetwork:
				m_LayerInfo[0].Name = "Recv";
				m_LayerInfo[1].Name = "Send";
				break;
			case RecordChannel.EResourceLoad:
				m_LayerInfo[0].Name = "SharedRes";
				m_LayerInfo[1].Name = "Prefab";
				break;
			}
		}

		public void EndRecord()
		{
			m_record = false;
			m_recordStart = false;
			m_RecordChannel = RecordChannel.ENone;
		}

		public void BeginRecord()
		{
			if (m_record)
			{
				m_recordStart = true;
			}
		}

		public void ClearRecord()
		{
			m_LayerInfo[0].Clear();
			m_LayerInfo[1].Clear();
		}

		public bool EnableRecord()
		{
			return m_recordStart;
		}

		public void AddPoint(uint key, string name, float size, int layer, RecordChannel channel)
		{
			if (m_recordStart && channel == m_RecordChannel && layer >= 0 && layer < m_MaxLayer)
			{
				Dictionary<uint, SizeInfo> sizeInfoMap = m_LayerInfo[layer].SizeInfoMap;
				SizeInfo value = null;
				if (!sizeInfoMap.TryGetValue(key, out value))
				{
					value = new SizeInfo();
					value.Name = name;
					sizeInfoMap.Add(key, value);
				}
				value.Count++;
				value.Size += size;
			}
		}

		public void Print()
		{
			//float num = 0f;
			//for (int i = 0; i < m_MaxLayer; i++)
			//{
			//	XSingleton<XCommon>.singleton.CleanStringCombine();
			//	float num2 = 0f;
			//	SizeInfo sizeInfo = null;
			//	LayerInfo layerInfo = m_LayerInfo[i];
			//	XSingleton<XCommon>.singleton.AppendString(layerInfo.Name, ":\r\n");
			//	Dictionary<uint, SizeInfo>.Enumerator enumerator = layerInfo.SizeInfoMap.GetEnumerator();
			//	while (enumerator.MoveNext())
			//	{
			//		SizeInfo value = enumerator.Current.Value;
			//		XSingleton<XCommon>.singleton.AppendString(string.Format("Name:{0} Count:{1} Size：{2}\r\n", value.Name, value.Count, value.Size));
			//		num += value.Size;
			//		num2 += value.Size;
			//		if (sizeInfo == null || sizeInfo.Size < value.Size)
			//		{
			//			sizeInfo = value;
			//		}
			//	}
			//	XSingleton<XDebug>.singleton.AddWarningLog(XSingleton<XCommon>.singleton.GetString());
			//	if (sizeInfo != null)
			//	{
			//		XSingleton<XDebug>.singleton.AddWarningLog2("max name:{0} Count:{1} Size：{2}", sizeInfo.Name, sizeInfo.Count, sizeInfo.Size);
			//	}
			//	XSingleton<XDebug>.singleton.AddWarningLog2("total {0} size:{1}", layerInfo.Name, num2);
			//}
			//XSingleton<XDebug>.singleton.AddWarningLog("total size:", num.ToString());
		}
	}
}
