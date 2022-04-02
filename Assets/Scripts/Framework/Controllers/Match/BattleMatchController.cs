using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class BattleMatchController : MonoBehaviour,IController
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
        battleMatchButton.onClick.AddListener(() => { this.SendCommand<BattleMatchCommand>(); });
    }
}
