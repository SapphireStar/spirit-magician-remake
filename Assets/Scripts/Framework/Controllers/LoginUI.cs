using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using UnityEngine.UI;
using ElfWizard.Commands;


namespace ElfWizard.Controller
{
    public class LoginUI : MonoBehaviour, IController
    {
        Button startButton;
        private void Start()
        {
            this.SendCommand<StartLoginCommand>();

        }

        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
    }
}