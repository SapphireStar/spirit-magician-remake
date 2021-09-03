using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public abstract class IBuff : MonoBehaviour
    {
        [SerializeField]
        protected int round;
        [SerializeField]
        protected float level;
        public abstract void ApplyBuff();
        public abstract void DeleteBuff();

        public void InitBuff(float _level)
        {
            level = _level;
        }

    }

}
