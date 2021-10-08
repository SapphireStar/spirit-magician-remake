using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard;
using Framework;
using System.Threading;

namespace Framework
{
    public class BattleUIController : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
        IUISystem uiSystem;
        IBattleModel battleModel;
        private void Awake()
        {
            battleModel = this.GetModel<IBattleModel>();
            uiSystem = this.GetSystem<IUISystem>();
            uiSystem.PushPanel(UIPanelType.BattleUI);
            this.RegisterEvent<EndAttackEvent>(PushSwitchSidePanel);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {

                uiSystem.PushPanel(UIPanelType.Command);
            }
        }

        void PushSwitchSidePanel(EndAttackEvent e)
        {
            StartCoroutine(WaitForSecond(1));
            GameObject panel = uiSystem.PushPanel(UIPanelType.SwitchSide);
            panel.GetComponent<SwitchSidePanel>().SetText("Switch To "+e.nextRound);


        }
        IEnumerator WaitForSecond(float time)
        {
            yield return new WaitForSeconds(time);
        }
        private void OnDestroy()
        {
            this.UnregisterEvent<EndAttackEvent>(PushSwitchSidePanel);
        }

    }
}