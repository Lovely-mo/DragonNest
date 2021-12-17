using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LuaTool
{
    public string Getstring(string name)
    {

        return name;
    }

    #region 鼠标类型射线
    /// <summary>
    /// 鼠标射线获取一个物体
    /// </summary>
    /// <returns></returns>
    public static GameObject GetMouseObj()
    {
        GameObject hitgameObject = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            hitgameObject = hit.collider.gameObject;
        }
        return hitgameObject;
    }

    /// <summary>
    /// 鼠标射线获取一个指定层的物体
    /// </summary>
    /// <returns></returns>
    public static GameObject GetMouselayerObj(int layer)
    {
        GameObject hitgameObject = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (layer == hit.collider.gameObject.layer)// 我是谁  我是int类型
            {
                hitgameObject = hit.collider.gameObject;
            }
        }
        return hitgameObject;
    }


    /// <summary>
    /// 射线获取射到物体上的点的位置
    /// </summary>
    /// <param name="origin">射线的起始点</param>
    /// <param name="direction">射线的方向</param>
    /// <returns></returns>
    public static Vector3 GetMouseRayPos()
    {
        Vector3 raypos = Vector3.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //射线碰到了物体
        if (Physics.Raycast(ray, out hit))
        {
            raypos = hit.point;
        }
        return raypos;
    }
    public static Vector3 GetMouseRayPos(float distance)
    {
        Vector3 raypos = Vector3.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //射线碰到了物体
        if (Physics.Raycast(ray, out hit))
        {
            raypos = hit.point;
        }
        else
        {
            raypos = ray.origin + (ray.direction * distance);
        }
        return raypos;
    }
    /// <summary>
    /// 鼠标类型射线碰撞的所有物体
    /// </summary>
    /// <param name="origin">射线的起始点</param>
    /// <param name="direction">射线的方向</param>
    /// <returns></returns>
    public static List<GameObject> GetMouseObjs()
    {
        List<GameObject> listobj = new List<GameObject>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit h = hits[i];
            listobj.Add(h.collider.gameObject);
        }
        return listobj;
    }
    /// <summary>
    /// 鼠标类型射线碰撞的所有物体 射线点
    /// </summary>
    /// <param name="origin">射线的起始点</param>
    /// <param name="direction">射线的方向</param>
    /// <returns></returns>
    public static List<Vector3> GetMouseRayPoss()
    {
        List<Vector3> listobj = new List<Vector3>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit h = hits[i];
            listobj.Add(h.point);
        }
        return listobj;
    }
    #endregion
    /// <summary>
    /// 射线获取物体
    /// </summary>
    /// <param name="origin">射线的起始点</param>
    /// <param name="direction">射线的方向</param>
    /// <returns></returns>
    public static GameObject GetRayObj(Vector3 origin, Vector3 direction)
    {
        GameObject hitgameObject = null;

        Ray ray = new Ray();
        ray.origin = origin;
        ray.direction = direction;

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);//划出射线，只有在scene视图中才能看到
            //销毁解除的游戏对象
            hitgameObject = hitInfo.collider.gameObject;
        }
       
        return hitgameObject;
    }

    public static Vector3 GetRayPos(Vector3 origin, Vector3 direction)
    {
        Vector3 raypos = Vector3.zero;
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = origin;
        ray.direction = direction;
        //射线碰到了物体
        if (Physics.Raycast(ray, out hit))
        {
            raypos = hit.point;
        }
        return raypos;
    }

    /// <summary>
    /// 射线获取物体
    /// </summary>
    /// <param name="origin">射线的起始点</param>
    /// <param name="direction">射线的方向</param>
    /// <returns></returns>
    public static List<GameObject> GetRayObjs(Vector3 origin, Vector3 direction)
    {
        List<GameObject> listobj = new List<GameObject>();
        Ray ray = new Ray();
        ray.origin = origin;
        ray.direction = direction;
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit h = hits[i];
            listobj.Add(h.collider.gameObject);
        }
        return listobj;
    }

    #region 界面排序用的
    //UI 界面排序用的
    //public static int tonumber(float Nums,int fanwei = 100000)
    //{
    //    return fanwei + (int)(Nums * 100); 
    //}
    public static int tonumber(float Nums)
    {
        return (int)(Nums * 100);
    }
    public static int Range(float start, float end)
    {
        return UnityEngine.Random.Range((int)start, (int)end);
    }
    #endregion


    public static string[] SplitD(string arr)
    {
        return arr.Split('.');
    }

    public static string[] SplitP(string arr)
    {
        return arr.Split('/');
    }


    //public static string  UTF8ToGB2312(string str)
    //{
    //    try
    //    {
    //        Encoding   gb2312 = Encoding.GetEncoding(65001);
    //        Encoding utf8 = Encoding.GetEncoding("gb2312");//Encoding.Default ,936
    //        byte[] temp = utf8.GetBytes(str);
    //        byte[] temp1 = Encoding.Convert(utf8, gb2312, temp);
    //        string result = gb2312.GetString(temp1);
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return "";
    //    }
    //} 
}