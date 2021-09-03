using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using PbBattle;
using Newtonsoft.Json;
using PbSpirit;

namespace ElfWizard.Manager
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOSE }
    public class BattleManager : BaseManagerNonMono
    {
        public NewPlayerController player;
        public NewPlayerController enemy;
        public NewPlayerController currentTurn;
        public BattleRoundInfo curRoundInfo;
        public BattleRoundInfo nextRoundInfo;
        public PlayerBattleInfo playerBattleInfo;
        public PlayerBattleInfo enemyBattleInfo;

        public BattleInfo battleInfo;
        public GameObject GameStartUI;
        public static bool attacked;//用于接受精灵的攻击是否已经结束
        //int roundCount;//记录回合数，用于判断是否需要减少buff回合数
        public int roundIndex;
        private int activeUID;
        private List<DiceInfo> diceInfo = new List<DiceInfo>();
        private DiceFormation diceFormation;
        private List<int> specialEffects;
        private List<BattleUnitCarriedSkill> buCarriedSkill = new List<BattleUnitCarriedSkill>();
        public int countDown=10;
        public const int RemainTime = 20;

        public BattleManager(GameFacade facade) : base(facade)
        {
        }

        private void Start()
        {

        }
        public override void OnInit()
        {
            player =GameObject.Instantiate(ResourceManager.Load<GameObject>("Player"), GameObject.Find("PlayerSpawnPoint").transform).GetComponent<NewPlayerController>();
            enemy = GameObject.Instantiate(ResourceManager.Load<GameObject>("Player"), GameObject.Find("EnemySpawnPoint").transform).GetComponent<NewPlayerController>();
            battleInfo = new BattleInfo();
            BattleUnit unit1 = new BattleUnit();
            BattleUnit unit2 = new BattleUnit();
            Spirit s1 = new Spirit { Id = "1", Name = "ElfFire01", Specialist = SpecialistType.StFire, Level = 1, SkillDescription = "test1", Selected = true };
            Spirit s2 = new Spirit { Id = "2", Name = "ElfIce01", Specialist = SpecialistType.StIce, Level = 1, SkillDescription = "test2", Selected = true };
            Spirit s3 = new Spirit { Id = "3", Name = "EnemySlimeFire01", Specialist = SpecialistType.StFire, Level = 1, SkillDescription = "test3", Selected = true };
            unit1.Spirits.Add(s1);
            unit1.Spirits.Add(s2);
            unit2.Spirits.Add(s3);
            s1.Rarity = RarityType.RtCommon;
            s2.Rarity = RarityType.RtCommon;
            s3.Rarity = RarityType.RtCommon;
            battleInfo.MapID = 1;
            unit1.UserBaseInfo = new Base.UserBaseInfo();
            unit2.UserBaseInfo = new Base.UserBaseInfo();
            battleInfo = new BattleInfo();
            battleInfo.Players.Add(unit1);
            battleInfo.Players.Add(unit2);
            unit1.UserBaseInfo.Uid = 1;
            unit2.UserBaseInfo.Uid = 2;
            playerBattleInfo = new PlayerBattleInfo();
            enemyBattleInfo = new PlayerBattleInfo();
            playerBattleInfo.Hp = 500;
            enemyBattleInfo.Hp = 500;
            base.OnInit();
            GameFacade.Instance.turn = BattleState.START;
            GameFacade.Instance.uiManager.PushPanel(UIPanelType.BattleUI);
            GameFacade.Instance.battleUI = GameObject.FindObjectOfType<BattleUIPanel>();

        }


        public void StartGame()
        {
           GameFacade.Instance.StartCoroutine(GameStart());
        }
        IEnumerator GameStart()
        {
            GameObject Canvas = GameObject.Find("Canvas");
            GameStartUI = Canvas.transform.Find("GameStart").gameObject;
            Debug.Log(GameStartUI.name);
            GameStartUI.SetActive(true);
            yield return new WaitForSeconds(2);
            GameStartUI.SetActive(false);
            SetInitialState(BattleState.PLAYERTURN);
            GameFacade.Instance.StartCoroutine(CountingDown());
/*            GameFacade.Instance.turn = Random.Range(0, 1) < 0.5f ? BattleState.PLAYERTURN : BattleState.ENEMYTURN;
            GameFacade.Instance.SetCurrentTurn(GameFacade.Instance.turn);
            if (GameFacade.Instance.turn == BattleState.PLAYERTURN)
                GameFacade.Instance.currentTurn = GameFacade.Instance.player;
            else GameFacade.Instance.currentTurn = GameFacade.Instance.enemy;   */
        }
        public void StartAttack()
        {
            GameFacade.Instance.StopAllCoroutines();
            GameFacade.Instance.StartCoroutine(SequenceAttack());

        }
        public void updateRoundInfo(BattleRoundInfo currentRI, BattleRoundInfo nextRI = null)
        {
            curRoundInfo = currentRI;
            if(nextRI!=null)
                nextRoundInfo = nextRI;

        }
        public void UpdateBattleRoundInfo(BattleRoundInfo info, S2C_UpdateBattleAction battleAction)
        {
            Debug.Log(JsonConvert.SerializeObject(info));
 
            foreach (var item in battleAction.PlayerBattleInfos)
            {
                if (item.Uid == Userdata.GetUid())
                {
                    translateInformation(item, playerBattleInfo);
                }
                else
                {
                    translateInformation(item, enemyBattleInfo);
                }
            }
            
                roundIndex = info.RoundIndex;
                activeUID = info.ActiveUID;
                foreach (var item in info.DiceInfo)
                {
                    diceInfo.Add(item);
                }
                diceFormation = info.Formation;
            try
            {
                foreach (var item in info.SpecialEffects)
                {
                    specialEffects.Add(item);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("更新场地特殊效果失败: " + e);
            }
                foreach (var item in info.BuCarriedSkill)
                {
                    buCarriedSkill.Add(item);
                }
            //}
/*            catch (System.Exception e)
            {
                Debug.Log("BattleRoundInfo信息错误，无法更新当前回合战斗信息: " + e);
            }*/


        }
        public void SetInitialState(BattleState state)
        {
            GameFacade.Instance.turn = state;
            if (state == BattleState.ENEMYTURN)
                currentTurn = enemy;
            else currentTurn = player;
        }
        private static void translateInformation(PlayerBattleInfo from, PlayerBattleInfo target)
        {
            target.Uid = from.Uid;
            target.Hp = from.Hp;
            target.Dices = from.Dices;
        }

        /// <summary>
        /// 向服务器发送开始攻击的信息
        /// </summary>
        /// <returns></returns>
        IEnumerator SequenceAttack()//根据精灵顺序进行攻击，开启协程后，让第一个精灵使用技能，根据静态变量attacked来判断上一个精灵的攻击是否已经完成，attacked参数由skilldeployer进行修改，当attacked被skilldeployer
                                    //调用后，值为true，此时，经过0.5秒后再让下一个精灵进行攻击
        {
            
            //yield return new WaitForSeconds(0.5f);
           NewPlayerController  player = currentTurn.GetComponent<NewPlayerController>();
            int i = 0;
            while (i < player.PlayerElfs.Count)
            {
                player.PlayerElfs[i].GetComponent<Elf_Monobehavior>().UseSkill();
                while (!player.PlayerElfs[i].GetComponent<Elf_Monobehavior>().attacked)
                {

                    yield return null;
                }
                yield return new WaitForSeconds(1f);
                attacked = false;
                i++;

            }

            attacked = false;
            for (int j = 0; j < player.PlayerElfs.Count; j++)
            {
                player.PlayerElfs[j].GetComponent<IBuffer>().UpdateBuff();
            }
            player.UpdateBuff();
            GameFacade.Instance.StartCoroutine(waitForNextRound());
        }

        IEnumerator waitForNextRound()
        {
            GameFacade.Instance.uiManager.PushPanel(UIPanelType.SwitchSide);

            yield return new WaitForSeconds(0.8f);
            //TODO如果其中一方死亡，退出战斗回合，出现结算画面
            GameFacade.Instance.SwitchRound();
            countDown = RemainTime;
            GameFacade.Instance.StartCoroutine(CountingDown());
        }
        IEnumerator CountingDown()
        {
            if (countDown <= 0)
            {
                countDown = RemainTime;
                BattleRequest.SendRequest(BattleActionType.BatTimeout);
                //StartAttack();
            }
            else
            {
                yield return new WaitForSeconds(1);
                countDown--;
                GameFacade.Instance.StartCoroutine(CountingDown());
            }
        }

    }
}