using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace ElfWizard.Commands
{
    public class LoginCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<LoginEvent>();
        }
    }
}