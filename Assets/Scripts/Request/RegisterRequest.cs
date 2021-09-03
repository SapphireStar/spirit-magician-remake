using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace ElfWizard {
    public class RegisterRequest : BaseRequest
    {
        private RegisterPanel registerPanel;
        public override void Awake()
        {
            requestCode = RequestCode.User;
            actionCode = ActionCode.Register;
            base.Awake();
        }
        private void Start()
        {
            registerPanel = GetComponent<RegisterPanel>();
        }
        public void SendRequest(string username, string password)
        {
            string data = username + "," + password;
            base.SendRequest(data);
        }
        public override void OnResponse(string data)
        {
            ReturnCode returnCode = (ReturnCode)int.Parse(data);
            registerPanel.OnRegisterResponse(returnCode);
        }
    }
}