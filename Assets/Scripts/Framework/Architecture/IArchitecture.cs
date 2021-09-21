using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IArchitecture
    {
        void RegisterSystem<T>(T system) where T : ISystem;
        void RegisterModel<T>(T system) where T : IModel;
        void RegisterUtility<T>(T utility) where T: IUtility;
        T GetUtility<T>() where T : class,IUtility;

        T GetModel<T>() where T : class,IModel;

        T GetSystem<T>() where T : class, ISystem;

        void SendCommand<T>() where T : ICommand, new();
        void SendCommand<T>(T command) where T : ICommand;

        void SendEvent<T>() where T : new();

        void SendEvent<T>(T e);
        IUnregister RegisterEvent<T>(Action<T> onEvent);
        void UnregisterEvent<T>(Action<T> onEvent);


    }
}