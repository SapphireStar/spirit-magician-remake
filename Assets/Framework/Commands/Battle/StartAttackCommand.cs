using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard;

namespace Framework
{
    public class StartAttackCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            Debug.Log("trigger");
            this.GetModel<IBattleModel>().RemainAttackElfs.Value = this.GetModel<IBattleModel>().currentTurn.PlayerElfs.Count;
            if (this.GetModel<IBattleModel>().RemainAttackElfs.Value == 0)
            {
                this.GetSystem<IBattleSystem>().StartAttack(0);
            }
        }
    }
}