using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using ElfWizard;

namespace Framework
{
    public class EnterSceneCommand : AbstractCommand
    {
        public string MapName;
        protected override void OnExecute()
        {
            this.GetSystem<IBattleSceneSystem>().Init();
            this.GetSystem<IBattleSystem>().Init();
        }
    }
}