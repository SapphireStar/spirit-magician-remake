using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard;

namespace Framework
{
    public interface IBattleSystem : ISystem 
    {
        public void StartAttack(int value);
    }

    public class BattleSystem : AbstractSystem, IBattleSystem
    {
        IBattleModel battleModel;
        protected override void OnInit()
        {
            battleModel = this.GetModel<IBattleModel>();
            battleModel.RemainAttackElfs.OnValueChanged += (value) => { StartAttack(value); };
        }

        int count = 0;
        public void StartAttack(int value)
        {

            if (value > 0)
            {

                battleModel.currentTurn.PlayerElfs[count].GetComponent<Elf_Monobehavior>().UseSkill();
                count++;
            }
            else
            {
                count = 0;
                for (int j = 0; j < battleModel.currentTurn.PlayerElfs.Count; j++)
                {
                    battleModel.currentTurn.PlayerElfs[j].GetComponent<IBuffer>().UpdateBuff();
                }
                battleModel.currentTurn.UpdateBuff();
            }


        }


    }
}