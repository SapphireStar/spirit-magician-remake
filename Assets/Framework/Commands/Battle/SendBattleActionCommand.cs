using System.Collections;
using System.Collections.Generic;
using PbBattle;
using UnityEngine;

namespace Framework
{
    public class SendBattleActionCommand : AbstractCommand
    {
        public BattleActionType battleActionType;
        public int[] diceLocked;
        protected override void OnExecute()
        {

        }
    }
}