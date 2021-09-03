using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class HealDirectImpact : IImpactEffect
    {
        float[] level;
        SkillData data;
        public void Excecute(SkillDeployer deployer, int targetIndex)
        {
            data = deployer.SkillData;
            for (int i = 0; i < data.attackTargets[targetIndex].Count; i++)
            {
                data.attackTargets[targetIndex][i].GetComponent<NewPlayerController>().health+=level[data.owner.GetComponent<Elf_Monobehavior>().Level];
            }

        }

        public void InitEffect(float level1, float level2, float level3)
        {
            level = new float[] { level1, level2, level3 };
        }
    }

}