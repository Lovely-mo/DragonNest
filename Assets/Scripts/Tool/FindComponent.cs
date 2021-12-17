using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using XLua;
using UnityEngine.AI;



//作者：ASnake
//链接：https://www.jianshu.com/p/59d0e47e6645
//来源：简书
//著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。

#region 碰撞触发定义
public enum PZenum
{
    /// <summary>
    /// 触发器进入
    /// </summary>
    OnTriggerEnter,
    /// <summary>
    /// 触发器离开
    /// </summary>
    OnTriggerExit,
    /// <summary>
    /// 触发器停留
    /// </summary>
    OnTriggerStay,
    /// <summary>
    /// 碰撞进入
    /// </summary>
    OnCollisionEnter,
    /// <summary>
    /// 碰撞离开
    /// </summary>
    OnCollisionExit,
    /// <summary>
    /// 碰撞停留
    /// </summary>
    OnCollisionStay,
}
[CSharpCallLua]
public delegate void DelegateTriggerFun(GameObject myself, GameObject other, Collider d);
[CSharpCallLua]
public delegate void DelegateCollisionFun(GameObject myself, GameObject other, Collision d);
public delegate void DelegateFun();
#endregion


#region 函数生周期枚举
public enum FUNC
{
    /// <summary>
    /// 唤醒事件，游戏一开始运行就执行，只执行一次。
    /// </summary>
    Awake = 0,
    /// <summary>
    /// 启用事件，只执行一次。当脚本组件被启用的时候执行一次。
    /// </summary>
    OnEnable = 1,
    /// <summary>
    /// 启用事件，只执行一次。当脚本组件被启用的时候执行一次。
    /// </summary>
    Start = 2,
    /// <summary>
    /// 固定更新事件，执行N次，0.02秒执行一次。所有物理组件相关的更新都在这个事件中处理。
    /// </summary>
    FixedUpdate = 3,
    /// <summary>
    /// 更新事件，执行N次，每帧执行一次。
    /// </summary>
    Update = 4,
    /// <summary>
    /// 稍后更新事件，执行N次，在 Update() 事件执行完毕后再执行。
    /// </summary>
    LateUpdate = 5,
    /// <summary>
    /// GUI渲染事件，执行N次，执行的次数是 Update() 事件的两倍。
    /// </summary>
    OnGUI = 6,
    /// <summary>
    /// 禁用事件，执行一次。在 OnDestroy() 事件前执行。或者当该脚本组件被“禁用”后，也会触发该事件。
    /// </summary>
    OnDisable = 7,
    /// <summary>
    /// 销毁事件，执行一次。当脚本所挂载的游戏物体被销毁时执行。
    /// </summary>
    OnDestroy = 8,
    /// <summary>
    /// 玩家丢失焦点进入焦点
    /// </summary>
    OnApplicationFocus = 9,
    /// <summary>
    /// 游戏暂停时
    /// </summary>
    OnApplicationPause = 10,
    /// <summary>
    /// 游戏退出时
    /// </summary>
    OnApplicationQuit = 11,
}
#endregion
/// <summary>
/// 查找组件
/// </summary>
[XLua.CSharpCallLua]
public class FindComponent : MonoBehaviour
{
    #region 函数生命周期注册
    private Dictionary<FUNC, System.Action> DicFUNC = new Dictionary<FUNC, Action>();
    public void ADDFUNC(FUNC fUNC, System.Action luaFun)
    {
        DicFUNC[fUNC] = luaFun;
    }
    void Awake() { if (DicFUNC.ContainsKey(FUNC.Awake)) { DicFUNC[FUNC.Awake](); } }//：唤醒事件，游戏一开始运行就执行，只执行一次。

    void OnEnable() { if (DicFUNC.ContainsKey(FUNC.OnEnable)) { DicFUNC[FUNC.OnEnable](); } }//：启用事件，只执行一次。当脚本组件被启用的时候执行一次。

    void Start() { if (DicFUNC.ContainsKey(FUNC.Start)) { DicFUNC[FUNC.Start](); } }//：开始事件，执行一次。

    void FixedUpdate() { if (DicFUNC.ContainsKey(FUNC.FixedUpdate)) { DicFUNC[FUNC.FixedUpdate](); } }//：固定更新事件，执行N次，0.02秒执行一次。所有物理组件相关的更新都在这个事件中处理。

    void Update() { if (DicFUNC.ContainsKey(FUNC.Update)) { DicFUNC[FUNC.Update](); } }//：更新事件，执行N次，每帧执行一次。

    void LateUpdate() { if (DicFUNC.ContainsKey(FUNC.LateUpdate)) { DicFUNC[FUNC.LateUpdate](); } }//：稍后更新事件，执行N次，在 Update() 事件执行完毕后再执行。

    void OnGUI() { if (DicFUNC.ContainsKey(FUNC.OnGUI)) { DicFUNC[FUNC.OnGUI](); } }//：GUI渲染事件，执行N次，执行的次数是 Update() 事件的两倍。

    void OnDisable() { if (DicFUNC.ContainsKey(FUNC.OnDisable)) { DicFUNC[FUNC.OnDisable](); } }//：禁用事件，执行一次。在 OnDestroy() 事件前执行。或者当该脚本组件被“禁用”后，也会触发该事件。

    void OnDestroy() { if (DicFUNC.ContainsKey(FUNC.OnDestroy)) { DicFUNC[FUNC.OnDestroy](); } }//：销毁事件，执行一次。当脚本所挂载的游戏物体被销毁时执行。

