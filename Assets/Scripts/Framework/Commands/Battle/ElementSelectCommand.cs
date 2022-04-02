using System.Collections;
using System.Collections.Generic;
using PbBattle;
using UnityEngine;
using PbSpirit;
using System;

namespace Framework {
    public class ElementSelectCommand : AbstractCommand
    {
        public List<int> LockedDices = new List<int>();
        protected override void OnExecute()
        {
            string s = "";
            Transform.FindObjectOfType<ElementController>().ClearElement(new StartAttackEvent());
            C2S_BattleAction BattleAction = new C2S_BattleAction();
            foreach (var item in LockedDices)//记录选择的骰子索引
            {
                s.Insert(s.Length,Enum.GetName(typeof(SpecialistType),(SpecialistType)item)+" ");
                BattleAction.LockedDices.Add(item);
            }
            BattleAction.RoundIndex = this.GetModel<IBattleModel>().curRoundInfo.RoundIndex;
            BattleAction.ActionType = BattleActionType.BatAttack;
            BattleAction.TargetUID = this.GetModel<IBattleModel>().activeUID.Value;
            Debug.Log("------Send BattleAction.Attack------");
            Debug.Log("send lockedDices: " + s);
            NetManager.Instance.Send<C2S_BattleAction>(BattleAction);
        }
    }
}