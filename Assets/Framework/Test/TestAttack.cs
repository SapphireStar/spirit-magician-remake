using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using ElfWizard;
public class TestAttack : MonoBehaviour
{
    IArchitecture instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = ElfWizardArch.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            instance.GetSystem<ISpawnSystem>().SpawnElf(2, new PbBattle.SkillEffect() { SkillID = "ElfFire01" });
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            instance.GetModel<IBattleModel>().currentTurn = ElfWizardArch.Instance.GetModel<IBattleModel>().enemy;
            instance.SendCommand<StartAttackCommand>();
        }

    }
}
