using UnityEngine;
using XLua;
using System.Collections.Generic;
using System.Collections;
using System;

namespace XLuaTest
{
    [LuaCallCSharp]
    public class Coroutine_Runner : MonoBehaviour
    {
        public void YieldAndCallback(object to_yield, Action callback)
        {
            Debug.Log("��ʼЭͬ");
            // ����Э�̣��ص�callback
            StartCoroutine(CoBody(to_yield, callback));
        }

        private IEnumerator CoBody(object to_yield, Action callback)
        {
            Debug.Log("ִ�С�������");
            if (to_yield is IEnumerator)
                yield return StartCoroutine((IEnumerator)to_yield);
            else
                yield return to_yield;
            callback();
        }
    }



    public static class CoroutineConfig
    {
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp
        {
            get
            {
                return new List<Type>()
            {
                typeof(WaitForSeconds),
                typeof(WWW)
            };
            }
        }
    }
}
