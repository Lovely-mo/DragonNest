using System.Collections.Generic;
using UnityEngine;

//     ┌────────────────┐
//     │┌┐  ┌┐  ┌┐                      │
//     │└┘  └┘  └┘                      │
//     │┌──────────────┐│
//     ││                                    ││     
//     ││       ScrollView            ││
//     ││                                    ││
//     │└──────────────┘│
//     │┌──────────────┐│
//     │└──────────────┘│
//     └────────────────┘
public class ConsoleLogOutput : MonoBehaviour
{
    /// <summary>
    /// log信息
    /// </summary>
    struct LogMsg
    {
        public string condition;
        public string stackTrace;
        public LogType type;
        public LogMsg(string condition, string stackTrace, LogType type)
        {
            //因为GUIButton字符串长度有限制（button宽度限制）,对显示的log信息长度做截取
            if (condition.Length > 100)
                this.condition = condition.Substring(0, 100);
            else
                this.condition = condition;
            this.stackTrace = condition + "\n" + stackTrace;
            this.type = type;
        }
    }
    /// <summary>
    /// 控制台是否是打开的
    /// </summary>
    bool isDrawGui = false;
    /// <summary>
    /// 全部log信息列表
    /// </summary>
    List<LogMsg> _logAllMsg;
    List<LogMsg> logAllMsg
    {
        get
        {
            if (_logAllMsg == null)
            {
                _logAllMsg = new List<LogMsg>();
            }
            return _logAllMsg;
        }
    }
    /// <summary>
    /// log信息列表
    /// </summary>
    List<LogMsg> _logMsg;
    List<LogMsg> logMsg
    {
        get
        {
            if (_logMsg == null)
            {
                _logMsg = new List<LogMsg>();
            }
            return _logMsg;
        }
    }
    /// <summary>
    /// 报错log信息列表
    /// </summary>
    List<LogMsg> _logErrMsg;
    List<LogMsg> logErrMsg
    {
        get
        {
            if (_logErrMsg == null)
            {
                _logErrMsg = new List<LogMsg>();
            }
            return _logErrMsg;
        }
    }
    /// <summary>
    /// 当前显示的log信息列表
    /// </summary>
    List<LogMsg> _logShowMsg;
    List<LogMsg> logShowMsg
    {
        set { _logShowMsg = value; }
        get
        {
            if (_logShowMsg == null)
            {
                _logShowMsg = new List<LogMsg>();
            }
            return _logShowMsg;
        }
    }


    #region 窗体区域
    //窗体区域坐标
    static Vector2 windowPos = new Vector2(Screen.width * 0.3f, 0);
    //窗体区域大小
    static Vector2 windowSize = new Vector2(Screen.width * 0.7f, Screen.height * 0.8f);
    //窗体
    Rect WindowBackgroundRect = new Rect(windowPos.x, windowPos.y, windowSize.x, windowSize.y);
    string WindowBackgroundName = "控制台";
    #endregion

    #region 按钮区域
    //按钮区域坐标
    static Vector2 btnPos = new Vector2(10, 25);
    //按钮区域大小
    static Vector2 btnSize = new Vector2(100, 20);
    //按钮排列坐标偏差值
    static Vector2 btnPosValue = new Vector2(110, 0);
    //按钮清理Log
    Rect clearBtnRect = new Rect(btnPos.x, btnPos.y, btnSize.x, btnSize.y);
    string clearBtnName = "清理Log";
    //按钮全部Log
    Rect allLogBtnRect = new Rect(btnPos.x + btnPosValue.x, btnPos.y + btnPosValue.y, btnSize.x, btnSize.y);
    string allLogBtnName = "全部Log";
    //按钮打印Log
    Rect logBtnRect = new Rect(btnPos.x + (btnPosValue.x * 2), btnPos.y + (btnPosValue.y * 2), btnSize.x, btnSize.y);
    string logBtnName = "打印Log";
    //按钮报错Log
    Rect logErrBtnRect = new Rect(btnPos.x + (btnPosValue.x * 3), btnPos.y + (btnPosValue.y * 3), btnSize.x, btnSize.y);
    string logErrBtnName = "报错Log";
    #endregion

    #region 日志列表区域
    //日志按钮坐标
    static Vector2 logListBtnPos = new Vector2(0, 30);
    //日志按钮大小
    static Vector2 logListBtnSize = new Vector2(windowSize.x - 60, 30);

    //日志
    private Vector2 logScrollPosition;
    //日志列表区域坐标
    static Vector2 logListPos = new Vector2(10, 60);
    //日志列表区域大小
    static Vector2 logListSize = new Vector2(windowSize.x - 13, Screen.height * 0.5f);
    //日志列表
    Rect logListRect = new Rect(logListPos.x, logListPos.y, logListSize.x, logListSize.y);

    //日志列表显示坐标
    static Vector2 logListViewPos = new Vector2(0, 0);
    //日志列表显示大小
    static Vector2 logListViewSize = new Vector2(windowSize.x - 30, 40);
    //日志列表显示
    Rect logListViewRect = new Rect(logListViewPos.x, logListViewPos.y, logListViewSize.x, logListViewSize.y);
    #endregion

