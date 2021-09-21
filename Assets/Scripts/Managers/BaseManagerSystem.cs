using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

namespace ElfWizard.Manager
{
    public abstract class BaseManagerSystem : ISystem
    {

        IArchitecture mArchitecture;

        public abstract void OnDestroy();

        public void Init()
        {
            OnInit();
        }

        void ICanSetArchitecture.setArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        public IArchitecture getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
        protected abstract void OnInit();
    }
}
