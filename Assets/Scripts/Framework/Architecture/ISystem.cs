using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{

    public interface ISystem : IBelongToArchitecture,ICanSetArchitecture,ICanGetUtility,ICanGetModel,ICanSendEvent,ICanRegisterEvent
    {
        void Init();
    }
    public abstract class AbstractSystem : ISystem
    {
        IArchitecture mArchitecture;
        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return mArchitecture;
        }
        void ICanSetArchitecture.setArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        void ISystem.Init()
        {
            OnInit();
        }
        protected abstract void OnInit();


    }
}