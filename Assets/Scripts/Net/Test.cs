using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using PbDict;
using PbLogin;
using UnityEngine.Networking;
using LitJson;
using Shared;
using ElfWizard;
using PbBattle;
using Newtonsoft.Json;
using System;
using System.Reflection;
using Framework;

public class Test : MonoBehaviour,IController
{//http://35.172.239.23:15201/gamelogin
    public string webIPPort = "35.172.239.23:15201";//"35.172.239.23:25001"
    public string sockectIP = "35.172.239.23";//"35.172.239.23";//
    public int sockectPort = 11001;//5050;//11001;//

   private Base.UserBaseInfo userBaseInfo = new Base.UserBaseInfo();

    private UserData userData = null;
    private string battleID = "";
    private bool isMyRound = false;

    private BattleRoundInfo curRoundInfo = null;
    private BattleRoundInfo nextRoundInfo = null;

    private bool hasSendRollAction = false;
    //private GameObject buttonConnect;
    //private GameObject buttonLogin;

    private Base.UserBaseInfo userInfo = null;
    // Start is called before the first frame update
    void Start()
    {
        Init();

    }
    public void Init()
    {

        Debug.Log("sockectIP: " + sockectIP); Debug.Log("sockectIP: " + sockectIP);
        Debug.Log("sockectPort: " + sockectPort);
        NetManager.Instance.RegistListener(EDict.PbLoginS2CAuth, this.PbLoginS2CAuth);
        NetManager.Instance.RegistListener(EDict.PbLoginS2CCreateRole, this.PbLoginS2CCreateRole);
        NetManager.Instance.RegistListener(EDict.SharedServerMessage, this.SharedServerMessage);
        NetManager.Instance.RegistListener(EDict.PbLoginS2CEnterGame, this.PbLoginS2CEnterGame);
        //NetManager.Instance.RegistListener(EDict.PbBattleS2CUpdateBattleAction, this.PbBattleS2CUpdateBattleAction);


        NetManager.Instance.RegistListener(EDict.PbMatchS2CBattleMatch, this.BattleHandler);
        NetManager.Instance.RegistListener(EDict.PbBattleS2CStartBattle, this.BattleHandler);
        NetManager.Instance.RegistListener(EDict.PbBattleS2CEnterBattle, this.BattleHandler);
        NetManager.Instance.RegistListener(EDict.PbBattleS2CBattleEnd, this.BattleHandler);


        NetManager.Instance.RegistListener(EDict.PbBattleS2CBattleAction, this.BattleHandler);
        NetManager.Instance.RegistListener(EDict.PbBattleS2CUpdateBattleAction, this.BattleHandler);//TODO:添加EnterGame回调

        this.RegisterEvent<LoginEvent>(OnLoginClicked);
        this.RegisterEvent<EnterGameEvent>(OnEnterGameClicked);
        //NetManager.Instance.RegistListener(EDict.PbBattleS2CBattleEnd, this.PbBattleS2CUpdateBattleAction);
        //NetManager.Instance.RegistListener(EDict.PbQueryS2CQueryBattleState, BattleRequest.OnResponse);
        // NetManager.Instance.RegistListener(EDict.PbLoginS2CEnterGame, (message) => Debug.Log("entergame: "));

        /*        buttonConnect = GameObject.Find("Connect");
                buttonConnect.GetComponent<Button>().onClick.AddListener(OnEnterGameClicked);

                buttonLogin = GameObject.Find("Login");
                buttonLogin.GetComponent<Button>().onClick.AddListener(OnLoginClicked);*/
    }

    void SharedServerMessage(IMessage obj)
    {
        Debug.Log("------- SharedServerMessage ----- ");
        ServerMessage sm = ServerMessage.Parser.ParseFrom(obj.ToByteArray());
        Debug.Log("Code: " + sm.Code + ", Content: " + sm.Content);
    }

    void PbLoginS2CAuth(IMessage obj) {
        Debug.Log("------- PbLoginS2CAuth ----- ");

        NetManager.Instance.BeginHeartBeat();

        S2C_Auth sa = S2C_Auth.Parser.ParseFrom(obj.ToByteArray());
        this.GetModel<IUserModel>().userBaseInfo = sa.User;
        
        if (!sa.Ok)//若验证失败，则登录失败
        {
            Debug.LogError("PbLoginS2CAuth fail!");
            return;
        }

        if (sa.User != null && !sa.IsNew) //若验证成功，且为老玩家
        {
            Debug.Log("Role UID: " + sa.User.Uid);
            userInfo = sa.User;
        }
        
    }

