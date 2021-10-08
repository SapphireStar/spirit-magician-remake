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
        IBattleModel battleModel;
        protected override void OnExecute()
        {
            battleModel = this.GetModel<IBattleModel>();

            if (InitRoundInfo != null)
                battleModel.curRoundInfo = (InitRoundInfo as S2C_EnterBattle).BattleRoundInfo;
            else
                Debug.Log("------³õÊ¼BattleRoundInfoÎª¿Õ------");
            battleModel.activeUID.Value = (InitRoundInfo as S2C_EnterBattle).BattleRoundInfo.ActiveUID;

            battleModel.player.UID = this.GetModel<IUserModel>().userBaseInfo.Uid;
            foreach (var item in battleModel.curRoundInfo.PlayerBattleInfos)
            {
                if (item.Uid != battleModel.player.UID)
                    battleModel.enemy.UID = item.Uid;
            }

            GameObject panel = this.GetSystem<IUISystem>().PushPanel(UIPanelType.SwitchSide);
            panel.GetComponent<SwitchSidePanel>().SetText("Game Start!");

        }
    }
}