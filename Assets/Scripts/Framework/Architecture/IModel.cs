using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IModel : IBelongToArchitecture,ICanSetArchitecture,ICanGetUtility,ICanSendEvent
    {
        void Init();
    }

    public abstract class AbstractModel : IModel
    {
        private IArchitecture mArchitecture;
        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return mArchitecture;
        }
        void ICanSetArchitecture.setArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        /// <summary>
        /// �ӿڵ���ʽʵ�֣���������Init�����ķ���
        /// </summary>
        void IModel.Init()
        {
            OnInit();
        }

        protected abstract void OnInit();



    }
}