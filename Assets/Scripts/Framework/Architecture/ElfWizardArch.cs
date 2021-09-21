using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Util;
using ElfWizard.Model;


namespace Framework
{
    public class ElfWizardArch : Architecture<ElfWizardArch>
    {
        protected override void Init()
        {
            Register<BattleModel>(new BattleModel());
        }
    }
}