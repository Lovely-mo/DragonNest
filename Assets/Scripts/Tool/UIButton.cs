using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
 
public class UIButton : Selectable
{
    private Dictionary<EventTriggerType, System.Action<GameObject>> keyActions = new Dictionary<EventTriggerType, System.Action<GameObject>>();

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
    public new void OnDestroy()
    {
        if (_OnDestroy != null)
        {
            _OnDestroy();
        }
        if (keyActions != null)
        {
            List<EventTriggerType> ls = new List<EventTriggerType>(keyActions.Keys);
            for (int i = 0; i < ls.Count; i++)
            {
                keyActions[ls[i]] = null;
            }
            keyActions.Clear();
        }
    }
    //
    // 摘要:
    //     Unset selection and transition to appropriate state.
    //
    // 参数:
    //   eventData:
    //     The eventData usually sent by the EventSystem.
    public override void OnDeselect(BaseEventData eventData) 
    { 
        base.OnDeselect(eventData); 
        if (keyActions.ContainsKey(EventTriggerType.Deselect)) { keyActions[EventTriggerType.Deselect](gameObject); }
    }
    //
    // 摘要:
    //     Determine in which of the 4 move directions the next selectable object should
    //     be found.
    //
    // 参数:
    //   eventData:
    //     The EventData usually sent by the EventSystem.
    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData); 
        if (keyActions.ContainsKey(EventTriggerType.Move)) { keyActions[EventTriggerType.Move](gameObject); }
    }
    //
    // 摘要:
    //     Evaluate current state and transition to pressed state.
    //
    // 参数:
    //   eventData:
    //     The EventData usually sent by the EventSystem.
    public override void OnPointerDown(PointerEventData eventData) 
    {
        base.OnPointerDown(eventData); 
        if (keyActions.ContainsKey(EventTriggerType.PointerDown)) { keyActions[EventTriggerType.PointerDown](gameObject); }
    }
    //
    // 摘要:
    //     Evaluate current state and transition to appropriate state.
    //
    // 参数:
    //   eventData:
    //     The EventData usually sent by the EventSystem.
    public override void OnPointerEnter(PointerEventData eventData) 
    {
        base.OnPointerEnter(eventData); 
        if (keyActions.ContainsKey(EventTriggerType.PointerEnter)) { keyActions[EventTriggerType.PointerEnter](gameObject); }
    }
    //
    // 摘要:
    //     Evaluate current state and transition to normal state.
    //
    // 参数:
    //   eventData:
    //     The EventData usually sent by the EventSystem.
    public override void OnPointerExit(PointerEventData eventData) 
    {
        base.OnPointerExit(eventData); 
        if (keyActions.ContainsKey(EventTriggerType.PointerExit)) { keyActions[EventTriggerType.PointerExit](gameObject); }
    }
    //
    // 摘要:
    //     Evaluate eventData and transition to appropriate state.
    //
    // 参数:
    //   eventData:
    //     The EventData usually sent by the EventSystem.
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData); 
        if (keyActions.ContainsKey(EventTriggerType.PointerUp)) { keyActions[EventTriggerType.PointerUp](gameObject); }
    }
    //
    // 摘要:
    //     Set selection and transition to appropriate state.
    //
    // 参数:
    //   eventData:
    //     The EventData usually sent by the EventSystem.
    public override void OnSelect(BaseEventData eventData) 
    {
        base.OnSelect(eventData); 
        if (keyActions.ContainsKey(EventTriggerType.Select)) { keyActions[EventTriggerType.Select](gameObject); }
    }
}