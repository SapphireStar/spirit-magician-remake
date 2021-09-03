using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using LitJson;
using UnityEngine.Networking;
using System.Reflection;
using Shared;
using System;
using PbLogin;
using Newtonsoft.Json;

namespace ElfWizard {
    public class UserdataPanel : BasePanel
    {
        JsonData data = new JsonData();
        Text text;

        public override void OnEnter()
        {
            base.OnEnter();
            text = GetComponentInChildren<Text>();

            //StartCoroutine(GetJson());
            GetUserdata();
            gameObject.SetActive(true);
            transform.localPosition = new Vector3(1080, 0, 0);
            transform.DOLocalMove(Vector3.zero, 0.2f, false);

        }
        public void OnCloseClick()
        {
            transform.DOLocalMove(new Vector3(1080, 0, 0), 0.2f, false).OnComplete(()=>uiManager.PopPanel());
        }
        public override void OnExit()
        {
            base.OnExit();
            gameObject.SetActive(false);

        }

        private void GetUserdata()
        {

            /*            UnityWebRequest request =UnityWebRequest.Get("file://"+Application.dataPath+ "/Resources/userdata/userdata.json");
                        AsyncOperation op = request.SendWebRequest();
                        yield return op;
                        if(!string.IsNullOrEmpty(request.error))
                        {
                            Debug.LogError(request.error);
                        }*/
            /*  弃用的获取信息方案：
             *  Userdata userdata = new Userdata();
                Type type = typeof(UserData);
                PropertyInfo[] info = type.GetProperties();
                foreach (PropertyInfo item in info)
                {
                  Debug.Log( item.Name+": "+ item.GetValue(data));
                }*/
            try
            {

                string json = JsonConvert.SerializeObject(Userdata.Instance.userdata);
                data = JsonMapper.ToObject(json);
                JsonData userbaseinfo = data["UserData"]["Player"]["UserBaseInfo"];
                text.text = "Userbaseinfo:\n";
                foreach (var item in userbaseinfo.Keys)
                {
                    text.text += item + ": ";
                    text.text += userbaseinfo[item] + "\n";
                }
            }
            catch (Exception e)
            {
                Debug.Log("can't examine userdata: " + e);
            }

        }
    }
}