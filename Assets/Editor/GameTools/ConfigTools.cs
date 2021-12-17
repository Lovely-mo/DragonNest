using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// modify by zfc @ 2018.11.16
/// 说明：此处xlsx生成lua 以及proto 生成lua配置工具
/// 如果生成失败 配置下protobuf环境 已经python环境 备注：python版本最好是2 以及安装读取excel库 xlrd
/// </summary>

public class ConfigTools : EditorWindow
{
    private static string xlsxFolder = string.Empty;
    private static string protoFolder = string.Empty;

    private bool xlsxGenLuaFinished = false;
    private bool protoGenLuaFinished = false;

    void OnEnable()
    {
        ReadPath();
    }

    [MenuItem("Tools/LuaConfig")]
    static void Init()
    {
        GetWindow(typeof(ConfigTools));
        ReadPath();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("xlsx path : ", EditorStyles.boldLabel, GUILayout.Width(80));
        xlsxFolder = GUILayout.TextField(xlsxFolder, GUILayout.Width(300));
        if (GUILayout.Button("...", GUILayout.Width(40)))
        {
            SelectXlsxFolder();
        }
        GUILayout.EndHorizontal();

        //GUILayout.Space(10);
        //GUILayout.BeginHorizontal();
        //GUILayout.Label("proto path : ", EditorStyles.boldLabel, GUILayout.Width(80));
        //protoFolder = GUILayout.TextField(protoFolder, GUILayout.Width(300));
        //if (GUILayout.Button("...", GUILayout.Width(40)))
        //{
        //    SelectProtoFolder();
        //}
        //GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Label("---------------------");
        if (GUILayout.Button("xlsx gen lua", GUILayout.Width(100)))
        {
            XlsxGenLua();
        }
        GUILayout.Label("---------------------");
        GUILayout.EndHorizontal();
    }

    static int count = 0;
    static List<string> files = new List<string>();
    static List<string> exported = new List<string>();

