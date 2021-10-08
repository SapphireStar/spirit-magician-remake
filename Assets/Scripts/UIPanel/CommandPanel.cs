using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PbBattle;
using UnityEngine.UI;
using ElfWizard;
using DG.Tweening;
using Google.Protobuf;
using PbSpirit;
using System.Reflection;
using System;
using Framework;

public class CommandPanel : BasePanel,IController
{
    public InputField IF;
    public Text text;
    public Scrollbar bar;
    private RectTransform trans;
    private List<string> history = new List<string>();
    private int histIndex;
    private IUISystem uiSystem;
   

    public override void OnEnter()
    {
        uiSystem = this.GetSystem<IUISystem>();
        gameObject.SetActive(true);
        trans = GetComponent<RectTransform>();
        base.OnEnter();
        trans.localPosition = new Vector3(0, -1925, 0);
        trans.DOLocalMove(Vector3.zero, 0.2f, false);
        IF.ActivateInputField();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiSystem.PopPanel();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetUpperCmd();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetLowerCmd();
        }
    }
    public override void OnPause()
    {


    }
    public override void OnResume()
    {

    }
    public void OnValueChanged()
    {

    }
    public void OnSubmit()
    {
        bar.value = 0;
        text.text += "\n" + IF.text;
        history.Add(IF.text);
        CommandHelper(IF.text);
        //GenerateBACommand(IF.text);
        IF.text = "";
        IF.ActivateInputField();
        histIndex = history.Count;
    }
    private void GetUpperCmd()
    {
        

        if (histIndex-1 >= 0)
        {
            histIndex--;
            IF.text = history[histIndex];
            IF.ActivateInputField();
        }
        IF.ActivateInputField();
    }
    private void GetLowerCmd()
    {

        if (histIndex+1 < history.Count)
        {
            histIndex++;
            IF.text = history[histIndex];
            IF.ActivateInputField();
        }
        IF.ActivateInputField();
    }
    public void testuserdata()
    {

    }
    /// <summary>
    /// 使用反射获取用户输入的方法名，然后调用相应方法
    /// </summary>
    /// <param name="s"></param>
    public void CommandHelper(string s)
    {
        string[] command = s.Split(' ');
        Type type = typeof(CommandPanel);
        MethodInfo methodInfo = type.GetMethod(command[0]);
        object[] parameters = new object[command.Length-1];
        for (int i = 0; i < command.Length-1; i++)
        {
            parameters[i] = command[i+1];
        }
        Debug.Log(parameters.Length);
        methodInfo.Invoke(this,new object[] { parameters });//这里将parameters数组整体看作为一个object，传递的参数类型为数组类型
                                                            //否则Invoke方法无法应对参数数量不同的方法
    }
    public static void roll(object[] dices)
    {
        try
        {
            string[] str = new string[dices.Length];
            for (int i = 0; i < str.Length; i++)
            {
                str[i] = dices[i] as string;
            }
            List<SpecialistType> specialistType = new List<SpecialistType>(str.Length);
            for (int i = 0; i < str.Length; i++)
            {
                specialistType.Add((SpecialistType)int.Parse(str[i]));
            }
            GameFacade.Instance.spawnManager.SetUpPlayerElement(specialistType);
        }
        catch (Exception e)
        {
            Debug.Log("can't roll element, please check damageSpecialists: " + e);
        }

    }
    public void userdata(object[] s)
    {
        try
        {
            uiSystem.PushPanel(UIPanelType.Userdata);
        }
        catch (Exception e)
        {
            Debug.Log("can't get userdata, please check the Internet: " + e);
        }

    }
    public void serverba(object[] s)
    {
        string[] commands = new string[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            commands[i] = s[i] as string;
        }
        S2C_UpdateBattleAction battleAction = new S2C_UpdateBattleAction();
        battleAction.ActionType = (BattleActionType)int.Parse(commands[0]);
        BattleRoundInfo info = new BattleRoundInfo();
        PlayerBattleInfo pbi = new PlayerBattleInfo();
        pbi.Dices = 6;
        info.PlayerBattleInfos.Add(pbi);
        info.PlayerBattleInfos.Add(pbi);
        battleAction.PlayerBattleInfos.Add(info.PlayerBattleInfos[0]);
        battleAction.PlayerBattleInfos.Add(info.PlayerBattleInfos[1]);
        battleAction.CurRoundInfo = info;
        IMessage obj = battleAction;
        //BattleRequest.OnResponse(obj);
    }
    public void clientba(object[] s)
    {
        string[] commands = new string[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            commands[i] = s[i] as string;
        }
        C2S_BattleAction battleAction = new C2S_BattleAction();
        battleAction.ActionType = (BattleActionType)int.Parse(commands[0]);
        battleAction.TargetUID = this.GetModel<IBattleModel>().curRoundInfo.ActiveUID;
        //Debug.Log("------>Active UID: "+GameFacade.Instance.curRoundInfo.ActiveUID);

        if (commands.Length > 1)
        {
            Debug.Log("command length:" + commands.Length);
            int[] temp = new int[commands.Length - 1];
            for (int i = 0; i < commands.Length-1; i++)
            {
                temp[i] =int.Parse(commands[i + 1]);
            }
            this.SendCommand<SendBattleActionCommand>(new SendBattleActionCommand()
            {
                battleActionType = (BattleActionType)int.Parse(commands[0]),
                diceLocked = temp
            }) ;
            GameFacade.Instance.SendBattleAction((BattleActionType)int.Parse(commands[0]), temp);
            Debug.Log("send battleaction: " + Enum.GetName(typeof(BattleActionType), battleAction.ActionType));
            return;
        }
        this.SendCommand<SendBattleActionCommand>(new SendBattleActionCommand()
        {
            battleActionType = (BattleActionType)int.Parse(commands[0])
        });


        Debug.Log("send battleaction: " + Enum.GetName(typeof( BattleActionType), battleAction.ActionType));

    }
    public static void match(object[] s)
    {
        string[] commands = splitCommands(s);
        PbMatch.C2S_BattleMatch battlematch = new PbMatch.C2S_BattleMatch();
        battlematch.BattleType = (BattleType)int.Parse(commands[0]);
        NetManager.Instance.Send(battlematch);
    }
    public static void enterbattle(object[] s)
    {
        string[] commands = splitCommands(s);
        C2S_EnterBattle enterbattle = new C2S_EnterBattle();
        NetManager.Instance.Send(enterbattle);

    }

    public static string[] splitCommands(object[] s)
    {
        string[] commands = new string[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            commands[i] = s[i] as string;
        }
        return commands;
    }
    public static void GenerateBACommand(string s)
    {

        if (s.StartsWith("clientba"))
        {
            s=s.Replace("clientba", "");
            string[] commands = s.Split(',');
            C2S_BattleAction battleAction = new C2S_BattleAction();
            battleAction.ActionType = BattleActionType.BatInit;
            IMessage obj = battleAction;
            NetManager.Instance.Send(obj);
        }

    }
    private static void GenerateBRICommand(string s)
    {
        BattleRoundInfo info = new BattleRoundInfo();
    }
    public override void OnExit()
    {
        base.OnExit();
        trans.DOLocalMove(new Vector3(0, -1925, 0), 0.2f, false).OnComplete(()=>gameObject.SetActive(false));
    }
}
