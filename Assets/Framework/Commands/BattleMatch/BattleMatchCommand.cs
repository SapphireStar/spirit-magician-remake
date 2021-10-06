using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PbMatch;

namespace Framework
{
    public class BattleMatchCommand : AbstractCommand
    {
        public PbBattle.BattleType BattleType = PbBattle.BattleType.BtPvp;
        protected override void OnExecute()
        {
            NetManager.Instance.Send<C2S_BattleMatch>(new C2S_BattleMatch() { BattleType =this.BattleType });
        }
    }
}