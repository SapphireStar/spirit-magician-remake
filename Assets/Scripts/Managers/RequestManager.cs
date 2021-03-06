using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ElfWizard.Manager;
using Framework;

namespace ElfWizard
{
    public interface IRequestSystem:ISystem
    {

    }
    public class RequestManager : BaseManagerSystem,IRequestSystem
    {

        private Dictionary<ActionCode, BaseRequest> requestDic = new Dictionary<ActionCode, BaseRequest>();
        public void AddRequest(ActionCode actionCode, BaseRequest request)
        {
            requestDic.Add(actionCode, request);
        }
        public void RemoveRequest(ActionCode actionCode)
        {
            requestDic.Remove(actionCode);
        }
        public void HandleResponse(ActionCode actionCode, string data)
        {
           BaseRequest request = requestDic.TryGet<ActionCode, BaseRequest>(actionCode);
            if (request == null)
            {
                Debug.LogWarning("无法得到ActionCode[" + actionCode + "]对应的Request类");

            }
            request.OnResponse(data);
        }

        public override void OnDestroy()
        {

        }

        protected override void OnInit()
        {

        }
    }
}
