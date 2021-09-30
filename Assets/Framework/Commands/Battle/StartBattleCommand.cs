using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PbBattle;
using Google.Protobuf;
using ElfWizard;

namespace Framework
{
    public class StartBattleCommand : AbstractCommand
    {
        public IMessage InitRoundInfo;
        protected override void OnExecute()
        {
            if (InitRoundInfo != null)
                this.GetModel<IBattleModel>().curRoundInfo = (InitRoundInfo as S2C_EnterBattle).BattleRoundInfo;
            else
                Debug.Log("------��ʼBattleRoundInfoΪ��------");

            GameObject panel = this.GetSystem<IUISystem>().PushPanel(UIPanelType.SwitchSide);
            panel.GetComponent<SwitchSidePanel>().SetText("Game Start!");
        }
    }
}