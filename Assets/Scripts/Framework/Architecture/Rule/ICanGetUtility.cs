using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework 
{
    public interface ICanGetUtility : IBelongToArchitecture
    {

    }
    public static class CanGetUtilityExtension
    {
        /// <summary>
        // this ICanGetUtility self ��C#�ķ�����չ����Ϊ����ĳ����̳���ICanGetUtility�ӿڣ������ʹ��this.GetUtility<T>()�����ø÷���
        // ��ˣ�ֻҪ��̳���ICanGetUtility�ӿڣ��Ϳ���ʹ�ø÷���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T GetUtility<T>(this ICanGetUtility self) where T : class,IUtility
        {
            return self.getArchitecture().GetUtility<T>();
        }
    }
}