    private void XlsxGenLua()
    {
        if (!CheckXlsxPath(xlsxFolder))
        {
            return;
        }

        files.Clear();
        exported.Clear();

        GetFilter(xlsxFolder + "/excel", "*.xlsx");

        foreach (var item in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(item);

            Process p = new Process();
            p.StartInfo.FileName = "python";
            p.StartInfo.Arguments = string.Format("excel2lua.py excel/{0}.xlsx lua/{0}.lua", fileName);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WorkingDirectory = xlsxFolder;
            p.Start();
            p.BeginOutputReadLine();
            p.OutputDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    UnityEngine.Debug.Log(e.Data);
                    if (e.Data.Contains("exported"))
                    {
                        exported.Add(fileName);
                        Process pr = sender as Process;
                        if (pr != null)
                        {
                            pr.Close();
                        }
                        if (exported.Count == files.Count)
                            xlsxGenLuaFinished = true;
                    }
                }
            });
        }
    }

    void Update()
    {
        if (protoGenLuaFinished)
        {
            protoGenLuaFinished = false;
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("Succee", "Proto gen lua finished!", "Conform");
        }

        if (xlsxGenLuaFinished)
        {
            xlsxGenLuaFinished = false;

            // copy files
            string destPath = Application.dataPath + "/LuaScripts/Config/Data";
            if (Directory.Exists(destPath))
            {
                Directory.Delete(destPath, true);
            }
            Directory.CreateDirectory(destPath);

            string[] luaFiles = Directory.GetFiles(xlsxFolder + "/lua");
            foreach (var oneFile in luaFiles)
            {
                string destFileName = Path.Combine(destPath, Path.GetFileName(oneFile));
                UnityEngine.Debug.Log("Copy To: " + destFileName);
                File.Copy(oneFile, destFileName, true);
            }

            AssetDatabase.Refresh();

            string content = exported.Count + " Xlsx gen lua finished!\n\n";
            foreach (var item in exported)
            {
                content += "-->" + item + "\n";
            }
            EditorUtility.DisplayDialog("Succee", content, "Conform");
        }
    }

    private bool CheckXlsxPath(string xlsxPath)
    {
        if (string.IsNullOrEmpty(xlsxPath))
        {
            return false;
        }

        if (!File.Exists(xlsxPath + "/trans2lua.bat"))
        {
            EditorUtility.DisplayDialog("Error", "Err path :\nNo find ./trans2lua.bat", "Conform");
            return false;
        }

        return true;
    }

    private bool CheckProtoPath(string protoPath)
    {
        if (string.IsNullOrEmpty(protoPath))
        {
            return false;
        }

        if (!File.Exists(protoPath + "/make_proto.bat"))
        {
            EditorUtility.DisplayDialog("Error", "Err path :\nNo find ./make_proto.bat", "Conform");
            return false;
        }

        return true;
    }

    private void SelectXlsxFolder()
    {
        var selXlsxPath = EditorUtility.OpenFolderPanel("Select xlsx folder", "", "");
        if (!CheckXlsxPath(selXlsxPath))
        {
            return;
        }

        xlsxFolder = selXlsxPath;
        SavePath();
    }

    private void SelectProtoFolder()
    {
        var selProtoPath = EditorUtility.OpenFolderPanel("Select proto folder", "", "");
        if (!CheckProtoPath(selProtoPath))
        {
            return;
        }

        protoFolder = selProtoPath;
        SavePath();
    }

    static private void SavePath()
    {
        EditorPrefs.SetString("xlsxFolder", xlsxFolder);
        EditorPrefs.SetString("protoFolder", protoFolder);
    }

    static private void ReadPath()
    {
        xlsxFolder = EditorPrefs.GetString("xlsxFolder");
        protoFolder = EditorPrefs.GetString("protoFolder");
    }

    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    static void GetFilter(string path, string searchPattern)
    {
        string[] names = Directory.GetFiles(path, searchPattern);
        string[] dirs = Directory.GetDirectories(path);

        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta"))
                continue;

            FileInfo file = new FileInfo(filename);
            if ((file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                continue;

            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs)
        {
            GetFilter(dir, searchPattern);
        }
    }

    static List<string> newpaths = new List<string>();
    static List<string> newfiles = new List<string>();

    // 遍历目录及其子目录
    static void Recursive(string path)
    {
        UnityEngine.Debug.Log("----" + path);
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);// 扩展名
            if (ext.Equals(".meta") || ext.Equals(".svn") || ext.Equals(".txt")
                || ext.Contains(".DS_Store") || ext.Contains(".exe") || ext.Contains(".bat")) continue;
            newfiles.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs)
        {
            newpaths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }

    [MenuItem("LuaTools/（1）生成PB文件", false, 0)]
    public static void BuildProtobufFile()
    {
        int index = Application.dataPath.ToLower().IndexOf("/assets");
        string root = Application.dataPath.ToLower().Substring(0, Application.dataPath.ToLower().Substring(0, index).Length);
        string dir = root + "/LuaProto/proto";
        UnityEngine.Debug.Log(dir);
        newpaths.Clear();
        newfiles.Clear();
        Recursive(dir);

        string protoc = Application.dataPath.ToLower().Replace("assets", "luaproto") + "/protoc.exe";
        string protoc_gen_dir = string.Format("\"" + Application.dataPath.ToLower().Replace("assets", "luaproto") + "/protoc-gen-lua/plugin/protoc-gen-lua.bat\""); //"\"d:/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";
        string outPath = Application.dataPath.ToLower() + "/LuaScripts/Net/Proto"; //"./";
        if (!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }
        foreach (string f in newfiles)
        {
            string name = Path.GetFileName(f);
            string ext = Path.GetExtension(f);
            //UnityEngine.Debug.Log(name + "   " + ext);
            if (!ext.Equals(".proto")) continue;

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = protoc;
            info.Arguments = string.Format(" --lua_out=\"{0}\" --plugin=protoc-gen-lua={1} {2}", outPath, protoc_gen_dir, name);
            //" --lua_out=\"c:/work\" --plugin=protoc-gen-lua=" + protoc_gen_dir + " " + name;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.UseShellExecute = true;
            info.WorkingDirectory = dir;
            info.ErrorDialog = true;
            //UnityEngine.Debug.Log(info.FileName + " " + info.Arguments);
            Process pro = Process.Start(info);
            pro.WaitForExit();
        }
        AssetDatabase.Refresh();
    }


    [MenuItem("LuaTools/（2）生成PB配置文件", false, 0)]
    /// <summary>
    /// 生成配置文件
    /// </summary>
    public static void GenerateProtoConfig()
    {
        string fileConfig = GetMsgIdPath();
        //UnityEngine.Debug.Log(fileConfig);
        if (File.Exists(fileConfig))//如果读取的文件存在，那么就进行解析
        {
            StringBuilder sbMsgIdMap = new StringBuilder();
            sbMsgIdMap.AppendLine("--Generated By msgid-gen-lua Do not Edit");
            sbMsgIdMap.AppendLine("local config = {");

            StringBuilder sbMsgIdDefine = new StringBuilder();
            sbMsgIdDefine.AppendLine("--Generated By msgid-gen-lua Do not Edit");
            sbMsgIdDefine.AppendLine("local config = {");

            FileStream fs = File.OpenRead(fileConfig);
            StreamReader sr = new StreamReader(fs);
            string line = string.Empty;
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                if (!line.StartsWith("#"))
                {
                    //UnityEngine.Debug.Log("读取的内容==" + line);
                    string[] tmps = line.Split(new char[] { '=', '.', '#' });
                    //UnityEngine.Debug.Log(string.Format("id={0},pb模块={1},类={2},注释={3}",tmps[0],tmps[1],tmps[2],tmps[3]));
                    #region 添加msgidmap
                    sbMsgIdMap.Append("\t");
                    sbMsgIdMap.Append("[");
                    sbMsgIdMap.Append(tmps[0]);
                    sbMsgIdMap.Append("] = ");
                    sbMsgIdMap.Append(tmps[1]);
                    sbMsgIdMap.Append("_pb.");
                    sbMsgIdMap.Append(tmps[2].TrimEnd());
                    sbMsgIdMap.Append("()");
                    sbMsgIdMap.Append(",");
                    sbMsgIdMap.Append("\t");
                    sbMsgIdMap.Append("  --");
                    sbMsgIdMap.Append(tmps[3]);
                    sbMsgIdMap.Append("\r\n");
                    #endregion

                    #region 添加msgiddefine
                    sbMsgIdDefine.Append("\t");
                    sbMsgIdDefine.Append(tmps[2].TrimEnd());
                    sbMsgIdDefine.Append(" = ");
                    sbMsgIdDefine.Append(tmps[0]);
                    sbMsgIdDefine.Append(",");
                    sbMsgIdDefine.Append("  --");
                    sbMsgIdDefine.Append(tmps[3]);
                    sbMsgIdDefine.Append("\r\n");
                    #endregion
                }
            }
            sbMsgIdMap.AppendLine("}");
            sbMsgIdMap.AppendLine("return config");

            sbMsgIdDefine.AppendLine("}");
            sbMsgIdDefine.AppendLine("return config");

            //UnityEngine.Debug.Log(sbMsgIdMap.ToString());
            string msgIdMapFilePath = Application.dataPath + "/LuaScripts/Net/Config/MsgIDMap.lua";
            string msgIdDefineFilePath = Application.dataPath + "/LuaScripts/Net/Config/MsgIDDefine.lua";
            CreatUIScript(msgIdMapFilePath, sbMsgIdMap.ToString());
            CreatUIScript(msgIdDefineFilePath, sbMsgIdDefine.ToString());
            sr.Close();
            fs.Close();
            AssetDatabase.Refresh();//刷新资源
        }
    }


    [MenuItem("LuaTools/(1)生成消息数据名称配置文件")]
    /// <summary>
    /// 生成消息数据名称配置文件
    /// </summary>
    private static void GenerateMsgDataNamesConfig()
    {
        CheckDir();
        string fileConfig = GetMsgIdPath();
        if (File.Exists(fileConfig))//如果读取的文件存在，那么就进行解析
        {
            StringBuilder sbPbDataConfig = new StringBuilder();
            sbPbDataConfig.AppendLine("--Generated By msgid-gen-lua Do not Edit");
            sbPbDataConfig.AppendLine("--这里存储的都是服务器返回给客户端的消息名称");
            sbPbDataConfig.AppendLine("--根据需要自己动态开启注释");
            sbPbDataConfig.AppendLine("local MsgDataNames = {");
            FileStream fs = File.OpenRead(fileConfig);
            StreamReader sr = new StreamReader(fs);
            string line = string.Empty;
            int indexCounter = 0;//计数器解决lua限制不能超过200个local的变量的问题
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                if (!line.StartsWith("#"))
                {
                    //UnityEngine.Debug.Log("读取的内容==" + line);
                    string[] tmps = line.Split(new char[] { '=', '.', '#' });
                    //UnityEngine.Debug.Log(string.Format("id={0},pb模块={1},类={2},注释={3}",tmps[0],tmps[1],tmps[2],tmps[3]));
                    if (tmps[2].ToLower().StartsWith("s"))
                    {
                        indexCounter++;
                        #region 生成网络消息配置文件
                        sbPbDataConfig.Append("\t");
                        sbPbDataConfig.Append("--");
                        sbPbDataConfig.Append(tmps[2].Trim());
                        sbPbDataConfig.Append("=\t");
                        sbPbDataConfig.Append("\"");
                        sbPbDataConfig.Append(tmps[2].Trim());
                        sbPbDataConfig.Append("\"");
                        sbPbDataConfig.Append(",");
                        sbPbDataConfig.Append("\t--");
                        sbPbDataConfig.Append(tmps[3]);
                        sbPbDataConfig.Append("\r\n");
                        #endregion
                    }
                }
            }
            UnityEngine.Debug.Log("服务器返回的数据=====" + indexCounter);
            sbPbDataConfig.AppendLine("}");
            sbPbDataConfig.AppendLine("return  ConstClass(\"MsgDataNames\",MsgDataNames)");

            string msgDataNamespFilePath = Application.dataPath + "/LuaScripts/Net/MsgHandler/AutoGen/MsgDataNames.lua";
            MVCTools.WriteToFile(msgDataNamespFilePath, sbPbDataConfig.ToString());
            sr.Close();
            fs.Close();
            AssetDatabase.Refresh();//刷新资源
        }
    }

    [MenuItem("LuaTools/(2)生成注册消息文件")]
    /// <summary>
    /// 生成pb注册消息文件
    /// </summary>
    private static void GenerateRegisterMsg()
    {
        CheckDir();
        string fileConfig = GetMsgIdPath();
        if (File.Exists(fileConfig))//如果读取的文件存在，那么就进行解析
        {
            StringBuilder sbRegisterMsg = new StringBuilder();
            sbRegisterMsg.AppendLine("--Generated By msgid-gen-lua Do not Edit\n");
            sbRegisterMsg.AppendLine("local PBMessageDataOne = {}");
            sbRegisterMsg.AppendLine("local MsgCallBackNames = require(\"Net/MsgHandler/AutoGen/MsgCallBackNames\")");
            sbRegisterMsg.AppendLine("local MsgDataNames = require(\"Net/MsgHandler/AutoGen/MsgDataNames\")\n");
            StringBuilder tmpsbRegisterMsg = new StringBuilder();

            StringBuilder sbRegisterMsg2 = new StringBuilder();
            sbRegisterMsg2.AppendLine("--Generated By msgid-gen-lua Do not Edit\n");
            sbRegisterMsg2.AppendLine("local PBMessageDataTwo = {}");
            sbRegisterMsg2.AppendLine("local MsgCallBackNames = require(\"Net/MsgHandler/AutoGen/MsgCallBackNames\")");
            sbRegisterMsg2.AppendLine("local MsgDataNames = require(\"Net/MsgHandler/AutoGen/MsgDataNames\")\n");
            StringBuilder tmpsbRegisterMsg2 = new StringBuilder();

            FileStream fs = File.OpenRead(fileConfig);
            StreamReader sr = new StreamReader(fs);
            string line = string.Empty;
            int indexCounter = 0;//计数器解决lua限制不能超过200个local的变量的问题
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                if (!line.StartsWith("#"))
                {
                    //UnityEngine.Debug.Log("读取的内容==" + line);
                    string[] tmps = line.Split(new char[] { '=', '.', '#' });
                    //UnityEngine.Debug.Log(string.Format("id={0},pb模块={1},类={2},注释={3}",tmps[0],tmps[1],tmps[2],tmps[3]));
                    if (tmps[2].ToLower().StartsWith("s"))
                    {
                        indexCounter++;
                        if (indexCounter <= 120)
                        {
                            #region 生成Registermsg.lua----------ToDo:需要做处理，限制local不能超过200个
                            sbRegisterMsg.Append("--");
                            sbRegisterMsg.AppendLine(tmps[3]);
                            sbRegisterMsg.Append("local function Get_");
                            sbRegisterMsg.Append(tmps[2].Trim());
                            sbRegisterMsg.Append("(");
                            sbRegisterMsg.AppendLine("msg)");
                            sbRegisterMsg.AppendLine("\t--TODO:根据需要打开注释的代码，然后在对应的函数里面写处理逻辑)");
                            sbRegisterMsg.Append("\tDataManager:GetInstance():Broadcast(MsgCallBackNames.");
                            sbRegisterMsg.Append(tmps[2].Trim());
                            sbRegisterMsg.AppendLine(", msg)");
                            sbRegisterMsg.Append("\t--DataManager:GetInstance():Broadcast(MsgDataNames.");
                            sbRegisterMsg.Append(tmps[2].Trim());
                            sbRegisterMsg.AppendLine(", msg)");
                            sbRegisterMsg.AppendLine("end\n");
                            tmpsbRegisterMsg.Append("PBMessageDataOne.Get_");
                            tmpsbRegisterMsg.Append(tmps[2].Trim());
                            tmpsbRegisterMsg.Append("=");
                            tmpsbRegisterMsg.Append("Get_");
                            tmpsbRegisterMsg.AppendLine(tmps[2].Trim());
                            #endregion
                        }
                        else
                        {
                            sbRegisterMsg2.Append("--");
                            sbRegisterMsg2.AppendLine(tmps[3]);
                            sbRegisterMsg2.Append("local function Get_");
                            sbRegisterMsg2.Append(tmps[2].Trim());
                            sbRegisterMsg2.Append("(");
                            sbRegisterMsg2.AppendLine("msg)");
                            sbRegisterMsg2.AppendLine("\t--TODO:根据需要打开注释的代码，然后在对应的函数里面写处理逻辑)");
                            sbRegisterMsg2.Append("\tDataManager:GetInstance():Broadcast(MsgCallBackNames.");
                            sbRegisterMsg2.Append(tmps[2].Trim());
                            sbRegisterMsg2.AppendLine(", msg)");
                            sbRegisterMsg2.Append("\t--DataManager:GetInstance():Broadcast(MsgDataNames.");
                            sbRegisterMsg2.Append(tmps[2].Trim());
                            sbRegisterMsg2.AppendLine(", msg)");
                            sbRegisterMsg2.AppendLine("end\n");
                            tmpsbRegisterMsg2.Append("PBMessageDataTwo.Get_");
                            tmpsbRegisterMsg2.Append(tmps[2].Trim());
                            tmpsbRegisterMsg2.Append("=");
                            tmpsbRegisterMsg2.Append("Get_");
                            tmpsbRegisterMsg2.AppendLine(tmps[2].Trim());
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            sbRegisterMsg.AppendLine(tmpsbRegisterMsg.ToString());
            sbRegisterMsg.Append("return PBMessageDataOne");
            sbRegisterMsg2.AppendLine(tmpsbRegisterMsg2.ToString());
            sbRegisterMsg2.Append("return PBMessageDataTwo");
            string registerMsgFilePath = Application.dataPath + "/LuaScripts/Net/MsgHandler/AutoGen/RegisterMsgPartOne.lua";
            MVCTools.WriteToFile(registerMsgFilePath, sbRegisterMsg.ToString());
            string register2MsgFilePath = Application.dataPath + "/LuaScripts/Net/MsgHandler/AutoGen/RegisterMsgPartTwo.lua";
            MVCTools.WriteToFile(register2MsgFilePath, sbRegisterMsg2.ToString());
            fs.Close();
            sr.Close();
            AssetDatabase.Refresh();//刷新资源
        }
    }

    [MenuItem("LuaTools/(3)生成消息处理文件")]
    public static void GenerateMsgHandler()
    {
        CheckDir();
        string fileConfig = GetMsgIdPath();
        if (File.Exists(fileConfig))//如果读取的文件存在，那么就进行解析
        {
            StringBuilder sbMsgHandler = new StringBuilder();
            sbMsgHandler.AppendLine("--Generated By msgid-gen-lua Do not Edit\n");
            sbMsgHandler.AppendLine("local MsgHandler = BaseClass(\"MsgHandler\", Singleton)");
            sbMsgHandler.AppendLine("local MsgIDDefine = require(\"Net/Config/MsgIDDefine\")");
            sbMsgHandler.AppendLine("local messageOne = require(\"Net/MsgHandler/AutoGen/RegisterMsgPartOne\")");
            sbMsgHandler.AppendLine("local messageTwo = require(\"Net/MsgHandler/AutoGen/RegisterMsgPartTwo\")\n");

            sbMsgHandler.AppendLine("local function RegisterMsg()\t");

            FileStream fs = File.OpenRead(fileConfig);
            StreamReader sr = new StreamReader(fs);
            string line = string.Empty;
            int indexCounter = 0;//计数器解决lua限制不能超过200个local的变量的问题
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                if (!line.StartsWith("#"))
                {
                    //UnityEngine.Debug.Log("读取的内容==" + line);
                    string[] tmps = line.Split(new char[] { '=', '.', '#' });
                    //UnityEngine.Debug.Log(string.Format("id={0},pb模块={1},类={2},注释={3}",tmps[0],tmps[1],tmps[2],tmps[3]));
                    if (tmps[2].ToLower().StartsWith("s"))
                    {
                        indexCounter++;
                        //这里解决lua 200个local的问题
                        if (indexCounter <= 120)
                        {
                            #region 生成MsgHandler.lua
                            sbMsgHandler.Append("\tDataManager:GetInstance():AddListener(MsgIDDefine.");
                            sbMsgHandler.Append(tmps[2].Trim());
                            sbMsgHandler.Append(",messageOne.Get_");
                            sbMsgHandler.Append(tmps[2].Trim());
                            sbMsgHandler.AppendLine(")");
                            #endregion
                        }
                        else
                        {
                            #region 生成MsgHandler.lua
                            sbMsgHandler.Append("\tDataManager:GetInstance():AddListener(MsgIDDefine.");
                            sbMsgHandler.Append(tmps[2].Trim());
                            sbMsgHandler.Append(",messageTwo.Get_");
                            sbMsgHandler.Append(tmps[2].Trim());
                            sbMsgHandler.AppendLine(")");
                            #endregion
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            sbMsgHandler.AppendLine("end\n");
            sbMsgHandler.AppendLine("local function __init(self)");
            sbMsgHandler.AppendLine("\t RegisterMsg()");
            sbMsgHandler.AppendLine("end\n");
            sbMsgHandler.AppendLine("MsgHandler.__init = __init\n");
            sbMsgHandler.AppendLine("return MsgHandler");

            string msgHandlerMsgFilePath = Application.dataPath + "/LuaScripts/Net/MsgHandler/AutoGen/MsgHandler.lua";
            MVCTools.WriteToFile(msgHandlerMsgFilePath, sbMsgHandler.ToString());

            fs.Close();
            sr.Close();
            AssetDatabase.Refresh();//刷新资源
        }
    }

    [MenuItem("LuaTools/(4)生成消息回调名称配置文件")]
    /// <summary>
    /// 生成消息数据名称配置文件
    /// </summary>
    private static void GenerateMsgCallBackNamesConfig()
    {
        CheckDir();
        string fileConfig = GetMsgIdPath();
        if (File.Exists(fileConfig))//如果读取的文件存在，那么就进行解析
        {
            StringBuilder sbMsgCallBackNames = new StringBuilder();
            sbMsgCallBackNames.AppendLine("--Generated By msgid-gen-lua Do not Edit");
            sbMsgCallBackNames.AppendLine("--这里存储的是消息回调名称");
            sbMsgCallBackNames.AppendLine("local MsgCallBackNames = {");
            FileStream fs = File.OpenRead(fileConfig);
            StreamReader sr = new StreamReader(fs);
            string line = string.Empty;
            int indexCounter = 0;//计数器解决lua限制不能超过200个local的变量的问题
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                if (!line.StartsWith("#"))
                {
                    //UnityEngine.Debug.Log("读取的内容==" + line);
                    string[] tmps = line.Split(new char[] { '=', '.', '#' });
                    //UnityEngine.Debug.Log(string.Format("id={0},pb模块={1},类={2},注释={3}",tmps[0],tmps[1],tmps[2],tmps[3]));
                    if (tmps[2].ToLower().StartsWith("s"))
                    {
                        indexCounter++;
                        #region 生成网络消息配置文件
                        sbMsgCallBackNames.Append("\t");
                        //sbMsgCallBackNames.Append("--");
                        sbMsgCallBackNames.Append(tmps[2].Trim());
                        sbMsgCallBackNames.Append("=\t");
                        sbMsgCallBackNames.Append("\"");
                        sbMsgCallBackNames.Append(tmps[2].Trim());
                        sbMsgCallBackNames.Append("\"");
                        sbMsgCallBackNames.Append(",");
                        sbMsgCallBackNames.Append("\t--");
                        sbMsgCallBackNames.Append(tmps[3]);
                        sbMsgCallBackNames.Append("\r\n");
                        #endregion
                    }
                }
            }
            UnityEngine.Debug.Log("服务器返回的数据=====" + indexCounter);
            sbMsgCallBackNames.AppendLine("}");
            sbMsgCallBackNames.AppendLine("return  ConstClass(\"MsgCallBackNames\",MsgCallBackNames)");

            string msgDataNamespFilePath = Application.dataPath + "/LuaScripts/Net/MsgHandler/AutoGen/MsgCallBackNames.lua";
            MVCTools.WriteToFile(msgDataNamespFilePath, sbMsgCallBackNames.ToString());
            fs.Close();
            sr.Close();
            AssetDatabase.Refresh();//刷新资源
        }
    }

    [MenuItem("LuaTools/-------一键生成")]
    public static void GeneratePBDataConfig()
    {
        GenerateMsgDataNamesConfig();
        GenerateRegisterMsg();
        GenerateMsgHandler();
        GenerateMsgCallBackNamesConfig();
        AssetDatabase.Refresh();//刷新资源
    }

    private static void CheckDir()
    {
        string path = Application.dataPath + "/LuaScripts/Net/MsgHandler/AutoGen";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// 获取配置文件陆军
    /// </summary>
    /// <returns></returns>
    public static string GetMsgIdPath()
    {
        string root = Application.dataPath.ToLower().Replace("assets", "/LuaProto/");
        UnityEngine.Debug.Log(root);
        string fileConfig = root + "msgid.conf";
        return fileConfig;
    }

    private static void CreatUIScript(string BaseFilePath, string str)
    {
        if (File.Exists(BaseFilePath))
        {
            File.Delete(BaseFilePath);
        }
        if (!File.Exists(BaseFilePath))
        {
            FileStream fs = File.Create(BaseFilePath);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(str);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
    }
}
