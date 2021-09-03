using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;

namespace ElfWizard
{
    public class LoginPanel : BasePanel
    {
        private Button closeButton;
        private InputField username_IF;
        private InputField password_IF;
        private Button loginButton;
        private Button registerButton;
        //private LoginRequest loginRequest;
        private bool successLogin;
        private Test testnet;
        private void Update()
        {
            if (successLogin)
            {
                GameFacade.Instance.LoadSceneAsync("map01",()=> { }/*, battleInfo*/);
                successLogin = false;
            }
        }
        public override void OnEnter()
        {
            base.OnEnter();

            testnet = GameFacade.Instance.testnet;
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.4f);
            transform.localPosition = new Vector3(1000, 0, 0);
            transform.DOLocalMove(Vector3.zero, 0.4f);

            username_IF = transform.Find("UserNameLabel").GetComponent<InputField>();
            password_IF = transform.Find("PasswordLabel").GetComponent<InputField>();
            closeButton = transform.Find("CloseButton").GetComponent<Button>();
            transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
            transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
            transform.Find("EnterButton").GetComponent<Button>().onClick.AddListener(OnEnterClick);

            //loginRequest = GetComponent<LoginRequest>();
            //testLogin = GetComponent<Test>();
            testnet.Init();
            closeButton.onClick.AddListener(OnCloseClick);
        }
        private void OnLoginClick()
        {
            PlayClickSound();
/*            string msg = "";
            if (string.IsNullOrEmpty(username_IF.text))
            {
                msg += "用户名不能为空 ";
            }
            if (string.IsNullOrEmpty(password_IF.text))
            {
                msg += "密码不能为空";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                uiManager.ShowMessage(msg);
                msg = "";
                return;
            }*/
            //loginRequest.SendRequest(username_IF.text, password_IF.text);
            StartCoroutine(testnet.doLogin());
        }
        private void OnEnterClick()
        {
            PlayClickSound();
            testnet.OnEnterGameClicked();
            
        }
        private void OnRegisterClick()
        {
            PlayClickSound();
            uiManager.PushPanel(UIPanelType.Register);
        }
        private void OnCloseClick()
        {
            PlayClickSound();
            transform.DOScale(0, 0.3f);
            Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.3f);
            tweener.OnComplete(() => uiManager.PopPanel());
        }
        public void OnLoginResponse(ReturnCode returnCode)
        {
            if (returnCode == ReturnCode.Success)
            {
                uiManager.ShowMessageAsync("登陆成功");
                successLogin = true;
            }
            else
            {
                uiManager.ShowMessageAsync("用户名或密码错误,请重新输入");
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            gameObject.SetActive(false);
        }
    }
}