    private void OnApplicationFocus(bool focus) { if (DicFUNC.ContainsKey(FUNC.OnApplicationFocus)) { DicFUNC[FUNC.OnApplicationFocus](); } } //玩家丢失焦点进入焦点

    private void OnApplicationPause(bool pause) { if (DicFUNC.ContainsKey(FUNC.OnApplicationPause)) { DicFUNC[FUNC.OnApplicationPause](); } }//游戏暂停时

    private void OnApplicationQuit() { if (DicFUNC.ContainsKey(FUNC.OnApplicationQuit)) { DicFUNC[FUNC.OnApplicationQuit](); } }//游戏退出时

    #endregion


    #region 获取物体脚本组件模块
    /// <summary>
    /// 添加查找组价
    /// </summary>
    /// <param name="O"></param>
    /// <returns></returns>
    public static FindComponent ADD(GameObject O)
    {
        FindComponent d = O.GetComponent<FindComponent>();
        if (d == null) { d = O.AddComponent<FindComponent>(); }
        return d;
    }
    public static Transform GetTransform(Transform transform, string name)
    {
        Transform obj = null;
        Dictionary<int, Transform> finds = GameTools.FindTransforms<Transform>(transform);
        foreach (int Iter in finds.Keys)
        {
            if (finds[Iter].name == name)
            {
                obj = finds[Iter];
            }
        }
        return obj;
    }
    /// <summary>
    /// 查找prefab 中 的物体
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetGameObject(string name)
    {
        GameObject obj = null;
        Dictionary<int, Transform> finds = GameTools.FindTransforms<Transform>(this.transform);
        foreach (int Iter in finds.Keys)
        {
            if (finds[Iter].name == name)
            {
                obj = finds[Iter].gameObject;
            }
        }
        ADD(obj);
        return obj;
    }
    /// <summary>
    /// 获得 prefab 上面挂载的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objName"></param>
    /// <returns></returns>
    private T getComponent<T>(string objName) where T : Component
    {
        T Cmpent = null;
        GameObject b = GetGameObject(objName);
        if (b != null)
        {
            Cmpent = b.GetComponent<T>();

            if (Cmpent == null)
            {
                Cmpent = b.AddComponent<T>();
            }
        }
        if (b == null) { Debug.LogError("函数名：getComponent 命令：查找" + objName + "在此物体对象集合中不存在！请给定正确名称！"); }
        return Cmpent;
    }

    /// <summary>
    /// 给Lua使用的组件
    /// </summary>
    /// <param name="objName">物体名字</param>
    /// <param name="ComponentType">脚本类型</param>
    /// <returns></returns>
    public Component GetComponent(string objName, System.Type ComponentType)
    {
        Component Cmpent = null;
        GameObject b = GetGameObject(objName);
        if (b != null)
        {
            Cmpent = b.GetComponent(ComponentType);
            if (Cmpent == null) { Cmpent = b.AddComponent(ComponentType); }
        }
        return Cmpent;
    }
    #endregion


    #region 碰撞模块
    /// <summary>
    /// 触发回调函数
    /// </summary>
    private Dictionary<PZenum, DelegateTriggerFun> Trigger = new Dictionary<PZenum, DelegateTriggerFun>();
    /// <summary>
    /// 碰撞回调函数
    /// </summary>
    private Dictionary<PZenum, DelegateCollisionFun> Collision = new Dictionary<PZenum, DelegateCollisionFun>();
    public void ADDTriggerFun(PZenum tyeps, DelegateTriggerFun d)
    {
        Trigger[tyeps] = d;
    }
    public void ADDCollisionFun(PZenum tyeps, DelegateCollisionFun d)
    {
        Collision[tyeps] = d;
    }
    //触发
    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OnTriggerEnter(Collider collider) ");

        if (Trigger.ContainsKey(PZenum.OnTriggerEnter)) { this.Trigger[PZenum.OnTriggerEnter](gameObject, collider.gameObject, collider); }
    }
    public void OnTriggerExit(Collider collider)
    {
        Debug.Log("OnTriggerExit(Collider collider) ");
        if (Trigger.ContainsKey(PZenum.OnTriggerExit)) { this.Trigger[PZenum.OnTriggerExit](gameObject, collider.gameObject, collider); }
    }
    public void OnTriggerStay(Collider collider)
    { if (this.Trigger.ContainsKey(PZenum.OnTriggerStay)) { Trigger[PZenum.OnTriggerStay](gameObject, collider.gameObject, collider); } }


    //碰撞信息检测：
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter(Collider collider) " + gameObject + "  " + collision.gameObject);

        if (Collision.ContainsKey(PZenum.OnCollisionEnter))
        { Collision[PZenum.OnCollisionEnter](gameObject, collision.gameObject, collision); }
    }
    public void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit(Collider collider) ");
        if (Collision.ContainsKey(PZenum.OnCollisionExit)) { Collision[PZenum.OnCollisionExit](gameObject, collision.gameObject, collision); }
    }
    public void OnCollisionStay(Collision collision)
    {
        //  Debug.Log("OnCollisionStay(Collider collider) ");
        if (Collision.ContainsKey(PZenum.OnCollisionStay)) { Collision[PZenum.OnCollisionStay](gameObject, collision.gameObject, collision); }
    }
    #endregion
}
