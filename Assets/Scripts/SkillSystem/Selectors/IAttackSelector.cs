using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    //����ѡ��
    public interface IAttackSelector
    {
        Dictionary<int,List<Transform>> SelectTarget(SkillData data, Transform skillTrans);
    }
}