using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace ElfWizard
{
    public class SetupBattleSceneCommand : AbstractCommand, ICommand
    {
        public NewPlayerController player;
        public NewPlayerController enemy;
        protected override void OnExecute()
        {
            this.GetModel<IBattleModel>().players.Add(player);
            this.GetModel<IBattleModel>().players.Add(enemy);
            this.GetModel<IBattleModel>().player = player;
            this.GetModel<IBattleModel>().enemy = enemy;
            this.SendEvent<SetupBattleSceneEvent>();
        }
    }
}