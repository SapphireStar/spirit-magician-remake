using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

namespace ElfWizard
{

    public class ReflectImpact : IImpactEffect
    {
        CharacterSkillSystem skillSystem;
        SkillData data;
        string[] level;
        string temp;
        SkillDeployer EnemyDeployer;
        Thread thread;
        public void Excecute(SkillDeployer deployer, int targetIndex)
        {
            data = deployer.SkillData;
            Elf_Monobehavior owner = deployer.SkillData.owner.GetComponent<Elf_Monobehavior>();
            deployer.GetComponent<TriggerDeployer>().isTriggered = false;
            EnemyDeployer = deployer.GetComponent<TriggerDeployer>().coll.GetComponent<SkillDeployer>();
            Transform[] opponent = data.attackTargets[targetIndex].ToArray(); //deployer.GetComponent<TriggerDeployer>().coll.GetComponentInParent<SkillDeployer>().SkillData.owner.transform;
            for (int i = 0; i < EnemyDeployer.SkillData.attackTargetTags.Length; i++)
            {
                for (int j = 0; j < EnemyDeployer.SkillData.attackTargets[i].Count; j++)
                {
                    EnemyDeployer.SkillData.attackTargets[i][j] = opponent[j];
                }

            }
            temp = EnemyDeployer.SkillData.atkRatio[EnemyDeployer.SkillData.atkRatio.Length - 1];
            EnemyDeployer.SkillData.atkRatio[EnemyDeployer.SkillData.atkRatio.Length - 1] = level[owner.Level].ToString() +';'+ level[owner.Level].ToString()+';' + level[owner.Level].ToString();

            thread = new Thread(new ThreadStart(setRatio));
            thread.Start();

        }

        public void InitEffect(float level1, float level2, float level3)
        {
            level = new string[] { level1.ToString(), level2.ToString(), level3.ToString() };
        }
        private void setRatio()
        {
            while (!EnemyDeployer.isDeployed) { Thread.Sleep(1000); }
            EnemyDeployer.SkillData.atkRatio[EnemyDeployer.SkillData.atkRatio.Length - 1] = temp;
            thread.Abort();
        }

    }

}
