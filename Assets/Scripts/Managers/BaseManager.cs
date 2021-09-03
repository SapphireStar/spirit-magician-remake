using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard.Manager
{
    public class BaseManager:MonoBehaviour
    {
        protected GameFacade facede;

        public virtual void OnInit() { }

        public virtual void OnDestroy() { }
    }
}
