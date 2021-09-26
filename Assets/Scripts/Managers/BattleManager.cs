using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using PbBattle;
using Newtonsoft.Json;
using PbSpirit;
using ElfWizard.Events;
using Framework;
using ElfWizard.Manager;

namespace ElfWizard
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOSE }

    public interface IBattleSystem:ISystem
    {

    }

    public class BattleManager : BaseManagerSystem, IBattleSystem
    {
        public NewPlayerController player = Framework.ElfWizardArch.Get<BattleModel>().player;
        public NewPlayerController enemy = Framework.ElfWizardArch.Get<BattleModel>().enemy;
        public NewPlayerController currentTurn;
        public BattleRoundInfo curRoundInfo;
        public BattleRoundInfo nextRoundInfo;
        public PlayerBattleInfo playerBattleInfo;
        public PlayerBattleInfo enemyBattleInfo;

        public BattleInfo battleInfo;
        public GameObject GameStartUI;
        public int roundIndex;
        private int activeUID;
        private List<DiceInfo> diceInfo = new List<DiceInfo>();
        private DiceFormation diceFormation;
        private List<int> specialEffects;
        private List<BattleUnitCarriedSkill> buCarriedSkill = new List<BattleUnitCarriedSkill>();
        public int countDown;
        private int RemainTime = 20;


        private void Start()
        {

        }
        protected override void OnInit()
        {
            Framework.ElfWizardArch.Get<BattleModel>().RemainAttackElfs.OnValueChanged += ElfAttack;
            this.RegisterEvent<GameStartEvent>(SetInitialState);
            Debug.Log("init");
            player =GameObject.Instantiate(ResourceManager.LoadObsolete<GameObject>("Player"), GameObject.Find("PlayerSpawnPoint").transform).GetComponent<NewPlayerController>();
            enemy = GameObject.Instantiate(ResourceManager.LoadObsolete<GameObject>("Player"), GameObject.Find("EnemySpawnPoint").transform).GetComponent<NewPlayerController>();
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
            GameFacade.Instance.turn = BattleState.START;
            GameFacade.Instance.uiManager.PushPanel(UIPanelType.BattleUI);
            GameFacade.Instance.battleUI = GameObject.FindObjectOfType<BattleUIPanel>();

        }


        public void StartGame()
        {
            this.SendEvent<GameStartEvent>();
            GameFacade.Instance.StartCoroutine(CountingDown());
        }

        public void StartAttack()
        {
            Framework.ElfWizardArch.Get<BattleModel>().RemainAttackElfs.Value = currentTurn.PlayerElfs.Count;

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
/*            try
            {
                foreach (var item in info.SpecialEffects)
                {
                    specialEffects.Add(item);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("更新场地特殊效果失败: " + e);
            }*/
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
        public void SetInitialState(GameStartEvent e)
        {
            GameFacade.Instance.turn = BattleState.PLAYERTURN;
            currentTurn = player;
        }
        private static void translateInformation(PlayerBattleInfo from, PlayerBattleInfo target)
        {
            target.Uid = from.Uid;
            target.Hp = from.Hp;
            target.Dices = from.Dices;
        }
        int value = 0;

        /// <summary>
        /// 利用BindableProperty的功能进行顺序攻击
        /// </summary>
        /// <param name="Value"></param>
        private void ElfAttack(int Value)
        {
            Debug.Log(Value);
            if ( Value > 0)
            {
               currentTurn.PlayerElfs[value].GetComponent<Elf_Monobehavior>().UseSkill();
                value++;
            }
            else
            {
                value = 0;
                for (int j = 0; j < currentTurn.PlayerElfs.Count; j++)
                {
                    currentTurn.PlayerElfs[j].GetComponent<IBuffer>().UpdateBuff();
                }
                currentTurn.UpdateBuff();
                GameFacade.Instance.StartCoroutine(waitForNextRound());
            }
            
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

        public override void OnDestroy()
        {
            Framework.ElfWizardArch.Get<BattleModel>().RemainAttackElfs.OnValueChanged -= ElfAttack;
            this.UnregisterEvent<GameStartEvent>(SetInitialState);
        }
    }
}