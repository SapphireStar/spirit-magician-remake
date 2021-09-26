using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard;
using Framework;

namespace Framework
{
    public class BattleUIController : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
        IUISystem uiSystem;
        private void Awake()
        {
            uiSystem = this.GetSystem<IUISystem>();
            uiSystem.PushPanel(UIPanelType.BattleUI);
            this.SendCommand<UpdateBattleUICommand>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {

                uiSystem.PushPanel(UIPanelType.Command);
            }
        }
    }
}