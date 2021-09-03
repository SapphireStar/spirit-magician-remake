using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard {
    public class ReduceHealthPerRoundBUFF :IBuff
    {

        public override void ApplyBuff()
        {
            if (GetComponent<NewPlayerController>())
                GetComponent<NewPlayerController>().GetHit(level);
            else
                GetComponent<Elf_Monobehavior>().GetHit(level);

            round--;
            if (round == 0)
            {
                DeleteBuff();
            }
        }

        public override void DeleteBuff()
        {
            Destroy(this);
        }

    }

}
