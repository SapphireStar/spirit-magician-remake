using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard;
namespace Framework
{
    public class UpdateBattleInfoCommand : AbstractCommand
    {
        public NewPlayerController player;
        public NewPlayerController enemy;
        protected override void OnExecute()
        {

            this.GetModel<IBattleModel>().player = this.player;
            this.GetModel<IBattleModel>().enemy = this.enemy;
            this.SendEvent<RequireBattleInfoEvent>();
        }
    }
}