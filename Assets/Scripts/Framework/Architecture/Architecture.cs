
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{

    /// <summary>
    /// Architecture的生命周期：
    /// Get()/Register() -> makeSureArchitecture -> Init()/RegisterUtility()/RegisterModel() -> InitModels(在注册Model完毕后，开始对Model进行初始化，
    /// 如果需要更改注册的Model接口，则需要在Model被初始化之前进行，对Model接口注册的更改可以在OnRegisterPatch委托中进行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>,new()
    {
        private static T mArchitecture = null;
        /// <summary>
        /// 判断mModels中的Model对象是否已经被初始化
        /// </summary>
        private bool mInited;
        private List<IModel> mModels = new List<IModel>();
        private List<ISystem> mSystems = new List<ISystem>();
        public static Action<T> OnRegisterPatch = architecture=> {  };

        /// <summary>
        /// 用于初始化该模块管理类
        /// </summary>
        private static void makeSureArchitecture()
        {

            if (mArchitecture == null)
            {

                mArchitecture = new T();
                mArchitecture.Init();
                OnRegisterPatch?.Invoke(mArchitecture);

                foreach (var architectureModel in mArchitecture.mModels)
                {
                    architectureModel.Init();
                }
                mArchitecture.mModels.Clear();


                foreach (var architectureSystem in mArchitecture.mSystems)
                {
                    architectureSystem.Init();
                }
                mArchitecture.mSystems.Clear();

                mArchitecture.mInited = true;
            }
        }

        public static IArchitecture Instance
        {
            get
            {
                if (mArchitecture == null)
                {
                    makeSureArchitecture();
                }
                return mArchitecture;
            }
        }

        protected abstract void Init();

        private IOCcontainer mContainer = new IOCcontainer();
        public static T Get<T>() where T:class
        {
            makeSureArchitecture();
            return mArchitecture.mContainer.Get<T>();
        }
        public static void Register<T>(T Instance)
        {
            makeSureArchitecture();
            mArchitecture.mContainer.Register<T>(Instance);
        }

        /// <summary>
        /// 因为Model需要获取到Utility层的对象，因此需要创建一个IArchitecture接口，定义一个GetUtility类，并将该Architecture类赋值给Model对象，通过GetUtility方法获取Utility对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void RegisterModel<T>(T model) where T: IModel
        {
            model.setArchitecture(this);
            mContainer.Register<T>(model);

            //mModels中的Model被初始化后，又进行了RegisterModel，则直接调用初始化方法
            if (!mInited)
            {
                mModels.Add(model);
            }
            else
            {
                model.Init();
            }
        }
        public void RegisterUtility<T>(T utility) where T: IUtility
        {
            mContainer.Register<T>(utility);
        }
        public T GetUtility<T>() where T:class, IUtility
        {
            return mContainer.Get<T>();
        }

        public void RegisterSystem<T>(T system) where T : ISystem
        {
            system.setArchitecture(this);
            mContainer.Register<T>(system);

            //mModels中的Model被初始化后，又进行了RegisterModel，则直接调用初始化方法
            if (!mInited)
            {
                mSystems.Add(system);
            }
            else
            {
                system.Init();
            }
        }

        public T GetSystem<T>() where T : class,ISystem
        {
            return mContainer.Get<T>();
        }

        public T GetModel<T>() where T : class,IModel
        {
            return mContainer.Get<T>();
        }

        public void SendCommand<T>() where T : ICommand, new()
        {
            var command = new T();
            command.setArchitecture(this);
            command.Execute();
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            command.setArchitecture(this);
            command.Execute();
        }

        private ITypeEventSystem mTypeEventSystem = new TypeEventSystem();
        public void SendEvent<T>() where T : new()
        {
            mTypeEventSystem.Send<T>();  
        }

        public void SendEvent<T>(T e) 
        {
            mTypeEventSystem.Send<T>(e);
        }

        public IUnregister RegisterEvent<T>(Action<T> onEvent)
        {
           return mTypeEventSystem.Register<T>(onEvent);
        }

        public void UnregisterEvent<T>(Action<T> onEvent)
        {
            mTypeEventSystem.Unregister<T>(onEvent);
        }
    }
}