using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard;
using System.Threading;
using System;

namespace Framework
{
    public interface IBattleSystem : ISystem 
    {
        void StartAttack(int value);
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
            this.SendEvent<StartAttackEvent>();//���Ϳ�ʼ�����¼�

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
                this.SendEvent<EndAttackEvent>(new EndAttackEvent() { nextRound =battleModel.nextRoundInfo.ActiveUID.ToString() });//�������������ͽ��������¼�

            }


        }


    }
}