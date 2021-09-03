using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard {
    //����˺� 
    public class DamageImpact : IImpactEffect
    {
        SkillData data;
        float[] level;
        public void Excecute(SkillDeployer deployer, int targetIndex)
        {

            data = deployer.SkillData;
            deployer.StartCoroutine(RepeatDamage(deployer,targetIndex));
        }

        IEnumerator RepeatDamage(SkillDeployer deployer, int targetIndex)//����˺�
        {
            float atkTime = 0;

            do
            {

                //ִ���˺�
                OnceDamage(targetIndex);
                yield return new WaitForSeconds(data.atkInterval);
                atkTime += data.atkInterval;
                deployer.CalculateTargets();//ÿ�ι����ж�����Ҫ�ж϶����Ƿ��ڹ�����Χ�ڣ���������߳���Χ��Ȼ�ܱ�����
            }
            while (atkTime > data.durationTime);
        }
        private void OnceDamage(int targetIndex)//�����˺�
        {
            //ʵ�ʹ���������������*����������
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