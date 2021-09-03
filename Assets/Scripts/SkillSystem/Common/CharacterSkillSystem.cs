using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ElfWizard
{
    //封装技能系统
    [RequireComponent(typeof(CharacterSkillManager))]
    public class CharacterSkillSystem : MonoBehaviour
    {
        SkillData skill;
        private CharacterSkillManager skillManager;
        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
        }
        private void DeploySkill()
        {
            skillManager.GenerateSkill(skill);
        }
        public void UseSkill(int id)
        {

            Debug.Log("------技能ID: " + id+"------");
            Debug.Log(skillManager.skills);
                skill = skillManager.PrepareSkill(id);
                if (skill == null) return;
                //播放动画
                skillManager.GenerateSkill(skill);
            
        }

        private void UseRandomSkill()
        {
            List<SkillData> usableSkills = new List<SkillData>();
            foreach (var item in skillManager.skills)
            {
                if (skillManager.PrepareSkill(item.skillID) != null)
                    usableSkills.Add(item);

            }
            if (usableSkills.Count > 0)
            {
              
                UseSkill(usableSkills[Random.Range(0, usableSkills.Count)].skillID);//随机调用一个技能
            }

        }
    }

}