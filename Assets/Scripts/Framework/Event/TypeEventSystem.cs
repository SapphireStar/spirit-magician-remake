using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface ITypeEventSystem
    {
        void Send<T>() where T : new();
        void Send<T>(T e);
        IUnregister Register<T>(Action<T> onEvent);
        void Unregister<T>(Action<T> onEvent);

    }
    public interface IUnregister
    {
        void Unregister();
    }

    /// <summary>
    //用于记录每一个被注册的具体事件委托，方便于未来需要对事件中特定的事件委托进行注销
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypeEventSystemUnregister<T> : IUnregister
    {
        public ITypeEventSystem TypeEventSystem;
        public Action<T> OnEvent;
        public void Unregister()
        {
            TypeEventSystem.Unregister<T>(OnEvent);
            TypeEventSystem = null;
            OnEvent = null;
        }
    }

    public class UnRegisterOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnregister> mUnregisters = new HashSet<IUnregister>();
        public void AddUnregister(IUnregister Unregister)
        {
            mUnregisters.Add(Unregister);
        }

        private void OnDestroy()
        {
            foreach (var register in mUnregisters)
            {
                register.Unregister();
            }
            mUnregisters.Clear();
        }
    }
    public static class UnregisterExtension
    {
        public static void UnregisterWhenDestroy(this IUnregister self, GameObject gameObject)
        {
            UnRegisterOnDestroyTrigger trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();
            if (trigger == null)
            {
               trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }
            trigger.AddUnregister(self);
        }
    }
    public class TypeEventSystem : ITypeEventSystem
    {
        public interface IRegistrations
        {

        }
        public class Registrations<T> : IRegistrations
        {
            public Action<T> OnEvent = e => { };
        }
        Dictionary<Type, IRegistrations> mRegistrations = new Dictionary<Type, IRegistrations>();
        public void Send<T>() where T : new()
        {
           
            T e = new T();
            Send<T>(e);
        }

        public void Send<T>(T e)
        {
            Type type = typeof(T);
            IRegistrations registrations;
            if (mRegistrations.TryGetValue(type, out registrations))
            {
                (registrations as Registrations<T>).OnEvent?.Invoke(e);
            }
        }
        public IUnregister Register<T>(Action<T> onEvent)
        {
            Type type = typeof(T);
            IRegistrations registrations;
            if (mRegistrations.TryGetValue(type, out registrations))
            {

            }
            else
            {
                registrations = new Registrations<T>();
                mRegistrations.Add(type, registrations);

            }
            (registrations as Registrations<T>).OnEvent += onEvent;

            return new TypeEventSystemUnregister<T>()
            {
                TypeEventSystem = this,
                OnEvent = onEvent
            };
        }
        public void Unregister<T>(Action<T> onEvent)
        {
            Type type = typeof(T);
            IRegistrations registrations;
            if (mRegistrations.TryGetValue(type, out registrations))
            {
                (registrations as Registrations<T>).OnEvent -= onEvent;
            }
        }
    }

}
