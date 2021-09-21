using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Framework
{
    public class Singleton<T> where T : Singleton<T>
    {
        private static T mInstance;

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    Type type = typeof(T);
                    var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if (ctor == null)
                    {
                        throw new Exception("Non Public Constructor not found: " + type.Name);
                    }
                    mInstance = ctor.Invoke(null) as T;

                }
                return mInstance;
            }
        }
    }
}