    void PbLoginS2CCreateRole(IMessage obj)
    {
        Debug.Log("------- PbLoginS2CCreateRole ----- ");

        S2C_CreateRole sc = S2C_CreateRole.Parser.ParseFrom(obj.ToByteArray());
        Debug.Log("PbLoginS2CCreateRole UID: " + sc.Uid);
    }
    void PbLoginS2CEnterGame(IMessage obj)
    {
        Debug.Log("------ PbLoginS2CEnterGame -----");
        S2C_EnterGame eg = S2C_EnterGame.Parser.ParseFrom(obj.ToByteArray());

        this.GetModel<IUserModel>().UserData = obj;
        setUpUserBaseInfo();//给当前类中的userBaseInfo赋值

        this.SendCommand<EnterSceneCommand>(new EnterSceneCommand { MapName = "MatchMenu" });//通过更改MapName来更改加载的地图

    }

    void setUpUserBaseInfo()
    {
        string json = JsonConvert.SerializeObject(this.GetModel<IUserModel>().UserData);
        JsonData data = JsonMapper.ToObject(json);
        JsonData info = data["UserData"]["Player"]["UserBaseInfo"];
        userBaseInfo.Account =(string) info["Account"];
        userBaseInfo.Lv = (int)info["Lv"];
        userBaseInfo.Uid = (int)info["Uid"];
    }

