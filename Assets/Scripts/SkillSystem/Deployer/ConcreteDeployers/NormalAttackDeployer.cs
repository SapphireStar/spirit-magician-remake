using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard {
    public class NormalAttackDeployer : SkillDeployer
    {
        public override void DeploySkill(GameObject skillGO)
        {
            //执行选区算法
            CalculateTargets();

            //执行影响算法
            ImpactTargets();
        }
    }

}