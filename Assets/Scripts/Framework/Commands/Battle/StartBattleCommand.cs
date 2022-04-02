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
        S2C_EnterBattle s2C_EnterBattle;
        protected override void OnExecute()
        {
            s2C_EnterBattle = InitRoundInfo as S2C_EnterBattle;

            battleModel = this.GetModel<IBattleModel>();
            if (InitRoundInfo != null)
                battleModel.curRoundInfo = s2C_EnterBattle.BattleRoundInfo;
            else
                Debug.Log("------³õÊ¼BattleRoundInfoÎª¿Õ------");
            battleModel.activeUID.Value = s2C_EnterBattle.BattleRoundInfo.ActiveUID;

            foreach (var item in s2C_EnterBattle.BattleRoundInfo.PlayerBattleInfos)
            {
                battleModel.playerBattleInfos.Add(item);
            }

            battleModel.player.UID = this.GetModel<IUserModel>().userBaseInfo.Uid;
            foreach (var item in battleModel.curRoundInfo.PlayerBattleInfos)
            {
                if (item.Uid != battleModel.player.UID)
                    battleModel.enemy.UID = item.Uid;
            }

            this.SendEvent<GameStartEvent>();
            GameObject panel = this.GetSystem<IUISystem>().PushPanel(UIPanelType.SwitchSide);
            panel.GetComponent<SwitchSidePanel>().SetText("Game Start!");

        }
    }
}