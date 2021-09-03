using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class SacrifyImpact : IImpactEffect
    {
        Elf_Monobehavior Owner;
        float[] level;
        public void Excecute(SkillDeployer deployer, int targetIndex)
        {
            Owner = deployer.SkillData.owner.GetComponent<Elf_Monobehavior>();
            Owner.GetHit(level[Owner.Level]);
            
        }

        public void InitEffect(float level1, float level2, float level3)
        {
            level = new float[] { level1, level2, level3 };
        }
    }
}
