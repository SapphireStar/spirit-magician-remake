using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ElfWizard.Manager;

namespace ElfWizard
{
    //技能释放器
    public abstract class SkillDeployer : MonoBehaviour
    {
        protected SkillData skillData;
        private IAttackSelector selector;//选区算法对象
        private IImpactEffect[] impactArray;//影响算法对象
        private IBuff buff;
        protected GameObject skillGO;//用于传入子类，根据不同的攻击方式给予技能对象不同的行为逻辑
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
                //创建算法对象
                InitDeployer(); 
            }
        }

        //初始化释放器
        private void InitDeployer()
        {

            //将创建对象交给工厂完成
            selector = DeployerConfigFactory.CreateAttackSelector(skillData);//让工厂根据skilldata中存储的选区形式，计算出释放技能时选中的目标
            impactArray = DeployerConfigFactory.CreateImpactEffects(skillData);//让工厂根据skilldata中存储的效果，生成相应的效果类
            if(skillData.Buff!=null&&skillData.Buff!="")
            buff = DeployerConfigFactory.CreateBuff(skillData);
            isDeployed = false;

        }
        //执行算法对象
        //选区
        public void CalculateTargets()
        {
            skillData.attackTargets = selector.SelectTarget(skillData, transform);//将技能能够选中的对象赋值给skilldata中的待影响对象数组

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

            for (int i = 0; i < skillData.impactType.Length; i++)//根据之前获取到的待影响对象对其进行效果的附加
                {
                
                    impactArray[i].Excecute(this, i);//i代表了攻击目标的索引
            }
            isDeployed = true;
            skillData.owner.GetComponent<Elf_Monobehavior>().attacked = true;
        }
       
/*        protected IEnumerator WaitSkillDeployed()//如果在技能释放的时候无法触发calculatetargets方法，则需要在具体的deployer中单独开启此协程
        {
            while (!isDeployed)
            {
                yield return null;
            }
            skillData.owner.GetComponent<Elf_Monobehavior>().attacked=true;

        }*/
        public abstract void DeploySkill(GameObject skillGO);//TODO将添加buff的代码写到DeploySkill方法中


        //效果



        //创建算法对象
        //执行算法对象

        //释放方式
    }
}