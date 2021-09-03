using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class DefensiveImpact : IImpactEffect
    {
        SkillData data;
        float[] level;
        NewPlayerController target;
        public void Excecute(SkillDeployer deployer, int targetIndex)
        {
            data = deployer.SkillData;
            target = data.attackTargets[targetIndex][0].GetComponent<NewPlayerController>();
            target.Defence += level[data.owner.GetComponent<Elf_Monobehavior>().Level];


        }

        public void InitEffect(float level1, float level2, float level3)
        {
            level = new float[3];
            level[0] = level1;
            level[1] = level2;
            level[2] = level3;
        }
    }

}
