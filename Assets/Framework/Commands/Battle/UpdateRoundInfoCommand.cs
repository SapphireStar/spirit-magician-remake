using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard;
using PbBattle;

namespace Framework
{
    public class UpdateRoundInfoCommand : AbstractCommand,ICommand
    {
        public int activeUID;
        public List<int> specialEffects;
        public BattleRoundInfo curRoundInfo;
        public BattleRoundInfo nextRoundInfo;
        IBattleModel battleModel;
        protected override void OnExecute()
        {
            battleModel = this.GetModel<IBattleModel>();
            battleModel.curRoundInfo = curRoundInfo;
            battleModel.nextRoundInfo = nextRoundInfo;
            battleModel.activeUID.Value = activeUID;

        }
    }
}