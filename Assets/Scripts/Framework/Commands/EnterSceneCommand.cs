using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using ElfWizard;
using UnityEngine.SceneManagement;

namespace Framework
{
    public class EnterSceneCommand : AbstractCommand
    {
        public string MapName;
        protected override void OnExecute()
        {
            this.GetModel<IGameModel>().MapName = MapName;
            SceneManager.LoadScene("Loading");

        }
    }
}