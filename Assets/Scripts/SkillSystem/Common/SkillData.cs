using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ElfWizard {
    [Serializable]
    public class SkillData
    {
        public int skillID;
        public string name;//技能名称
        public string description;
        public int coolTime;//技能冷却时间
        public int coolRemain;//技能剩余冷却时间
        public int costSP;//魔法消耗
        public float attackDistance;
        public float attackAngle;
        public string[] attackTargetTags;//攻击对象目标数组
                                         //[HideInInspector]
        public Dictionary<int, List<Transform>> attackTargets;
        public string[] impactType;//技能影响的类型
        public int nextBatterld;//连击的下一个技能编号
        public string[] atkRatio;//伤害比率
        public float durationTime;//伤害间隔
        public float atkInterval;
        [HideInInspector]
        public GameObject owner;//技能所属
        public string prefabName;
        [HideInInspector]
        public GameObject skillPrefab;
        public string animationName;
        public string hitFxName;
        [HideInInspector]
        public GameObject hitFxPrefab;
        public int level;//技能等级
        public SkillAttackType attackType;//攻击类型
        public SelectorType selectorType;//攻击影响范围类型
        public string Buff;


    }

}