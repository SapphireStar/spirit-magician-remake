using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Manager;
using Common;
using UnityEngine.SceneManagement;
using PbBattle;
using System.IO;
using PbSpirit;
using System;

namespace ElfWizard
{
    public class GameFacade : MonoBehaviour
    {
        /// <summary>
        /// battle Informations
        /// </summary>
       // public BattleActionType actionType;
        /// <summary>
        /// Managers
        /// </summary>
        public BattleManager battleManager;
        public SpawnManager spawnManager;
        public RequestManager requestManager;
        public AudioManager audioManager;
        public UIManager uiManager;
        public BattleSceneManager BSManager;

        public Test testnet;
        /// <summary>
        /// UIPanels
        /// </summary>
        public BattleUIPanel battleUI;

        private static GameFacade _instance;
        public BattleState turn;
        public static GameFacade Instance { get { return _instance; } }

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



            //battleManager = new BattleManager(this);
            spawnManager = GameObject.FindObjectOfType<SpawnManager>();
/*            requestManager = new RequestManager(this);
           // clientManager = new ClientManager(this);
            uiManager = new UIManager(this);
            BSManager = new BattleSceneManager(this);
            audioManager = new AudioManager(this);*/
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

                spawnManager.OnInit();
            }
/*            //battleManager.OnInit();
            uiManager.OnInit();
            //BSManager.OnInit();
            requestManager.OnInit();
            audioManager.OnInit();
            //clientManager.OnInit();*/
            if(SceneManager.GetActiveScene().name== "MainMenu")
            {
                uiManager.PushPanel(UIPanelType.Start);
            }
            //testLogin.Init();
        }

        public void StartGame()
        {
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
        }
        public void SwitchToPlayer()
        {
            GameFacade.Instance.SetCurrentTurn(BattleState.PLAYERTURN);
            turn = BattleState.PLAYERTURN;
        }

        public void UpdateRoundInfo(BattleRoundInfo currentRI, BattleRoundInfo nextRI = null)
        {
            battleManager.updateRoundInfo(currentRI, nextRI);
            
        }
        public void SetCurrentTurn(BattleState state)
        {
            spawnManager.SetCurrentTurn(state);
        }
        public List<Spirit> GetUnitSpirits(BattleUnit unit)
        {
            List<Spirit> spirits = new List<Spirit>();
            foreach (var item in unit.Spirits)
            {
                spirits.Add(item);
            }
            return spirits;
        }

        public void LoadSceneAsync(string sceneToLoad, Action Initializations, BattleInfo battleInfo = null)
        {
            Constants.action = Initializations;
            Constants.SceneToLoad = sceneToLoad;//TODO:这里后期需要服务端与客户端统一地图命名规则，将SceneToLoad设为battleInfo中的mapID
            battleManager.battleInfo = battleInfo;
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
