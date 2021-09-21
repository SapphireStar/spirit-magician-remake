using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface ICanRegisterEvent:IBelongToArchitecture
    {

    }

    public static class CanRegisterEventExtension
    {
        public static IUnregister RegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent)
        {
           return self.getArchitecture().RegisterEvent<T>(onEvent);
        }
        public static void UnregisterEvent<T>(this ICanRegisterEvent self,Action<T> onEvent)
        {
            self.getArchitecture().UnregisterEvent<T>(onEvent);
        }

    }
}