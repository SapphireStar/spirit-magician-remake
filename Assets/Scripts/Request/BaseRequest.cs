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
        /// 当脚本被激活时，将对应的request加入到requestDic中
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
        /// 当需要当前Request做出反应时，调用该OnResponse方法
        /// </summary>
        /// <param name="data"></param>
        public virtual void OnResponse(string data) { }

        /// <summary>
        /// 当脚本被销毁时，将该request从requestDic中移除
        /// </summary>
        private void OnDestroy()
        {
            //GameFacade.Instance.RemoveRequest(actionCode);
        }
    }
}
