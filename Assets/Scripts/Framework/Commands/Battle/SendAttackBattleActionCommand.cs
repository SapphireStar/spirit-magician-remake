using System.Collections;
using System.Collections.Generic;
using PbBattle;
using UnityEngine;

namespace Framework
{
    public class SendAttackBattleActionCommand : AbstractCommand
    {
        public C2S_BattleAction BattleAction = new C2S_BattleAction();
        public int[] diceLocked;
        protected override void OnExecute()
        {
            BattleAction.RoundIndex = this.GetModel<IBattleModel>().curRoundInfo.RoundIndex;
            BattleAction.ActionType = BattleActionType.BatAttack;
            BattleAction.TargetUID = this.GetModel<IBattleModel>().activeUID.Value;
            NetManager.Instance.Send<C2S_BattleAction>(BattleAction);
        }
    }
}