using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard.Util
{
    public class Event<T> where T : Event<T>
    {
        static Action mOnAction;
        public static void Register(Action action)
        {
            mOnAction += action;
        }
        public static void Unregister(Action action)
        {
            mOnAction -= action;
        }

        public static void Trigger()
        {
            mOnAction?.Invoke();//��ί�в�Ϊ�գ���ִ��ί��
        }
    }
}