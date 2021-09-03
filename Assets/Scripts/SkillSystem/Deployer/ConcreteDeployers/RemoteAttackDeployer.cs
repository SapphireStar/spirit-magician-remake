using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class RemoteAttackDeployer : SkillDeployer
    {
        private float skillSpeed = 10;
        bool isAttacked;//用来记录攻击是否击中敌人
        Vector3 targetPos;
        int attackTargetAmount;
        private void Update()
        {
            if (skillGO != null&&!isAttacked && SkillData.attackTargets!=null&& SkillData.attackTargets.Count > 0)
            {
                if (skillData.attackTargets!=null&&skillData.attackTargets[skillData.attackTargets.Count - 1].Count > 0 )
                {

                    skillGO.transform.position = Vector3.MoveTowards(skillGO.transform.position, skillData.attackTargets[attackTargetAmount-1][0].position, skillSpeed * Time.deltaTime);
                    if (CalculateDistance())
                    {
                        isAttacked = true;
                        ImpactTargets();
                        GameObjectPool.Instance.CollectObject(skillGO);
                    }
                }
            }

        }

        public override void DeploySkill(GameObject skillGO)
        {

            isAttacked = false;
            this.skillGO = skillGO;
            CalculateTargets();
            if (SkillData.attackTargets.Count > 0)
            {
                attackTargetAmount = SkillData.attackTargets.Count;
                targetPos = skillData.attackTargets[attackTargetAmount - 1][0].position;
            }
            else
            {
                GameObjectPool.Instance.CollectObject(skillGO);
            }
            



        }
        private bool CalculateDistance()
        {
            return Mathf.Abs(Vector3.Magnitude(skillGO.transform.position - SkillData.attackTargets[attackTargetAmount - 1][0].position)) < 0.01f;
        }
    }
}



