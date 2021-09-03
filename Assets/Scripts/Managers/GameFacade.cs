using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Manager;
using Common;
using UnityEngine.SceneManagement;
using PbBattle;
using System.IO;
using PbSpirit;

namespace ElfWizard
{
    public class GameFacade : MonoBehaviour
    {
        /// <summary>
        /// battle Informations
        /// </summary>
       // public BattleActionType actionType;
        public NewPlayerController player;
        public NewPlayerController enemy;
        public NewPlayerController currentTurn;
        public PlayerBattleInfo playerBattleInfo = new PlayerBattleInfo();
        public PlayerBattleInfo enemyBattleInfo = new PlayerBattleInfo();
        public BattleInfo battleInfo;
        public List<int> specialEffects = new List<int>();
        public BattleRoundInfo curRoundInfo;
        public BattleRoundInfo nextRoundInfo;
        /// <summary>
        /// Managers
        /// </summary>
        public BattleManager battleManager;
        public SpawnManager spawnManager;
        public RequestManager requestManager;
        public AudioManager audioManager;
        public UIManager uiManager;
        BattleSceneManager BSManager;

        public Test testnet;
        /// <summary>
        /// UIPanels
        /// </summary>
        public BattleUIPanel battleUI;

        private static GameFacade _instance;
        public BattleState turn;
        public static GameFacade Instance { get { return _instance; } }
        private void OnGUI()
        {
/*            if(GUI.Button( new Rect(500, 500, 300, 100), "readBttleInfo"))
            {
                BattleInfo temp = new BattleInfo();
                temp.Id = "3";
                temp.Type = BattleType.BtPvp;
                temp.RoundNum = 3;
                temp.SpecialEffects.Add(new List<int> { 1, 2, 3, 4, 5 });
                temp.MapID = 3;
                temp.DefaultDices = 5;
                temp.EndTime = 60;
                temp.StartTime = 10;
                BattleUnit unit = new BattleUnit();
                temp.Players.Add(unit);
                byte[] info = new byte[temp.CalculateSize()];

                Google.Protobuf.CodedOutputStream buffer = new Google.Protobuf.CodedOutputStream(info);
                temp.WriteTo(buffer);
                BattleInfo battleInfo = BattleInfo.Parser.ParseFrom(info);
               // BattleInfo battleInfo = ProtobufManager.ReadBattleInfo(info);
                Debug.Log(battleInfo.Id + " " + battleInfo.Type +" "+battleInfo.MapID+ " " + battleInfo.RoundNum + " " + battleInfo.SpecialEffects[3]);
            }*/
        }
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                _instance = this;
            }
            DontDestroyOnLoad(gameObject);
            /*            battleManager = new BattleManager();
                        spawnManager = new SpawnManager();*/
        }

        void Start()
        {
           Init();
        }
        public void Init()
        {
            try
            {
                player = Instantiate(ResourceManager.Load<GameObject>("Player"), GameObject.Find("PlayerSpawnPoint").transform).GetComponent<NewPlayerController>();
                enemy = Instantiate(ResourceManager.Load<GameObject>("Player"), GameObject.Find("EnemySpawnPoint").transform).GetComponent<NewPlayerController>();

            }
            catch (System.Exception e)
            {
                Debug.Log("无法生成玩家: " + e);
            }

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

            unit1.UserBaseInfo = new Base.UserBaseInfo();
            unit2.UserBaseInfo = new Base.UserBaseInfo();
            battleInfo = new BattleInfo();
            battleInfo.Players.Add(unit1);
            battleInfo.Players.Add(unit2);
            unit1.UserBaseInfo.Uid = 1;
            unit2.UserBaseInfo.Uid = 2;
            playerBattleInfo.Hp = 20;
            enemyBattleInfo.Hp = 20;

            battleManager = GameObject.FindObjectOfType<BattleManager>();
            spawnManager = GameObject.FindObjectOfType<SpawnManager>();
            requestManager = new RequestManager(this);
           // clientManager = new ClientManager(this);
            uiManager = new UIManager(this);
            BSManager = new BattleSceneManager(this);
            audioManager = new AudioManager(this);
            //testLogin = gameObject.GetComponent<Test>();

            InitManagers();
  
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote)&&GameObject.FindObjectOfType<CommandPanel>()==null)
            {
                uiManager.PushPanel(UIPanelType.Command);
            }
        }
        void InitManagers()
        {

            if (battleManager != null && spawnManager != null)
            {
                battleManager.OnInit();
                spawnManager.OnInit();
            }
            uiManager.OnInit();
            BSManager.OnInit();
            requestManager.OnInit();
            audioManager.OnInit();
            //clientManager.OnInit();
            if(SceneManager.GetActiveScene().name== "MainMenu")
            {
                uiManager.PushPanel(UIPanelType.Start);
            }
            //testLogin.Init();
        }
        public void SetInitialState(BattleState state)
        {
            turn = state;
            if (state == BattleState.ENEMYTURN)
                currentTurn = enemy;
            else currentTurn = player;
        }
        public void StartGame()
        {
            battleUI.InitBattlePanel(player.health, enemy.health);
            battleManager.StartGame();
        }
        public void StartAttack()
        {
            battleManager.StartAttack();
        }
        public void SendBattleAction(BattleActionType actionType, int[] lockedDices = null)
        {
            testnet.SendBattleAction(actionType, lockedDices);
        }
        public void RollElement()
        {
            //clientManager.SendRequest(RequestCode.User, ActionCode.Communicate, "随机生成元素");
            //spawnManager.RollElements(turn);
        }
        public void AskIfSpawn(ElementType type, int amount)
        {
            spawnManager.AskIfSpawn(type,amount);
        }
        public void SwitchRound()
        {
            if (turn == BattleState.PLAYERTURN)
            {

                SwitchToEnemy();

            }

            else if (turn == BattleState.ENEMYTURN)
            {

                SwitchToPlayer();
            }

        }
        public void SwitchToEnemy()//设置当前回合用来切换生成精灵的位置和种类
        {
            GameFacade.Instance.SetCurrentTurn(BattleState.ENEMYTURN);
            turn = BattleState.ENEMYTURN;
            currentTurn = enemy;
        }
        public void SwitchToPlayer()
        {
            GameFacade.Instance.SetCurrentTurn(BattleState.PLAYERTURN);
            turn = BattleState.PLAYERTURN;
            currentTurn = player;
        }

        public void UpdateRoundInfo(BattleRoundInfo currentRI, BattleRoundInfo nextRI = null)
        {
            curRoundInfo = currentRI;
            nextRoundInfo = nextRI;
            
        }
        public void SetCurrentTurn(BattleState state)
        {
            spawnManager.SetCurrentTurn(state);
        }
        public List<Spirit> GetUnitSpirits(BattleUnit unit)
        {
            return ProtobufManager.GetUnitSpirits(unit);
        }
        public void LoadSceneAsync(string sceneToLoad,BattleInfo battleInfo = null)
        {
            Constants.SceneToLoad = sceneToLoad;
            this.battleInfo = battleInfo;
            SceneManager.LoadScene("Loading");
        }
        public void HitUI(Vector3 position, float damage)
        {
            Vector3 offset = new Vector3(300, 0, 0);
            Vector3 _position = Camera.main.WorldToScreenPoint(position);
/*            float width = Screen.width;
            float height = Screen.height;
            float currentWidth =(float) offset.x * width / 1080.0f;
            float currentHeight = (float)offset.y * height / 1920.0f;*/
            GameObject hitUI = GameObjectPool.Instance.CreateObject("HitUI", ResourceManager.Load<GameObject>("HitUI"), _position + offset, Quaternion.identity);
            hitUI.GetComponent<HitAnim>().SetDamage(damage);
        }
        public void AddRequest(ActionCode actionCode, BaseRequest request)
        {
            requestManager.AddRequest(actionCode, request);
        }
        public void RemoveRequest(ActionCode actionCode)
        {
            requestManager.RemoveRequest(actionCode);
        }
        public void HandleResponse(ActionCode actionCode, string data)
        {
            requestManager.HandleResponse(actionCode, data);
        }
        public void ShowMessage(string msg)
        {
            uiManager.ShowMessage(msg);
        }
        public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
        {
           // clientManager.SendRequest(requestCode, actionCode, data);
        }

        public void InitPlayerPackage(List<Spirit> playerSpirits, List<Spirit> enemySpirits)
        {
            spawnManager.InitPlayerPackage(playerSpirits, enemySpirits);
        }
        private void OnDestroy()
        {
            try
            {
                if (battleManager != null && spawnManager != null)
                {
                    battleManager.OnDestroy();
                    spawnManager.OnDestroy();
                }
                requestManager.OnDestroy();
                //clientManager.OnDestroy();
                uiManager.OnDestroy();
                GameObjectPool.Instance.ClearAll();
            }
            catch (System.Exception e)
            {
                Debug.Log("销毁管理器失败: " + e);
            }

        }


    }
}
