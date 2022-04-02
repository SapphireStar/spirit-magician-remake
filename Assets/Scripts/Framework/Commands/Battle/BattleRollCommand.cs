using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class BattleRollCommand : AbstractCommand, ICommand
{
    ElementController elementController;

    protected override void OnExecute()
    {
        this.SendEvent<BattleRollEvent>();
        elementController = Transform.FindObjectOfType<ElementController>();
        foreach (var item in this.GetModel<IBattleModel>().curRoundInfo.Formation.DamageSpecialists)
        {
            Debug.Log(item);
        }
        elementController.SetupElement(this.GetModel<IBattleModel>().curRoundInfo.DiceInfo);
       
    }
}
