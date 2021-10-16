
using UnityEngine;
using Framework;
using ElfWizard;
using Google.Protobuf;
using System.Collections.Generic;
using PbBattle;
using System;

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

        //instance.GetModel<IBattleModel>().diceFormation = formation;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Google.Protobuf.Collections.RepeatedField<DiceInfo> diceInfos = new Google.Protobuf.Collections.RepeatedField<DiceInfo>();
            diceInfos.Add(new Google.Protobuf.Collections.RepeatedField<DiceInfo> { new DiceInfo() { DiceValue = 1 }, new DiceInfo() { DiceValue = 1 }, new DiceInfo() { DiceValue = 1 }, new DiceInfo() { DiceValue = 2 }, new DiceInfo() { DiceValue = 2 }, });
            instance.GetModel<IBattleModel>().curRoundInfo = new BattleRoundInfo();
            try
            {
                instance.GetModel<IBattleModel>().curRoundInfo.DiceInfo.Clear();

            }
            catch(Exception e)
            {
                Debug.Log("currentRoundInfo DiceInfo is null");
            }
            instance.GetModel<IBattleModel>().curRoundInfo.DiceInfo.Add(diceInfos);
            transform.Find("Elements").GetComponent<ElementController>().SetupElement(diceInfos);
        }

    }
}
