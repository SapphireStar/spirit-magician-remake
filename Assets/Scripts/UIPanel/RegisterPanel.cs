using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace ElfWizard
{
    public class RegisterPanel : BasePanel
    {
        private InputField username_IF;
        private InputField password_IF;
        private InputField rePassword_IF;
        private Button registerButton;
        private Button closeButton;
        private RegisterRequest registerRequest;

        private void Start()
        {
            registerRequest = GetComponent<RegisterRequest>();
            username_IF = transform.Find("UserNameLabel").GetComponent<InputField>();
            password_IF = transform.Find("PasswordLabel").GetComponent<InputField>();
            rePassword_IF = transform.Find("RePasswordLabel").GetComponent<InputField>();

            registerButton = transform.Find("RegisterButton").GetComponent<Button>();
            closeButton = transform.Find("CloseButton").GetComponent<Button>();
            registerButton.onClick.AddListener(OnRegisterClick);
            closeButton.onClick.AddListener(OnCloseClick);
        }
        public override void OnEnter()
        {

            base.OnEnter();
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.4f);
            transform.localPosition = new Vector3(1000, 0, 0);
            transform.DOLocalMove(Vector3.zero, 0.4f);

        }

        private void OnRegisterClick()
        {
            PlayClickSound();
            string msg = "";
            if (string.IsNullOrEmpty(username_IF.text))
            {
                msg += "用户名不能为空 ";

            }
            if (string.IsNullOrEmpty(password_IF.text))
            {
                msg += "密码不能为空 ";
            }
            if (string.IsNullOrEmpty(rePassword_IF.text))
            {
                msg += "密码输入不一致";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                uiManager.ShowMessage(msg);
            }
            else
            {
                //TODO请求服务器注册
                registerRequest.SendRequest(username_IF.text, password_IF.text);
            }
        }
        public void OnRegisterResponse(ReturnCode returnCode)
        {
            if (returnCode == ReturnCode.Success)
            {
                uiManager.ShowMessageAsync("注册成功");
            }
            else
            {
                uiManager.ShowMessageAsync("用户名重复，注册失败");
            }
        }
        private void OnCloseClick()
        {
            PlayClickSound();
            transform.DOScale(0, 0.3f);
            Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.3f);
            tweener.OnComplete(() => uiManager.PopPanel());
        }

        public override void OnExit()
        {
            base.OnExit();
            gameObject.SetActive(false);
        }


    }
}