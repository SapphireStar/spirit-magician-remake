using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard {
    public class NormalAttackDeployer : SkillDeployer
    {
        public override void DeploySkill(GameObject skillGO)
        {
            //ִ��ѡ���㷨
            CalculateTargets();

            //ִ��Ӱ���㷨
            ImpactTargets();
        }
    }

}