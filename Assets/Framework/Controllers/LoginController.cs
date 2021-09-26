using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using ElfWizard.Commands;

namespace ElfWizard.Controller
{
    public class LoginController : MonoBehaviour, IController
    {

        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }

        void Start()
        {
            //this.SendCommand<StartLoginCommand>();
        }


        void Update()
        {

        }
    }
}