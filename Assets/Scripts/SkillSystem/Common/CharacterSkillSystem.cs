using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ElfWizard
{
    //��װ����ϵͳ
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

            Debug.Log("------����ID: " + id+"------");
            Debug.Log(skillManager.skills);
                skill = skillManager.PrepareSkill(id);
                if (skill == null) return;
                //���Ŷ���
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
              
                UseSkill(usableSkills[Random.Range(0, usableSkills.Count)].skillID);//�������һ������
            }

        }
    }

}