using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameTools
{
    ///// <summary>
    ///// 加载parfab函数
    ///// </summary>
    ///// <param name="prefabNme"></param>
    ///// <returns></returns>
    //public static GameObject LoadPrefab(string prefabNme)
    //{
    //    GameObject prefabObj = ResourcesAB.Load<GameObject>(prefabNme) as GameObject;
    //    if (prefabObj != null)
    //    {
    //        GameObject _view = GameObject.Instantiate(prefabObj, Vector3.zero, Quaternion.identity) as GameObject;
    //        if (_view)
    //        {
    //            _view.transform.position = Vector3.zero;
    //            _view.transform.localPosition = Vector3.zero;
    //            _view.transform.localScale = Vector3.one;
    //            return _view;
    //        }
    //        else
    //        {
    //            Debug.LogError(" GameObject.Instantiate  失败!");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError(prefabNme + "  加载资源失败!!! ");
    //    }
    //    return null;
    //}

    //public static GameObject LoadPrefab(string prefabNme,Transform Root)
    //{
    //    GameObject prefabObj = ResourcesAB.Load<GameObject>(prefabNme) as GameObject;
    //    if (prefabObj != null)
    //    {
    //        GameObject _view = GameObject.Instantiate(prefabObj, Root,false) as GameObject;
    //        if (_view)
    //        {
    //            return _view;
    //        }
    //        else
    //        {
    //            Debug.LogError(" GameObject.Instantiate  失败!");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError(prefabNme + "  加载资源失败!!! ");
    //    }
    //    return null;
    //}

    //public static T LoadPrefab<T>(string prefabNme)
    //{
    //    try
    //    {
    //        GameObject objData = LoadPrefab(prefabNme);
    //        T Tdata = objData.GetComponent<T>();
    //        return Tdata;
    //    }
    //    catch (System.Exception Ex)
    //    {
    //        Debug.Log(Ex.ToString());
    //    }
    //    return default(T);
    //}


    //public static GameObject LoadAssetsPrefab(string prefabNme)
    //{
    //    GameObject prefabObj = ResourcesAB.Load<GameObject>(prefabNme) as GameObject;
    //    if (prefabObj != null)
    //    {
    //        GameObject _view = GameObject.Instantiate(prefabObj, prefabObj.transform.position, prefabObj.transform.localRotation) as GameObject;
    //        if (_view)
    //        {
    //            return _view;
    //        }
    //        else
    //        {
    //            Debug.LogError(" GameObject.Instantiate  失败!");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError(prefabNme + "  加载资源失败!!! ");
    //    }
    //    return null;
    //}
     

    /// <summary>
    /// 射向地图 获取地面高度
    /// </summary>
    /// <returns>The absolute terr hei.</returns>
    /// <param name="pos">Position.</param>
    /// <param name="_mask">_mask.</param>
    public static Vector3 GetAbsoluteTerrHei(Vector3 pos, LayerMask _mask)
    {
        //有可能会出现水下状态

        float x = pos.x;
        float y = pos.y;
        float z = pos.z;

        Ray ray = new Ray();
        ray.origin = new Vector3(x, 1000.0f, z);
        ray.direction = new Vector3(0.0f, -1.0f, 0.0f);

        RaycastHit hit;
        LayerMask nl = 1 << _mask;
        if (Physics.Raycast(ray, out hit, 1500.0f, nl))
        {
            y = (1000.0f - hit.distance);
            return new Vector3(x, y, z);
        }
        else
        {
            return new Vector3(x, y, z);
        }
    }
    /// <summary>
    /// 判断亮点之间的距离是否符合 最大距离
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="fmax"></param>
    /// <returns></returns>
    public static bool vPos2Distance(Vector3 v1, Vector3 v2, float fmax)
    {
        if ((v1 - v2).magnitude <= fmax)
        {
            return true;
        }
        return false;
    }

    public static Color getColor(int colortype)
    {
        Color clr = new Color();
        switch (colortype)
        {
            case 0://灰色
                {
                    clr = Color.gray;
                }
                break;

            case 1://白
                {
                    clr = Color.white;
                }
                break;
            case 2://绿
                {
                    clr = Color.green;

                }
                break;
            case 3://蓝
                {
                    clr = Color.blue;
                }
                break;
            case 4://紫
                {
                    clr = new Color(0.784f, 0, 1);
                }
                break;
            case 5://橙
                {
                    clr = Color.yellow;
                }
                break;
            case 6://红
                {
                    clr = Color.red;
                }
                break;
        }

        return clr;
    }

    /// <summary>
    /// 获取位置
    /// </summary>
    /// <param name="toPos"></param>
    /// <returns></returns>
    Vector3 GetPos(Vector3 toPos)
    {
        //感觉根据射线高度可以发起玩家摔死了
        Ray ray = new Ray();

        float x = toPos.x;
        float y = toPos.y;
        float z = toPos.z;

        ray.origin = new Vector3(x, y + 20, z);
        ray.direction = new Vector3(0f, -1.0f, 0f);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.cyan);//划出射线，只有在scene视图中才能看到
            return hitInfo.point;
        }
        return Vector3.zero;

    }

    /// <summary>
    /// 查找一个矩阵下所有的矩阵
    /// </summary>
    /// <param name="tform"></param>
    /// <param name="alldd"></param>
    /// <returns></returns>

    public static Dictionary<int, T> FindTransforms<T>(Transform root, Dictionary<int, T> Chile = null)
    {
        if (Chile == null) { Chile = new Dictionary<int, T>(); }
         
        T d = root.GetComponent<T>();
        if (d != null)
        {
            Chile[root.GetInstanceID()] = d;
        }

        if (root.childCount == 0)
        {
            return Chile;
        }
        for (int i = 0; i < root.childCount; i++)
        {
            FindTransforms(root.GetChild(i), Chile);
        }
        return Chile;
    }

    public static GameObject GetGameObject(Transform rootTransform, string name)
    {
        GameObject obj = null;
        Dictionary<int, Transform> finds = GameTools.FindTransforms<Transform>(rootTransform);
        foreach (int Iter in finds.Keys)
        {
            if (finds[Iter].name == name) { obj = finds[Iter].gameObject; }
        }
        return obj;
    }

    /// <summary>
    /// 指定路径下查找指定后缀文件
    /// </summary>
    /// <param name="file">路径</param>
    /// <param name="Extension">后缀</param>
    /// <param name="fileList"></param>
    public static  List<string> FindExtensionFile(string file,string Extension, List<string> fileList = null)
    {
        if (fileList == null) {   fileList = new List<string>();  }

        DirectoryInfo drInfo = new DirectoryInfo(file);
        if (drInfo == null) 
        {
            return fileList;
        }
        //获取当前目录下所有以*.RSM结尾的文件，并添加至fileList 

        FileInfo[] fi = drInfo.GetFiles();

        foreach (FileInfo f in fi)
        {
            if (f.Extension == Extension)
            {
                fileList.Add(f.FullName);
            }
        }
        //获取当前目录下所有子文件夹
        DirectoryInfo[] subDr = drInfo.GetDirectories();
        //遍历所有子文件夹
        foreach (DirectoryInfo subDir in drInfo.GetDirectories())
        {
            FindExtensionFile(subDir.FullName, Extension, fileList);
        }
        return fileList;
    }
 
    ///// <summary>
    ///// 递归查找所有文件资源
    ///// </summary>
    ///// <param name="file">递归起始路径</param>
    ///// <param name="undesiredExtension">需要排除的资源后缀名</param>
    ///// <param name="detection">文件检测 false 不通过true 通过</param>
    ///// <param name="fileList">无需传值</param>
    ///// <returns></returns>
    //public static Dictionary<string, FileDescription> FindAllFiles(string file, string[] undesiredExtension,bool detection = false, Dictionary<string, FileDescription> fileList = null)
    //{
    //    if (fileList == null) { fileList = new Dictionary<string, FileDescription>(); }

    //    DirectoryInfo drInfo = new DirectoryInfo(file);
    //    if (drInfo == null)
    //    {
    //        return fileList;
    //    }
    //    //获取当前目录下所有以*.RSM结尾的文件，并添加至fileList 

    //    FileInfo[] fi = drInfo.GetFiles();

    //    foreach (FileInfo f in fi)
    //    {
    //        //通过文件检测
    //        bool bExtension = !detection;
    //        for (int i = 0; i < undesiredExtension.Length; i++)
    //        {
    //            if (f.Extension == undesiredExtension[i])
    //            {
    //                //不通过文件检测
    //                bExtension = detection;
    //            }
    //        }
    //        if (bExtension)
    //        {
    //            fileList[f.FullName] = FileDescription.GetNew(f);
    //        }
    //    }

    //    //获取当前目录下所有子文件夹
    //    //遍历所有子文件夹
    //    foreach (DirectoryInfo subDir in drInfo.GetDirectories())
    //    {
    //        //递归
    //        FindAllFiles(subDir.FullName, undesiredExtension, detection, fileList);
    //    }
    //    return fileList;
    //}

    private const string PATH_SPLIT_CHAR = "\\";
    /// <summary>
    /// 复制指定目录的所有文件
    /// </summary> .manifest
    /// <param name="sourceDir">原始目录</param>
    /// <param name="targetDir">目标目录</param>
    /// <param name="exclude">排除的拷贝项目</param>
    /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
    /// <param name="copySubDir">如果为true,包含目录,否则不包含</param>
    public static void CopyFiles(string sourceDir, string targetDir, string exclude, bool overWrite = true, bool copySubDir = true)
    {
        if (!Directory.Exists(targetDir))
            Directory.CreateDirectory(targetDir);

        //复制当前目录文件
        foreach (string sourceFileName in Directory.GetFiles(sourceDir))
        {
            string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PATH_SPLIT_CHAR) + 1));
            string Extension = Path.GetExtension(sourceFileName);
            if (Extension != exclude)
            {
                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {
                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Copy(sourceFileName, targetFileName, overWrite);
                    }
                }
                else
                {
                    File.Copy(sourceFileName, targetFileName, overWrite);
                }
            }
        }
        //复制子目录
        if (copySubDir)
        {
            foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
            {
                string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                if (!Directory.Exists(targetSubDir))
                    Directory.CreateDirectory(targetSubDir);
                CopyFiles(sourceSubDir, targetSubDir, exclude, overWrite, copySubDir);

            }
        }
    }
}
