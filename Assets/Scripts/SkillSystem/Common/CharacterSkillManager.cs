using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class CharacterSkillManager : MonoBehaviour//���ܹ�����
    {
        public List<SkillData> skills = new List<SkillData>();//�����б�
                                  //���ɼ���
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
        private void InitSkill(SkillData data)//��ʼ������
        {
            /*
                        ��Դӳ���
            ��Դ����               ��Դ����·��

            */
            //data.skillPrefab = Resources.Load<GameObject>("Skill/" + data.prefabName);
            ResourceManager manager = new ResourceManager();
            data.skillPrefab = ResourceManager.Load<GameObject>(data.prefabName);
            data.owner = gameObject;
        }

        public SkillData PrepareSkill(int id)
        {
            //����id ���Ҽ�������
            SkillData data = Find(s => s.skillID == id);
            //�ж�����
            if (data != null && data.coolRemain <= 0)
            {
                return data;
            }
            return null;
            //���ؼ�������
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
        }//ʹ��ί�в��������а����ض����ݵĶ���
        public GameObject GenerateSkill(SkillData data)//���ɼ���
        {
            
            //��������Ԥ���壨��Ч��
            GameObject skillGo = GameObjectPool.Instance.CreateObject(data.prefabName, data.skillPrefab, transform.position, transform.rotation);
            //���ݼ�������
            SkillDeployer deployer = skillGo.GetComponentInChildren<SkillDeployer>();
            skillGo.tag = data.owner.tag;
            deployer.SkillData = data;//�����㷨����
            deployer.DeploySkill(skillGo);//ִ���㷨����

            //GameObjectPool.Instance.CollectObject(skillGo, data.durationTime);//���ټ���

            StartCoroutine(CoolTimeDown(data)); //����������ȴ
            return skillGo;
        }
        private IEnumerator CoolTimeDown(SkillData data)//������ȴ
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