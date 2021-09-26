using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ElfWizard.Controller;

namespace ElfWizard
{

    public class LoginRequest :BaseRequest
    {
        private LoginPanel loginPanel;

        public override void Awake()
        {
            requestCode = RequestCode.User;
            actionCode = ActionCode.Login;
            base.Awake();
        }
        private void Start()
        {

            loginPanel = GetComponent<LoginPanel>();
        }

        public void SendRequest(string username, string password)
        {
            string data = username+ "," + password;
            base.SendRequest(data);
        }
        public override void OnResponse(string data)
        {
            ReturnCode returnCode = (ReturnCode)int.Parse(data);
            loginPanel.OnLoginResponse(returnCode);
        }


    }
}
