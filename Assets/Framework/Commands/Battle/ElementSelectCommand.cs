using System.Collections;
using System.Collections.Generic;
using PbBattle;
using UnityEngine;

namespace Framework {
    public class ElementSelectCommand : AbstractCommand
    {
        public List<int> LockedDices = new List<int>();
        protected override void OnExecute()
        {

            Transform.FindObjectOfType<ElementController>().ClearElement();
            C2S_BattleAction BattleAction = new C2S_BattleAction();
            foreach (var item in LockedDices)//记录选择的骰子索引
            {
                BattleAction.LockedDices.Add(item);
            }
            BattleAction.ActionType = BattleActionType.BatAttack;
            BattleAction.TargetUID = this.GetModel<IBattleModel>().activeUID.Value;
            NetManager.Instance.Send<C2S_BattleAction>(BattleAction);
        }
    }
}