    void BattleHandler(IMessage obj)
    {

        if (obj is S2C_StartBattle)
        {
            Debug.Log("------S2CStartBattle------");
            battleID = ((S2C_StartBattle)obj).BattleInfo.Id;

            this.GetModel<IBattleModel>().battleInfo = obj as BattleInfo;
            Debug.Log("BattleHandler ---- battleID: " + battleID);

            C2S_EnterBattle msg = new C2S_EnterBattle();
            NetManager.Instance.Send(msg);

            
        }


        if (obj is S2C_EnterBattle)
        {
           // GameFacade.Instance.StartGame();//TODO 测试用
            Debug.Log("------S2CEnterBattle------");
            Debug.Log("Start Battle In 2 Seconds");
            this.SendCommand<EnterSceneCommand>(new EnterSceneCommand() { MapName = "TestFramework" });
            StartCoroutine(WaitForEnterBattle(2));

        }
        IEnumerator WaitForEnterBattle(float time)
        {
            yield return new WaitForSeconds(time);
            this.SendCommand<StartBattleCommand>(new StartBattleCommand() { InitRoundInfo = obj });


            curRoundInfo = ((S2C_EnterBattle)obj).BattleRoundInfo;
 
            isMyRound = (curRoundInfo.ActiveUID == userBaseInfo.Uid);
            Debug.Log("BattleHandler ---- battleActiveUID: " + curRoundInfo.ActiveUID + ", isMyRound: " + isMyRound);

            foreach (var bu in curRoundInfo.PlayerBattleInfos)
            {
                Debug.Log("UID: " + bu.Uid + ", HP: " + bu.Hp);
            }

            if (curRoundInfo.ActiveUID == userBaseInfo.Uid)
            {
                StartCoroutine(DelaySendBattleAction(BattleActionType.BatRoll));
            }
        }

        if (obj is S2C_UpdateBattleAction)
        {
            //GameFacade.Instance.UpdateRoundInfo(curRoundInfo, nextRoundInfo);
            Debug.Log("----------- S2C_UpdateBattleAction ---------");

            var uba = (S2C_UpdateBattleAction)obj;

            curRoundInfo = uba.CurRoundInfo;
            this.GetModel<IBattleModel>().curRoundInfo = uba.CurRoundInfo;
            this.GetModel<IBattleModel>().nextRoundInfo = uba.NextRoundInfo;
            this.SendCommand<UpdateRoundInfoCommand>(new UpdateRoundInfoCommand
            {
                curRoundInfo = uba.CurRoundInfo,
                nextRoundInfo = uba.NextRoundInfo,
                activeUID = curRoundInfo.ActiveUID

            }) ;

            Debug.Log("curRoundInfo: " + JsonConvert.SerializeObject(curRoundInfo));

            nextRoundInfo = uba.NextRoundInfo;

            Debug.Log("nextRoundInfo: " + JsonConvert.SerializeObject(nextRoundInfo));



            if (uba.ActionType == BattleActionType.BatAttack)
            {
                //应该在此进行攻击表现,然后进入下一轮
                //使用curRoundInfo里的effects来进行表

                Debug.Log("对攻击进行表现");
                
                foreach (var bu in curRoundInfo.PlayerBattleInfos)
                {
                    Debug.Log("UID: " + bu.Uid + ", HP: " + bu.Hp);
                }
                this.SendCommand<StartAttackCommand>();
                if (nextRoundInfo != null)
                {
                    //进入下一轮
                    curRoundInfo = nextRoundInfo;
                    nextRoundInfo = null;
                    if (curRoundInfo.ActiveUID == userBaseInfo.Uid)
                    {

                        StartCoroutine(DelaySendBattleAction(BattleActionType.BatRoll));
                        Debug.Log("------roll骰子------");
                    }
                }

            }
            else if (uba.ActionType == BattleActionType.BatRoll)
            {
                this.SendCommand<BattleRollCommand>();
            }
            else if (uba.ActionType == BattleActionType.BatRoll && hasSendRollAction)//如果已经选择了骰子，并且接受到了来自服务器batroll的回应，则开始攻击
            {
                if (curRoundInfo.ActiveUID == userBaseInfo.Uid)
                {
                    StartCoroutine(DelaySendBattleAction(BattleActionType.BatAttack));
                }
            }
        }

        if (obj is S2C_BattleEnd)
        {

            var be = (S2C_BattleEnd)obj;
            Debug.Log("BattleEnd ---- battleID: " + be.BattleID + ", winner is: " + be.WinnerUid);

        }

    }
    private IEnumerator DelaySendBattleAction(BattleActionType actionType)
    {
        Debug.Log("----- Start DelaySendBattleAction -----");
        yield return new WaitForSeconds(2);

        SendBattleAction(actionType);
    }
    public void SendBattleAction(BattleActionType actionType,int[] lockedDices = null)
    {
        C2S_BattleAction action = new C2S_BattleAction();
        action.ActionType = actionType;
        action.RoundIndex = curRoundInfo.RoundIndex;
        //action.TargetUID = GameFacade.Instance.battleManager.enemyBattleInfo.Uid;
        if (lockedDices != null)
        {
            for (int i = 0; i < lockedDices.Length; i++)
            {
                action.LockedDices.Add(lockedDices[i]);
            }
        }
        //Debug.Log("------>tartgetUID: "+action.TargetUID);
        NetManager.Instance.Send(action);

        hasSendRollAction = (actionType == BattleActionType.BatRoll);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterGameClicked(EnterGameEvent e)
    {
        if (null == userInfo )//若角色为空（新用户），则开始创建角色
        {
            Debug.Log("------- Need C2S_CreateRole ----- ");
            C2S_CreateRole c2S_CreateRole = new C2S_CreateRole();
            c2S_CreateRole.Name = "Test001";

            NetManager.Instance.Send(c2S_CreateRole);
        }else//若为老用户，则直接进入游戏
        {
            C2S_EnterGame enterGame = new C2S_EnterGame();
            enterGame.Uid = userInfo.Uid;
            bool result = NetManager.Instance.Send(enterGame);
        }
    }

    private void OnLoginClicked(LoginEvent e)
    {
        Debug.Log("calling web login");

        StartCoroutine(doLogin());
      
    }

    public IEnumerator doLogin()
    {
        WWWForm form = new WWWForm();

        form.AddField("channel", "dev");
        form.AddField("account", "test001"); //平台account，例如微信，qq，Facebook等等,SDK login成功后，都会返回account和平台token。“三方认证”
        form.AddField("channel_tag", "t03");

        string uri = "http://"+ webIPPort + "/gamelogin";
        string response = "";

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            Debug.Log(pages[page] + ":\nReceived: " + webRequest.responseCode);

            if (webRequest.responseCode == 200 || webRequest.responseCode == 0)
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                response = webRequest.downloadHandler.text;
                Debug.Log("response: "+response);
                // Sample of Received  'webRequest.downloadHandler.text' : {"ret":0,"msg":"","data":{"game_account":"61134f7cccb78745c607","token":"t_1628655484_2FD2F5A98D","redirecturl":""}}

                //TODO: need judge the response has error or not

                Debug.Log("Connect");
                NetManager.Instance.ConnectToGSServer(sockectIP, sockectPort, (success) => {
                    if (success)
                    {
                        Debug.Log("connect success");
                        JsonData jsonData = JsonMapper.ToObject(response);

                        JsonData jd = jsonData["data"];

                        Debug.Log("Auth");
                        C2S_Auth authReq = new C2S_Auth();
                        authReq.Account = (string)jd["game_account"];
                        authReq.Token = (string)jd["token"];
                        bool result = NetManager.Instance.Send(authReq);//该方法自动获取相应IMessage所对应的requestCode，然后发送给服务端

                        Debug.Log("NetManager.Instance.Send(authReq): " + result);
                    }
                });

            }
            else
            {
                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
            }
        }
    }
    IArchitecture mArchitecture;

    IArchitecture IBelongToArchitecture.getArchitecture()
    {
        return ElfWizardArch.Instance;
    }
}
