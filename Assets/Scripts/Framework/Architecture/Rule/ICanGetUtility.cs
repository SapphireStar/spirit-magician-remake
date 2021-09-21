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
        // this ICanGetUtility self 是C#的方法扩展，意为，若某个类继承了ICanGetUtility接口，则可以使用this.GetUtility<T>()来调用该方法
        // 因此，只要类继承了ICanGetUtility接口，就可以使用该方法
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