using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ElfWizard
{
    public class MessagePanel : BasePanel
    {
        private Text text;
        private float showTime;
        private string message = "";
        private void Update()
        {
            if (!string.IsNullOrEmpty(message))
            {
                ShowMessage(message);
                message = "";
            }
        }
        public override void OnEnter()
        {
            base.OnEnter();
            text = GetComponent<Text>();
            text.enabled = false;
            uiManager.InjectMsgPanel(this);
        }
        public void ShowMessageAsync(string msg)
        {
            message = msg;
        }
        public void ShowMessage(string msg)
        {
            text.CrossFadeAlpha(1, 0, false);
            text.color = Color.white;
            text.text = msg;
            text.enabled = true;
            Invoke("Hide", showTime);
        }
        private void Hide()
        {
            text.CrossFadeAlpha(0, 1, false);
        }

    }
}