using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Shared;
using Google.Protobuf;
using Base;
using Newtonsoft.Json;

public class Userdata
{
    private static Userdata _instance;
    public IMessage userdata;
    public static JsonData userbaseinfo;
    public static Userdata Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Userdata();

            }
            return _instance;
        }
    }
    public static int GetUid()
    {
        if (userbaseinfo == null)
        {
            string json = JsonConvert.SerializeObject(Userdata.Instance.userdata);
            JsonData data = JsonMapper.ToObject(json);
            userbaseinfo = data["UserData"]["Player"]["UserBaseInfo"];
        }
        return (int)userbaseinfo["Uid"];
    }
}
