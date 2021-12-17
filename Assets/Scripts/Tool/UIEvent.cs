using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI  界面事件
/// </summary>
public class UIEvent : EventTrigger
{
    
    public static UIEvent ADD(GameObject O)
    {
        UIEvent d = O.GetComponent<UIEvent>();
        if (d != null)
            return d;
        return O.AddComponent<UIEvent>();
    }

    /// <summary>
    /// 判断是否为枚举值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsChooseEnum(int key)
    {
        foreach (int item in Enum.GetValues(typeof(EventTriggerType)))
        {
            if (key==item)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 添加事件回调函数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="Fun"></param>
    public void AddFunction(EventTriggerType type, System.Action<GameObject> Fun)
    {
        keyActions[type] = Fun;
    }
    /// <summary>
    /// lua 删除对象时需要触发的方法
    /// </summary>
    public DelegateFun _OnDestroy = null;
    public void OnDestroy()
    {
        if (_OnDestroy != null) { _OnDestroy(); }

        if (keyActions != null)
        {
            List<EventTriggerType> ls = new List<EventTriggerType>(keyActions.Keys);
            for (int i = 0; i < ls.Count; i++) { keyActions[ls[i]] = null; }
            keyActions.Clear();
        } 
    }

    
    /// <summary>
    /// 函数回调字典
    /// </summary>
    private Dictionary<EventTriggerType, System.Action<GameObject>> keyActions = new Dictionary<EventTriggerType, System.Action<GameObject>>();
    //
    // 摘要:
    //     Called before a drag is started.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnBeginDrag(PointerEventData eventData) 
    {
        base.OnBeginDrag(eventData);
        if (keyActions.ContainsKey(EventTriggerType.BeginDrag)) { keyActions[EventTriggerType.BeginDrag](gameObject); } 
    }
    //
    // 摘要:
    //     Called by the EventSystem when a Cancel event occurs.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnCancel(BaseEventData eventData) 
    {
        base.OnCancel(eventData);
        if (keyActions.ContainsKey(EventTriggerType.Cancel)) { keyActions[EventTriggerType.Cancel](gameObject); } 
    }
    //
    // 摘要:
    //     Called by the EventSystem when a new object is being selected.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnDeselect(BaseEventData eventData) 
    {
        base.OnDeselect(eventData);
        if (keyActions.ContainsKey(EventTriggerType.Deselect)) { keyActions[EventTriggerType.Deselect](gameObject); }
    }
    //
    // 摘要:
    //     Called by the EventSystem every time the pointer is moved during dragging.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnDrag(PointerEventData eventData) 
    {
        base.OnDrag(eventData);
        if (keyActions.ContainsKey(EventTriggerType.Drag)) { keyActions[EventTriggerType.Drag](gameObject); } 
    }
    //
    // 摘要:
    //     Called by the EventSystem when an object accepts a drop.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnDrop(PointerEventData eventData) 
    {
        base.OnDrop(eventData);
        if (keyActions.ContainsKey(EventTriggerType.Drop)) { keyActions[EventTriggerType.Drop](gameObject); }
    }
    //
    // 摘要:
    //     Called by the EventSystem once dragging ends.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnEndDrag(PointerEventData eventData) 
    {
        base.OnEndDrag(eventData);
        if (keyActions.ContainsKey(EventTriggerType.EndDrag)) { keyActions[EventTriggerType.EndDrag](gameObject); }
    }
    //
    // 摘要:
    //     Called by the EventSystem when a drag has been found, but before it is valid
    //     to begin the drag.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnInitializePotentialDrag(PointerEventData eventData) 
    {
        base.OnInitializePotentialDrag(eventData);
        if (keyActions.ContainsKey(EventTriggerType.InitializePotentialDrag)) { keyActions[EventTriggerType.InitializePotentialDrag](gameObject); }
    }
    //
    // 摘要:
    //     Called by the EventSystem when a Move event occurs.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
        if (keyActions.ContainsKey(EventTriggerType.InitializePotentialDrag)) { keyActions[EventTriggerType.InitializePotentialDrag](gameObject); } 
    }
    //
    // 摘要:
    //     Called by the EventSystem when a Click event occurs.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnPointerClick(PointerEventData eventData) 
    {
        base.OnPointerClick(eventData);
        if (keyActions.ContainsKey(EventTriggerType.PointerClick)) { keyActions[EventTriggerType.PointerClick](gameObject); }
    }
    //
    // 摘要:
    //     Called by the EventSystem when a PointerDown event occurs.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnPointerDown(PointerEventData eventData) 
    {
        base.OnPointerDown(eventData);
        if (keyActions.ContainsKey(EventTriggerType.PointerDown)) { keyActions[EventTriggerType.PointerDown](gameObject); }
    }
    //
    // 摘要:
    //     Called by the EventSystem when the pointer enters the object associated with
    //     this EventTrigger.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnPointerEnter(PointerEventData eventData) 
    {
        base.OnPointerEnter(eventData);
        if (keyActions.ContainsKey(EventTriggerType.PointerEnter)) { keyActions[EventTriggerType.PointerEnter](gameObject); } 
    }
    //
    // 摘要:
    //     Called by the EventSystem when the pointer exits the object associated with this
    //     EventTrigger.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnPointerExit(PointerEventData eventData) 
    {
        base.OnPointerExit(eventData);
        if (keyActions.ContainsKey(EventTriggerType.PointerExit)) { keyActions[EventTriggerType.PointerExit](gameObject); }
    }
    //
    // 摘要:
    //     Called by the EventSystem when a PointerUp event occurs.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnPointerUp(PointerEventData eventData) 
    {
        base.OnPointerUp(eventData);
        if (keyActions.ContainsKey(EventTriggerType.PointerUp)) { keyActions[EventTriggerType.PointerUp](gameObject); }
    }
    //
    // 摘要:
    //     Called by the EventSystem when a Scroll event occurs.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnScroll(PointerEventData eventData) 
    {
        base.OnScroll(eventData);
        if (keyActions.ContainsKey(EventTriggerType.Scroll)) { keyActions[EventTriggerType.Scroll](gameObject); }
    }
    //
    // 摘要:
    //     Called by the EventSystem when a Select event occurs.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnSelect(BaseEventData eventData) 
    {
        base.OnSelect(eventData);
        if (keyActions.ContainsKey(EventTriggerType.Select)) { keyActions[EventTriggerType.Select](gameObject); } 
    }
    //
    // 摘要:
    //     Called by the EventSystem when a Submit event occurs.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnSubmit(BaseEventData eventData) 
    {
        base.OnSubmit(eventData);
        if (keyActions.ContainsKey(EventTriggerType.Submit)) { keyActions[EventTriggerType.Submit](gameObject); } 
    }
    //
    // 摘要:
    //     Called by the EventSystem when the object associated with this EventTrigger is
    //     updated.
    //
    // 参数:
    //   eventData:
    //     Current event data.
    public override void OnUpdateSelected(BaseEventData eventData) 
    {
        base.OnUpdateSelected(eventData);
        if (keyActions.ContainsKey(EventTriggerType.UpdateSelected)) { keyActions[EventTriggerType.UpdateSelected](gameObject); }
    }
}
