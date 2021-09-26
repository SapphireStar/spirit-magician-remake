using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using ElfWizard;
public class TestAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ElfWizardArch.Instance.GetSystem<ISpawnSystem>().SpawnElf(2, new PbBattle.SkillEffect() { SkillID = "ElfFire01" });
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ElfWizardArch.Instance.GetModel<IBattleModel>().currentTurn = ElfWizardArch.Instance.GetModel<IBattleModel>().enemy;
            ElfWizardArch.Instance.SendCommand<StartAttackCommand>();
        }
    }
}
