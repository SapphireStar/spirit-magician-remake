using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ElfWizard.Manager;

namespace ElfWizard
{
    //�����ͷ���
    public abstract class SkillDeployer : MonoBehaviour
    {
        protected SkillData skillData;
        private IAttackSelector selector;//ѡ���㷨����
        private IImpactEffect[] impactArray;//Ӱ���㷨����
        private IBuff buff;
        protected GameObject skillGO;//���ڴ������࣬���ݲ�ͬ�Ĺ�����ʽ���輼�ܶ���ͬ����Ϊ�߼�
        public bool isDeployed;
        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                skillData = value;
                //�����㷨����
                InitDeployer(); 
            }
        }

        //��ʼ���ͷ���
        private void InitDeployer()
        {

            //���������󽻸��������
            selector = DeployerConfigFactory.CreateAttackSelector(skillData);//�ù�������skilldata�д洢��ѡ����ʽ��������ͷż���ʱѡ�е�Ŀ��
            impactArray = DeployerConfigFactory.CreateImpactEffects(skillData);//�ù�������skilldata�д洢��Ч����������Ӧ��Ч����
            if(skillData.Buff!=null&&skillData.Buff!="")
            buff = DeployerConfigFactory.CreateBuff(skillData);
            isDeployed = false;

        }
        //ִ���㷨����
        //ѡ��
        public void CalculateTargets()
        {
            skillData.attackTargets = selector.SelectTarget(skillData, transform);//�������ܹ�ѡ�еĶ���ֵ��skilldata�еĴ�Ӱ���������

            //StartCoroutine(WaitSkillDeployed());
        }
        public void ImpactTargets()
        {
            for (int i = 0; i < impactArray.Length; i++)
            {

                string[] r = SkillData.atkRatio[i].Split(';');
                float[] f = new float[r.Length];
                for (int j = 0; j < r.Length; j++)
                {
                    f[j] = float.Parse(r[j]);
                }
                
                impactArray[i].InitEffect(f[0], f[1], f[2]);
            }

            for (int i = 0; i < skillData.impactType.Length; i++)//����֮ǰ��ȡ���Ĵ�Ӱ�����������Ч���ĸ���
                {
                
                    impactArray[i].Excecute(this, i);//i�����˹���Ŀ�������
            }
            isDeployed = true;
            skillData.owner.GetComponent<Elf_Monobehavior>().attacked = true;
        }
       
/*        protected IEnumerator WaitSkillDeployed()//����ڼ����ͷŵ�ʱ���޷�����calculatetargets����������Ҫ�ھ����deployer�е���������Э��
        {
            while (!isDeployed)
            {
                yield return null;
            }
            skillData.owner.GetComponent<Elf_Monobehavior>().attacked=true;

        }*/
        public abstract void DeploySkill(GameObject skillGO);//TODO�����buff�Ĵ���д��DeploySkill������


        //Ч��



        //�����㷨����
        //ִ���㷨����

        //�ͷŷ�ʽ
    }
}