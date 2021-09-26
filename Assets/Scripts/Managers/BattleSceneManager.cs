using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Manager;
using PbBattle;
using Base;
using PbSpirit;
using Framework;
using ElfWizard.Events;

namespace ElfWizard
{
    public interface IBattleSceneSystem : ISystem
    {

    }
    public class BattleSceneManager : BaseManagerSystem,IBattleSceneSystem
    {
        IBattleModel battleModel;

        public string ID;
        public int roundNum;
        public List<int> specialEffects;
        public int mapID;
        public BattleUnit playerUnit;
        public BattleUnit enemyUnit;
        public int defaultDices;
        public long startTime;
        public long endTime;
        private BattleInfo battleInfo;
        BattleManager battleManager;
        /// <summary>
        /// 该类用于初始化游戏场景
        /// </summary>
        /// <param name="gameFacade"></param>
        /// <param name="battleInfo"></param>

        protected override void OnInit()//TODO:后期修改各信息的赋值方式
        {
            battleModel = this.GetModel<IBattleModel>() ;
            battleManager = GameFacade.Instance.battleManager;
            battleInfo = (this.GetModel<IBattleModel>() as BattleModel).battleInfo;
            Debug.Log(battleManager.battleInfo.MapID);
            mapID = battleInfo.MapID;
            playerUnit = battleInfo.Players[0];
            enemyUnit = battleInfo.Players[1];
            ID = battleInfo.Id;
            roundNum = battleInfo.RoundNum;
            foreach (var item in battleInfo.SpecialEffects)
            {
                specialEffects.Add(item);

            }
            defaultDices = battleInfo.DefaultDices;
            startTime = battleInfo.StartTime;

                battleManager.player.tag = "Player";
                battleManager.enemy.tag = "Enemy";
                battleManager.player.transform.localPosition = Vector3.zero;
                battleManager.enemy.transform.localPosition = Vector3.zero;
                battleManager.enemy.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                battleManager.player.UID = battleInfo.Players[0].UserBaseInfo.Uid;
                battleManager.enemy.UID = battleInfo.Players[1].UserBaseInfo.Uid;
                battleManager.playerBattleInfo.Uid = battleInfo.Players[0].UserBaseInfo.Uid;
                battleManager.enemyBattleInfo.Uid = battleInfo.Players[1].UserBaseInfo.Uid;
                InitSpiritPackage();

            

        }
        void InitSpiritPackage()
        {
            List<Spirit> playertmp = new List<Spirit>();
            List<Spirit> enemytmp = new List<Spirit>();
            foreach (var item in playerUnit.Spirits)
            {
                playertmp.Add(item);
            }
            foreach (var item in enemyUnit.Spirits)
            {
                enemytmp.Add(item);
            }
            this.SendEvent<InitPlayerElfPackageEvent>(new InitPlayerElfPackageEvent()//发送初始化精灵事件，并将玩家的精灵赋值给事件对象
            {
                playerElfs = playertmp,
                enemyElfs = enemytmp
            }) ;
        }

        public override void OnDestroy()
        {
        }

    }

}
