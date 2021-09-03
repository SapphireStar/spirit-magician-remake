using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ElfWizard {
    [Serializable]
    public class SkillData
    {
        public int skillID;
        public string name;//��������
        public string description;
        public int coolTime;//������ȴʱ��
        public int coolRemain;//����ʣ����ȴʱ��
        public int costSP;//ħ������
        public float attackDistance;
        public float attackAngle;
        public string[] attackTargetTags;//��������Ŀ������
                                         //[HideInInspector]
        public Dictionary<int, List<Transform>> attackTargets;
        public string[] impactType;//����Ӱ�������
        public int nextBatterld;//��������һ�����ܱ��
        public string[] atkRatio;//�˺�����
        public float durationTime;//�˺����
        public float atkInterval;
        [HideInInspector]
        public GameObject owner;//��������
        public string prefabName;
        [HideInInspector]
        public GameObject skillPrefab;
        public string animationName;
        public string hitFxName;
        [HideInInspector]
        public GameObject hitFxPrefab;
        public int level;//���ܵȼ�
        public SkillAttackType attackType;//��������
        public SelectorType selectorType;//����Ӱ�췶Χ����
        public string Buff;


    }

}