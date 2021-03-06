using UnityEngine;
using System.Collections;
using ElfWizard.Manager;
using Framework;

namespace ElfWizard
{
    public class BasePanel : MonoBehaviour, IController
    {
        protected IUISystem uiManager;
/*        protected UIManager uiManager;
        private GameFacade gameFacade;
        public UIManager UImanager
        {
            set { uiManager = value; }
        }
        public GameFacade GameFacade { set { gameFacade = value; } }*/
        /// <summary>
        /// 界面被显示出来
        /// </summary>
        public virtual void OnEnter()
        {
            uiManager = this.GetSystem<IUISystem>();
        }
        protected void PlayClickSound()
        {
            this.GetSystem<IAudioSystem>().PlayNormalSound("ButtonClick");
        }
        /// <summary>
        /// 界面暂停
        /// </summary>
        public virtual void OnPause()
        {

        }

        /// <summary>
        /// 界面继续
        /// </summary>
        public virtual void OnResume()
        {

        }

        /// <summary>
        /// 界面不显示,退出这个界面，界面被关系
        /// </summary>
        public virtual void OnExit()
        {

        }

        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
    }
}