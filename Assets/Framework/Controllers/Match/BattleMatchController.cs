using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class BattleMatchUIController : MonoBehaviour,IController
    {
        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
        Button battleMatchButton;
        // Start is called before the first frame update
        void Start()
        {
            battleMatchButton = transform.Find("Canvas/Match").GetComponent<Button>();
            battleMatchButton.onClick.AddListener(()=> { this.SendCommand<BattleMatchCommand>(); });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}