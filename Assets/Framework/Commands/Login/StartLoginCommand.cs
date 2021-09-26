using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

namespace ElfWizard.Commands 
{
    public class StartLoginCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<IUISystem>().ToString();
            (this.GetSystem<IUISystem>() as UIManager).PushPanel(UIPanelType.Start);
        }
    }
}