using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework {
    public class ElementSelectCommand : AbstractCommand
    {
        protected override void OnExecute()
        {

            Transform.FindObjectOfType<ElementController>().ClearElement();
        }
    }
}