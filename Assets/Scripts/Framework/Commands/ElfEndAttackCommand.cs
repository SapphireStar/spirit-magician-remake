using System.Collections;
using System.Collections.Generic;
using Framework;
using ElfWizard.Model;
using ElfWizard.Util;
using UnityEngine;

namespace ElfWizard.Commands
{
    public class ElfEndAttackCommand : AbstractCommand,ICommand
    {
        public void Execute()
        {
            OnExecute();
        }

        protected override void OnExecute()
        {
            Framework.ElfWizardArch.Get<BattleModel>().currentTurnElfs.Value--;
        }
    }
}