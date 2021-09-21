using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard.Util
{
    public class IOCcontainer
    {
        private Dictionary<Type, object> mInstance = new Dictionary<Type, object>();
        public void Register<T>(T Instance)
        {
            var key = typeof(T);
            if (mInstance.ContainsKey(key))
            {
                mInstance[key] = Instance;
            }
            else
            {
                mInstance.Add(key, Instance);
            }
        }


        public T Get<T>() where T : class
        {
            Type key = typeof(T);
            if (mInstance.TryGetValue(key, out object RetValue))
            {
                return RetValue as T;
            }
            return null;
        }
    }
}