using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard.Manager
{
    public class BaseManagerNonMono
    {
        protected GameFacade facade;
        public BaseManagerNonMono(GameFacade facade)
        {
            this.facade = facade;
        }
        public virtual void OnInit() { }

        public virtual void OnDestroy() { }
    }
}
