
using UnityEngine;
using Framework;
using ElfWizard;
using Google.Protobuf;
using System.Collections.Generic;

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
        PbBattle.DiceFormation formation = new PbBattle.DiceFormation();
        formation.DamageSpecialists.Add(new List<PbSpirit.SpecialistType>() { PbSpirit.SpecialistType.StFire, PbSpirit.SpecialistType.StFire, PbSpirit.SpecialistType.StFire, PbSpirit.SpecialistType.StHoly, PbSpirit.SpecialistType.StHoly });
        instance.GetModel<IBattleModel>().diceFormation = formation;
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Find("Elements").GetComponent<ElementController>().SetupElement(formation);
        }

    }
}
