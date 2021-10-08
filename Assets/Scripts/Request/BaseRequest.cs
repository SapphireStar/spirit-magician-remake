using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace ElfWizard
{
    public class BaseRequest : MonoBehaviour
    {
        protected RequestCode requestCode = RequestCode.None;
        protected ActionCode actionCode = ActionCode.None;
        protected GameFacade gameFacade;
        public void SetFacade(GameFacade facade)
        {
            //this.gameFacade = facade;
        }
        /// <summary>
        /// ���ű�������ʱ������Ӧ��request���뵽requestDic��
        /// </summary>
        public virtual void Awake()
        {
/*            gameFacade = GameFacade.Instance;
            gameFacade.AddRequest(actionCode, this);*/
        }

        protected virtual void SendRequest() { }
        protected void SendRequest(string data)
        {
            //gameFacade.SendRequest(requestCode, actionCode, data);
        }


        /// <summary>
        /// ����Ҫ��ǰRequest������Ӧʱ�����ø�OnResponse����
        /// </summary>
        /// <param name="data"></param>
        public virtual void OnResponse(string data) { }

        /// <summary>
        /// ���ű�������ʱ������request��requestDic���Ƴ�
        /// </summary>
        private void OnDestroy()
        {
            //GameFacade.Instance.RemoveRequest(actionCode);
        }
    }
}
