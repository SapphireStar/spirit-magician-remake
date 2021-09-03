using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class CostSPImpact : IImpactEffect
    {
        public void Excecute(SkillDeployer deployer, int targetIndex)
        {
            var status = deployer.SkillData.owner.GetComponent<Elf_Monobehavior>();


        }

        public void InitEffect(float level1, float level2, float level3)
        {
            
        }
    }

}