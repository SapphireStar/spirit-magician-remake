using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard;

namespace Framework
{
    public class GameOverCommand : AbstractCommand
    {
        public string winner;
        protected override void OnExecute()
        {
            this.SendEvent<GameOverEvent>();
        }
    }
}

