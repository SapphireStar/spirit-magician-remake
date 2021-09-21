using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface ICanGetModel : IBelongToArchitecture
    {

    }
    public static class CanGetModelExtension
    {
        public static T GetModel<T>(this ICanGetModel self) where T:class, IModel
        {
            return self.getArchitecture().GetModel<T>();
        }
    }
}