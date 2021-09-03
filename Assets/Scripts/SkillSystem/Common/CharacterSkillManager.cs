using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class CharacterSkillManager : MonoBehaviour//技能管理器
    {
        public List<SkillData> skills = new List<SkillData>();//技能列表
                                  //生成技能
        private void Start()
        {
            for (int i = 0; i < gameObject.GetComponent<Elf_Monobehavior>().skills.Length; i++)
            {
                skills.Add(SkillConfigReader.GetSkillData(gameObject.GetComponent<Elf_Monobehavior>().skills[i]));
            }
          

            for (int i = 0; i < skills.Count; i++)
            {
                InitSkill(skills[i]);
            }
        }
        private void InitSkill(SkillData data)//初始化技能
        {
            /*
                        资源映射表
            资源名称               资源完整路径

            */
            //data.skillPrefab = Resources.Load<GameObject>("Skill/" + data.prefabName);
            ResourceManager manager = new ResourceManager();
            data.skillPrefab = ResourceManager.Load<GameObject>(data.prefabName);
            data.owner = gameObject;
        }

        public SkillData PrepareSkill(int id)
        {
            //根据id 查找技能数据
            SkillData data = Find(s => s.skillID == id);
            //判断条件
            if (data != null && data.coolRemain <= 0)
            {
                return data;
            }
            return null;
            //返回技能数据
        }
        private SkillData Find(Func<SkillData, bool> handler)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                //if (skills[i].skillID == id)
                if (handler(skills[i]))
                    return skills[i];

            }
            return null;
        }//使用委托查找数组中包含特定内容的对象
        public GameObject GenerateSkill(SkillData data)//生成技能
        {
            
            //创建技能预制体（特效）
            GameObject skillGo = GameObjectPool.Instance.CreateObject(data.prefabName, data.skillPrefab, transform.position, transform.rotation);
            //传递技能数据
            SkillDeployer deployer = skillGo.GetComponentInChildren<SkillDeployer>();
            skillGo.tag = data.owner.tag;
            deployer.SkillData = data;//创建算法对象
            deployer.DeploySkill(skillGo);//执行算法对象

            //GameObjectPool.Instance.CollectObject(skillGo, data.durationTime);//销毁技能

            StartCoroutine(CoolTimeDown(data)); //开启技能冷却
            return skillGo;
        }
        private IEnumerator CoolTimeDown(SkillData data)//技能冷却
        {
            data.coolRemain = data.coolTime;
            while (data.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                data.coolRemain--;
            }
        }

    }
}