    #region 详细日志区域
    //详细日志
    private Vector2 logContentScrollPosition;
    //详细日志区域坐标
    static Vector2 logContentPos = new Vector2(10, logListPos.y + logListSize.y + 20);
    //详细日志区域大小
    static Vector2 logContentSize = new Vector2(windowSize.x - 13, windowSize.y - windowPos.y - logContentPos.y);
    //详细日志
    Rect logContentRect = new Rect(logContentPos.x, logContentPos.y, logContentSize.x, logContentSize.y);

    //详细日志显示坐标
    static Vector2 logContentViewPos = new Vector2(0, 0);
    //详细日志显示大小
    static Vector2 logContentViewSize = new Vector2(logContentSize.x, logContentSize.y - 10);
    //详细日志显示
    Rect logContentViewRect = new Rect(logContentViewPos.x, logContentViewPos.y, logContentViewSize.x, logContentViewSize.y);

    //文本坐标
    static Vector2 logTextPos = new Vector2(0, 0);
    //文本大小
    static Vector2 logTextSize = new Vector2(logContentViewSize.x, logContentViewSize.y);
    //文本
    Rect logTextRect = new Rect(logTextPos.x, logTextPos.y, logTextSize.x, logTextSize.y);
    //日志详细内容
    string logText = "";
    #endregion


    void Start()
    {
        Application.logMessageReceived += DrawLog;
        logShowMsg = logAllMsg;
    }

    /// <summary>
    /// 控制台输出回调
    /// </summary>
    /// <param name="condition">控制台输出信息</param>
    /// <param name="stackTrace">控制台输出详细信息</param>
    /// <param name="type">控制台输出类型</param>
    void DrawLog(string condition, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Error:
                logAllMsg.Add(new LogMsg(condition, stackTrace, type));
                logErrMsg.Add(new LogMsg(condition, stackTrace, type));
                break;
            case LogType.Assert:
                break;
            case LogType.Warning:
                break;
            case LogType.Log:
                logAllMsg.Add(new LogMsg(condition, stackTrace, type));
                logMsg.Add(new LogMsg(condition, stackTrace, type));
                break;
            case LogType.Exception:
                break;
            default:
                break;
        }
    }


    void OnGUI()
    {
        if (Event.current.isMouse && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
        {
            if (isDrawGui)
            {
                isDrawGui = false;
            }
            else
            {
                isDrawGui = true;
            }
        }

        if (isDrawGui)
        {
            DrawConsole();
        }

    }


    /// <summary>
    /// 绘制控制台
    /// </summary>
    void DrawConsole()
    {
        GUI.Window(0, WindowBackgroundRect, WindowFunction, WindowBackgroundName);

    }
    /// <summary>
    /// 窗体
    /// </summary>
    /// <param name="id"></param>
    void WindowFunction(int id)
    {
        if (GUI.Button(clearBtnRect, clearBtnName))
        {
            //清理控制台输出
            ClearLogMsgList();
        }
        if (GUI.Button(allLogBtnRect, allLogBtnName))
        {
            //显示全部控制台输出
            logShowMsg = logAllMsg;
        }
        if (GUI.Button(logBtnRect, logBtnName))
        {
            //只显示控制台输出打印
            logShowMsg = logMsg;
        }
        if (GUI.Button(logErrBtnRect, logErrBtnName))
        {
            //只显示控制台输出报错
            logShowMsg = logErrMsg;
        }
        //定义窗体可以活动的范围
        logListViewRect.height = logShowMsg.Count * logListBtnSize.y + logListViewSize.y;
        logScrollPosition = GUI.BeginScrollView(logListRect, logScrollPosition, logListViewRect, false, false);
        for (int i = logShowMsg.Count - 1; i >= 0; i--)
        {
            GUI.color = LogCloor(logShowMsg[i].type);
            if (GUI.Button(new Rect(logListBtnPos.x, i * logListBtnPos.y, logListBtnSize.x, logListBtnSize.y), logShowMsg[i].condition))
            {
                logText = logShowMsg[i].stackTrace;
            }
            GUI.color = LogCloor(LogType.Log);
        }
        GUI.EndScrollView();
        logContentScrollPosition = GUI.BeginScrollView(logContentRect, logContentScrollPosition, logContentViewRect, false, false);
        GUI.TextArea(logTextRect, logText);
        GUI.EndScrollView();
    }

    /// <summary>
    /// 清理log列表
    /// </summary>
    void ClearLogMsgList()
    {
        logAllMsg.Clear();
        logMsg.Clear();
        logErrMsg.Clear();
        logShowMsg.Clear();
        logText = "";
    }
    /// <summary>
    /// 返回log颜色
    /// </summary>
    /// <param name="logType">日志类型</param>
    /// <returns></returns>
    Color LogCloor(LogType logType)
    {
        switch (logType)
        {
            case LogType.Error:
                return Color.red;
            case LogType.Assert:
                return Color.white;
            case LogType.Warning:
                return Color.white;
            case LogType.Log:
                return Color.white;
            case LogType.Exception:
                return Color.white;
            default:
                return Color.white;
        }
    }

}