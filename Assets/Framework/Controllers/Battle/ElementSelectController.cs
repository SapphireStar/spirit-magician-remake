using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PbBattle;
using PbSpirit;
using System;

namespace Framework
{
    public class ElementSelectController :MonoBehaviour, IController
    {
        private SpecialistType mElementType;
        public SpecialistType ElementType
        {
            get
            {
                return mElementType;
            }
            set
            {
                mElementType = value;
            }
        }

        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
        
        private void OnMouseUp()
        {

            mElementType = (SpecialistType)Enum.Parse(typeof(SpecialistType), name.Replace("(Clone)", ""));
            DiceFormation formation = this.GetModel<IBattleModel>().diceFormation;
            int count = 0 ;
            List<int> lockedDices = new List<int>();
            for (int i = 0; i < formation.DamageSpecialists.Count; i++)
            {
                if (formation.DamageSpecialists[i].Equals(mElementType))
                {
                    count++;
                    lockedDices.Add(i);
                }
            }
            Debug.Log(count);
            if (count >= 2)
            {
                this.GetSystem<ISpawnSystem>().SpawnElf(this.GetModel<IBattleModel>().activeUID.Value, this.GetModel<IBattleModel>().GetUidCarriedSkill());
            }
            this.SendCommand<ElementSelectCommand>(new ElementSelectCommand() {LockedDices = lockedDices});
        }
    }
}