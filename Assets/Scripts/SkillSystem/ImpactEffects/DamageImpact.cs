using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard {
    //造成伤害 
    public class DamageImpact : IImpactEffect
    {
        SkillData data;
        float[] level;
        public void Excecute(SkillDeployer deployer, int targetIndex)
        {

            data = deployer.SkillData;
            deployer.StartCoroutine(RepeatDamage(deployer,targetIndex));
        }

        IEnumerator RepeatDamage(SkillDeployer deployer, int targetIndex)//多次伤害
        {
            float atkTime = 0;

            do
            {

                //执行伤害
                OnceDamage(targetIndex);
                yield return new WaitForSeconds(data.atkInterval);
                atkTime += data.atkInterval;
                deployer.CalculateTargets();//每次攻击判定都需要判断对象是否在攻击范围内，否则对象走出范围依然能被攻击
            }
            while (atkTime > data.durationTime);
        }
        private void OnceDamage(int targetIndex)//单次伤害
        {
            //实际攻击力：攻击倍率*基础攻击力
            Elf_Monobehavior owner = data.owner.GetComponent<Elf_Monobehavior>();
            float atk = level[owner.Level] * owner.Damage;
            float elementAtk = owner.FireDamage + owner.IceDamage + owner.DarkDamage + owner.HolyDamage + owner.NatureDamage;
            for (int i = 0; i < data.attackTargets[targetIndex].Count; i++)
            {

                data.attackTargets[targetIndex][i].GetComponent<NewPlayerController>().GetHit(atk*(1-data.attackTargets[targetIndex][i].GetComponent<NewPlayerController>().Defence)+elementAtk* (1 - data.attackTargets[targetIndex][i].GetComponent<NewPlayerController>().Defence));

            }
        }

        public void InitEffect(float level1, float level2, float level3)
        {
             level = new float[] {level1,level2,level3 };
        }
    }

}