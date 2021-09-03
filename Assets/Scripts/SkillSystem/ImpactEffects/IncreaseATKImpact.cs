using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class IncreaseATKImpact:IImpactEffect
    {
        SkillData data;
        float[] level;
        Elf_Monobehavior[] target;
        public void Excecute(SkillDeployer deployer, int targetIndex)
        {
            data = deployer.SkillData;
            target = new Elf_Monobehavior[data.attackTargets[targetIndex].Count];
            for (int i = 0; i < data.attackTargets[targetIndex].Count; i++)
            {
                target[i] = data.attackTargets[targetIndex][i].GetComponent<Elf_Monobehavior>();
                target[i].Damage += level[target[i].Level];
            }


          
        }

        public void InitEffect(float level1, float level2, float level3)
        {
            level = new float[] { level1, level2, level3 };  
        }
    }

}
