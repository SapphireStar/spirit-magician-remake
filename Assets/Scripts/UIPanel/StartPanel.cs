using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ElfWizard
{
    public class StartPanel : BasePanel
    {
        Button loginButton;
        Animator anim;
        public override void OnEnter()
        {

            base.OnEnter();
            loginButton = GameObject.Find("LoginButton").GetComponent<Button>();
            anim = loginButton.GetComponent<Animator>();
            loginButton.onClick.AddListener(OnLoginClick);
        }
        private void OnLoginClick()
        {
            uiManager.PushPanel(UIPanelType.Login);
            PlayClickSound();

        }
        public override void OnPause()
        {
            base.OnPause();
            anim.enabled = false;
            loginButton.transform.parent.DOScale(0, 0.3f).OnComplete(() => loginButton.transform.parent.gameObject.SetActive(false));
        }
        public override void OnResume()
        {
            base.OnResume();
            loginButton.transform.parent.gameObject.SetActive(true);
            loginButton.transform.parent.DOScale(1, 0.2f).OnComplete(() => anim.enabled = true);
        }
    }
}