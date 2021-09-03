using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PbBattle;
using Google.Protobuf;
using Newtonsoft.Json;
using System;

namespace ElfWizard
{
    public class BattleRequest
    {
        string requestcode = "该request类型的requestCode";
        GameFacade facade;
        

        public static void SendRequest(BattleActionType type)
        {
            C2S_BattleAction battleaction = new C2S_BattleAction();
            //TODO: 发送battleAction
            switch (type)
            {
                case BattleActionType.BatInit:
                    
                    break;
                case BattleActionType.BatRoll:
                    break;
                case BattleActionType.BatAttack:
                    break;
                case BattleActionType.BatSurrender:
                    break;
                case BattleActionType.BatTimeout:
                    if (GameFacade.Instance.curRoundInfo.ActiveUID == GameFacade.Instance.playerBattleInfo.Uid)
                    {
                        battleaction.ActionType = BattleActionType.BatTimeout;
                        battleaction.RoundIndex = GameFacade.Instance.curRoundInfo.RoundIndex;
                        battleaction.TargetUID = GameFacade.Instance.enemyBattleInfo.Uid;
                        NetManager.Instance.Send(battleaction);
                    }
                    break;

            }
        }
        public static void OnResponse(string RequestCode, BattleInfo battleInfo)
        {
            GameFacade.Instance.Init();
        }
        public static void OnResponse(IMessage obj)
        {

            Debug.Log("------BattleRequest OnResponse------");
            S2C_UpdateBattleAction battleAction = S2C_UpdateBattleAction.Parser.ParseFrom(obj.ToByteArray());
            BattleRoundInfo curRoundInfo = battleAction.CurRoundInfo;
            BattleRoundInfo nextRoundInfo = battleAction.NextRoundInfo;
            GameFacade.Instance.UpdateRoundInfo(curRoundInfo, nextRoundInfo);//更新Gamefacade中的roundinfo
            GameFacade.Instance.battleManager.UpdateBattleRoundInfo(battleAction.CurRoundInfo, battleAction);


            Debug.Log("curRoundInfo: " + JsonConvert.SerializeObject(curRoundInfo));
            Debug.Log("nextRoundInfo: " + JsonConvert.SerializeObject(nextRoundInfo));

            switch (battleAction.ActionType)
            {
                case BattleActionType.BatInit:
                    Debug.Log("------Init Battle------");
                    GameFacade.Instance.StartGame();
                    break;
                case BattleActionType.BatRoll:
                    Debug.Log("------Roll------");
                    //List<PbSpirit.SpecialistType> temp = new List<PbSpirit.SpecialistType>();
/*                    foreach (var item in battleAction.CurRoundInfo.Formation.DamageSpecialists)
                    {
                        temp.Add(item);
                    }*/
                    List<PbSpirit.SpecialistType> temp = new List<PbSpirit.SpecialistType>();
                    foreach (DiceInfo item in curRoundInfo.DiceInfo)
                    {
                        temp.Add((PbSpirit.SpecialistType)item.DiceValue);
                    }
                    GameFacade.Instance.spawnManager.SetUpPlayerElement(temp);//表现骰子信息
                    break;
                case BattleActionType.BatAttack:
                    Debug.Log("------Attack------");
                    GameFacade.Instance.spawnManager.UpdatePlayerElfs(curRoundInfo);
                    GameFacade.Instance.StartCoroutine(Wait(1, GameFacade.Instance.StartAttack));
                    break;
                case BattleActionType.BatSurrender:

                    break;
                case BattleActionType.BatTimeout:
                    Debug.Log("------TimeOut------");
                    GameFacade.Instance.spawnManager.UpdatePlayerElfs(curRoundInfo);
                    GameFacade.Instance.StartCoroutine(Wait(1, GameFacade.Instance.StartAttack));
                    break;

            }
        }
        static IEnumerator Wait(float time,Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }


    }
}