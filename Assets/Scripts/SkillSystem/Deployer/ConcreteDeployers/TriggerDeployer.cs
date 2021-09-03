using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Manager;

namespace ElfWizard
{

    public class TriggerDeployer : SkillDeployer,IResetable
    {
        public bool isTriggered;
        GameObject GO;
        public Collider coll;
        void Start()
        {
            //StartCoroutine(WaitSkillDeployed());
            isDeployed = true;
            skillData.owner.GetComponent<Elf_Monobehavior>().attacked = true;
        }
        public override void DeploySkill(GameObject skillGO)
        {

            transform.SetParent(skillData.owner.transform);
            GO = skillGO;
            if (isTriggered)
            {

                CalculateTargets();
                ImpactTargets();

            }

        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag != SkillData.owner.tag)
            {

                coll = other;
                isTriggered = true;
                DeploySkill(GO);
            }
        }


        public void OnReset()
        {
/*            if (transform.parent != null)
            {
                if (transform.parent.GetComponentsInChildren<SkillDeployer>().Length > 1)
                {
                    GameObjectPool.Instance.CollectObject(gameObject);
                    BattleManager.attacked = true;
                    return;
                }
            }*/
        }
    }

}