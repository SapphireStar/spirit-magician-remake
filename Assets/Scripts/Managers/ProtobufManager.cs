using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PbBattle;
using ProtoBuf;
using PbDict;
using PbSpirit;

namespace ElfWizard.Manager
{
    public class ProtobufManager : BaseManagerSystem
    {

        public static void SendBattleType(BattleType type)
        {

        }
        public static void SendEnterRequest()
        {

        }
        public static void SendBattleAction(int roundIndex,BattleActionType actionType, List<int> lockedDices, int targetUID)
        {
            C2S_BattleAction battleAction = new C2S_BattleAction();
            battleAction.RoundIndex = roundIndex;
            battleAction.ActionType = actionType;
            foreach (var item in lockedDices)
            {
                battleAction.LockedDices.Add(item);
            }
            battleAction.TargetUID = targetUID;

            
        }
        public static BattleInfo ReadBattleInfo(byte[] battleInfo)
        {

            return BattleInfo.Parser.ParseFrom(battleInfo);
        }
        public static BattleRoundInfo ReadRoundInfo(byte[] roundInfo)
        {
            return BattleRoundInfo.Parser.ParseFrom(roundInfo);
        }
        public static List<Spirit> GetUnitSpirits(BattleUnit unit)
        {
            List<Spirit> spirits = new List<Spirit>();
            foreach (var item in unit.Spirits)
            {
                spirits.Add(item);
            }
            return spirits;
        }
        public void SendRequest()
        {

        }

        public override void OnDestroy()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnInit()
        {
            throw new System.NotImplementedException();
        }
    }
}