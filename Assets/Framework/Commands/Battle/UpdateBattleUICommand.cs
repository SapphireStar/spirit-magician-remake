using System.Collections;
using System.Collections.Generic;
using PbBattle;
using UnityEngine;

namespace Framework
{

    public class UpdateBattleUICommand : AbstractCommand
    {
        public PlayerBattleInfo PlayerBattleInfo;
        public PlayerBattleInfo EnemyBattleInfo;
        protected override void OnExecute()
        {
            
        }
    }
}