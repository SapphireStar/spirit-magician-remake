using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class IncreaseFireImpact : IImpactEffect
    {
        SkillData data;
        float[] level;//后期参数将由skilldata提供，这里暂时给定值
        public void Excecute(SkillDeployer deployer, int targetIndex)
        {
            data = deployer.SkillData;
            Elf_Monobehavior[] target = new Elf_Monobehavior[data.attackTargets[targetIndex].Count];
            for (int i = 0; i < data.attackTargets[targetIndex].Count; i++)
            {
            target[i] = data.attackTargets[targetIndex][i].GetComponent<Elf_Monobehavior>();
            }
            foreach (var item in target)
            {
                item.FireDamage += level[item.Level];
            }

            
        }

        public void InitEffect(float level1, float level2, float level3)
        {
            level = new float[] { level1, level2, level3 };  
        }
    }

}
