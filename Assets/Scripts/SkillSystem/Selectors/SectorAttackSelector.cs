using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class SectorAttackSelector : IAttackSelector
    {

        public Dictionary<int,List<Transform>> SelectTarget(SkillData data, Transform skillTrans)
        {

            Dictionary<int, List<Transform>> targets = new Dictionary<int, List<Transform>>(data.attackTargetTags.Length);
            for (int i = 0; i < data.attackTargetTags.Length; i++)
            {
                targets.Add(i, new List<Transform>());
            }
            //���ݹ���Ŀ��������ǩ��ȡ��Ϸ����
            for (int i = 0; i < data.attackTargetTags.Length; i++)
            {
  
                GameObject[] tempGOArray = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
                if (tempGOArray.Length == 0)
                    return new Dictionary<int, List<Transform>>();
                for (int j = 0; j < tempGOArray.Length; j++)        //����ȡ���Ķ����transform��ӵ��������󼯺���
                {
                    targets[i].Add(tempGOArray[j].transform);
                }


            }

            //�ж�������Χ
            /*           targets = targets.FindAll(t => Vector3.Distance(t.position, skillTrans.position) <= data.attackDistance
                                          && Vector3.Angle(skillTrans.forward,t.position-skillTrans.position)<=data.attackAngle/2);*/

            //ɸѡ����Ķ���
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i] = targets[i].FindAll(t => t.GetComponent<NewPlayerController>().isDead != true);
            }
            

            //����Ŀ�꣨Ⱥ��/������
            if (data.attackType == SkillAttackType.Group||targets.Count==0)
                return targets;


            Dictionary<int, List<Transform>> tmp = new Dictionary<int, List<Transform>>();
            for (int i = 0; i < targets.Count; i++)
            {
                tmp.Add(i, new List<Transform>());
                Transform min = targets[i][0];
                //��Ϊ���幥�����ж��ĸ�Ŀ������Ҹ�����ѡ�������Ŀ����й���
                foreach (var item in targets[i])
                {
                    if (Mathf.Abs(Vector3.Magnitude(item.position - skillTrans.position)) < Mathf.Abs(Vector3.Magnitude(min.position - skillTrans.position)))
                    {
                        min = item;
                    }
                }
                tmp[i].Add(min);
            }

            


/*            for (int i = 0; i < targets.Count; i++)
            {

                for (int j = 0; j < targets[i].Count; j++)
                {
                    tmp[i].Add(targets[i][j]);
                }
            }*/

            return tmp;

            


        }

    }
}
