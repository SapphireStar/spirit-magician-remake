using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class BuffApplier : MonoBehaviour
    {
        public SkillData data;
        List<IBuff> buffs = new List<IBuff>();
        public float buffDamage;
        public float buffFireDamage;
        public float buffIceDamage;
        public float buffDarkDamage;
        public float buffHolyDamage;
        public float buffNatureDamage;
        public void UpdateBuff(SkillData data)
        {


        }
        public float GetDamage()
        {
            return buffDamage;
        }
        public float GetElementDamage()
        {
            return buffFireDamage + buffIceDamage + buffDarkDamage + buffHolyDamage + buffNatureDamage;
        }


    }

}
