using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class EnterGameCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        this.SendEvent<EnterGameEvent>();
    }
}
