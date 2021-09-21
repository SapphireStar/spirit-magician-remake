
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{

    /// <summary>
    /// Architecture���������ڣ�
    /// Get()/Register() -> makeSureArchitecture -> Init()/RegisterUtility()/RegisterModel() -> InitModels(��ע��Model��Ϻ󣬿�ʼ��Model���г�ʼ����
    /// �����Ҫ����ע���Model�ӿڣ�����Ҫ��Model����ʼ��֮ǰ���У���Model�ӿ�ע��ĸ��Ŀ�����OnRegisterPatchί���н���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>,new()
    {
        private static T mArchitecture = null;
        /// <summary>
        /// �ж�mModels�е�Model�����Ƿ��Ѿ�����ʼ��
        /// </summary>
        private bool mInited;
        private List<IModel> mModels = new List<IModel>();
        private List<ISystem> mSystems = new List<ISystem>();
        public static Action<T> OnRegisterPatch = architecture=> {  };

        /// <summary>
        /// ���ڳ�ʼ����ģ�������
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
        /// ��ΪModel��Ҫ��ȡ��Utility��Ķ��������Ҫ����һ��IArchitecture�ӿڣ�����һ��GetUtility�࣬������Architecture�ำֵ��Model����ͨ��GetUtility������ȡUtility����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void RegisterModel<T>(T model) where T: IModel
        {
            model.setArchitecture(this);
            mContainer.Register<T>(model);

            //mModels�е�Model����ʼ�����ֽ�����RegisterModel����ֱ�ӵ��ó�ʼ������
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

            //mModels�е�Model����ʼ�����ֽ�����RegisterModel����ֱ�ӵ��ó�ʼ������
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