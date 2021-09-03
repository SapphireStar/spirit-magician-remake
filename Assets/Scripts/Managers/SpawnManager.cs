using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PbSpirit;
using PbBattle;
using System;

namespace ElfWizard.Manager
{
    public enum ElementType
    {
        ST_None = 0,
        ST_Fire = 1,
        ST_Ice = 2,
        ST_Holy = 3,
        ST_Evil = 4,
        ST_Natural = 5,
        ST_Mystery = 6,
        ST_Magician = 100
    }
    public class SpawnManager : BaseManager
    {
        public List<Spirit> playerElfPackage;
        public List<Spirit> enemyElfPackage;
        public static SpawnManager Instance;
        int[] PlayerElement;
        int[] EnemyElement;
        public ElementSlotHolder elementSlotHolder;
        BattleManager battleManager;
/*        public List<ElementType> checkPlayerHasType = new List<ElementType>();
        public List<ElementType> checkEnemyHasType = new List<ElementType>();*/

        void Start()
        {
/*
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else Destroy(this.gameObject);*/
/*            playerElfPackage.Add(new Spirit { Id = "1", Name = "ElfFire01", Specialist = SpecialistType.StFire, Rarity = 1, Level = 1, SkillDescription = "", Selected = true });
            enemyElfPackage.Add(new Spirit { Id = "1", Name = "ElfFire01", Specialist = SpecialistType.StFire, Rarity = 1, Level = 1, SkillDescription = "", Selected = true });*/
        }
        public override void OnInit()
        {
            base.OnInit();
            battleManager = GameFacade.Instance.battleManager;
            playerElfPackage = new List<Spirit>();
            enemyElfPackage = new List<Spirit>();
            facede = GameFacade.Instance;
            //playerElfPackage = GenerateElf.InitPackage(将服务器传入的二进制Spirit集合通过protobuf转化成SpiritPackage类，然后在这个方法中将SpiritPackage类中包含的所有精灵数据赋值给playerElfPackage);
            //enemy同理
/*            playerElfPackage = GenerateElf.InitPackage("Assets/Scripts/protobuf/src/testSpPack.bin");
            enemyElfPackage = GenerateElf.InitPackage("Assets/Scripts/protobuf/src/enemytestSpPack.bin");*/
/*            Debug.Log(playerElfPackage[0].Name + " " + playerElfPackage[1].Name);
            Debug.Log(enemyElfPackage[0].Name);*/
        }

        public void OnGUI()
        {
/*       if (GUI.Button(new Rect(10, 400, 100, 200),"generatetest"))
            {
                GameObjectPool.Instance.CreateObject("ElfFire01", ResourceManager.Load<GameObject>("ElfFire01"), new Vector3(0, 0, 0), Quaternion.identity);
            }*/
        }
/*        public void RollElements(BattleState turn)
        {
            if (turn == BattleState.PLAYERTURN)
            {
                RollPlayerElements();
            }
            else RollEnemyElements();
        }*/
        public void SetUpPlayerElement(List<SpecialistType> dices)
        {
            elementSlotHolder.ClearSlotSet();
            foreach (var item in elementSlotHolder.ElementSlots)
            {
                item.gameObject.SetActive(false);//下一次roll点前将所有slot关闭，不然之前roll出的元素图标会依旧存在
            }
            for (int i = 0; i < dices.Count; i++)
            {
                elementSlotHolder.SetUp(dices[i]);
            }
        }
/*         void RollPlayerElements()
        {

            PlayerElement = new int[facede.playerBattleInfo.Dices];
            elementSlotHolder.ClearSlotSet();
            foreach (var item in elementSlotHolder.ElementSlots)
            {
                item.gameObject.SetActive(false);//下一次roll点前将所有slot关闭，不然之前roll出的元素图标会依旧存在
            }
            int[] roll = new int[facede.playerBattleInfo.Dices];
            for (int i = 0; i < facede.playerBattleInfo.Dices; i++)
            {

                roll[i] = UnityEngine.Random.Range(1, 6);
                elementSlotHolder.SetUp((ElementType)roll[i]);


            }
        }*/
/*         void RollEnemyElements()
        {
            EnemyElement = new int[facede.enemyBattleInfo.Dices];
            elementSlotHolder.ClearSlotSet();
            foreach (var item in elementSlotHolder.ElementSlots)
            {
                item.gameObject.SetActive(false);//下一次roll点前将所有slot关闭，不然之前roll出的元素图标会依旧存在
            }
            int[] roll = new int[facede.enemyBattleInfo.Dices];
            for (int i = 0; i < facede.enemyBattleInfo.Dices; i++)
            {

                roll[i] = Random.Range(1, 6);
                elementSlotHolder.SetUp((ElementType)roll[i]);


            }
        }*/
        int CheckLevel(int amount)//判断生成的精灵的等级
        {
            if (amount >= 5)
                return 3;
            else if (amount == 3 || amount == 4)
                return 2;
            else return 1;
        }
        public void UpdatePlayerElfs(BattleRoundInfo roundInfo)
        {
            Dictionary<int, string> currentPlayerElfs = new Dictionary<int, string>
                    {
                        { 0, "ElfFire01" },
                        { 1, "ElfNature01" },
                        { 2, "ElfIce02" },
                        { 3, "ElfHoly01" },
                        { 4, "ElfEvil01" },
                        { 5, "ElfHoly01" },
                        { 6, "ElfNature01" },
                        { -1,"ElfFire01" },
                        { 7, "ElfEvil01" },
                    };//TODO:未来根据不同的用户创建不同的映射

            List<int> curElfUID = new List<int>();
            foreach (var item in GameFacade.Instance.battleManager.currentTurn.PlayerElfs)
            {
                curElfUID.Add(item.GetComponent<Elf_Monobehavior>().ElfID);
            }

            foreach (var item in roundInfo.BuCarriedSkill)
            {
                Debug.Log("elfID: " + item.CarriedSkills.Count);
/*                if (!curElfUID.Contains(int.Parse(item.CarriedSkills[0].SkillID)))
                {
                    GameFacade.Instance.currentTurn.AddElf(currentPlayerElfs[item.Uid], 1, int.Parse(item.CarriedSkills[0].SkillID));
                                                                                                    //这里提供需要添加的精灵的名称，等级，
                                                                                                    //和uid，uid用于给生成的精灵做标记，
                                                                                                    //以区分对应精灵是否已经存在于场上，
                                                                                                    //假定uid是唯一的，即使是相同精灵，uid也不同
                }*/
            }
            

        }
        public void AskIfSpawn(ElementType type,int amount)
        {

            if (amount >= 2)//防止按照slot加入list的顺序来获取对应的amount，而游戏中生成的元素顺序是随机的,因此使用find
            {

                if (GameFacade.Instance.turn == BattleState.PLAYERTURN)
                {
                    foreach (var item in playerElfPackage)
                    {
                        if (item.Specialist == (PbSpirit.SpecialistType)type)
                        {
                            SpawnElfofType(type,CheckLevel(amount));
                            foreach (var slot in elementSlotHolder.ElementSlots)
                            {
                                slot.Init();
                            }
                            break;
                        }

                    }
                }
                else
                {
                    foreach (var item in enemyElfPackage)
                    {
                        if (item.Specialist == (PbSpirit.SpecialistType)type)
                        {
                            SpawnElfofType(type,CheckLevel(amount));
                            foreach (var slot in elementSlotHolder.ElementSlots)
                            {
                                slot.Init();
                            }
                            break;
                        }

                    }
                }

            }
        }
        /// <summary>
        /// 若未来要使用此方法，需要注意为该方法传入uid，并将最后一句代码的uid位置的0改为uid
        /// </summary>
        /// <param name="type"></param>
        /// <param name="level"></param>
        public void SpawnElfofType(ElementType type,int level)
        {
            List<string> elf = new List<string>();
            if (GameFacade.Instance.turn == BattleState.PLAYERTURN)
            {
                foreach (var item in playerElfPackage)
                {
                    if (item.Specialist == (PbSpirit.SpecialistType)type)
                    {
                        elf.Add(item.Name);
                    }
                }
            }
            else
            {
                foreach (var item in enemyElfPackage)
                {
                    if (item.Specialist == (PbSpirit.SpecialistType)type)
                    {
                        elf.Add(item.Name);
                    }
                }
            }

            battleManager.currentTurn.AddElf(elf[UnityEngine.Random.Range(0, elf.Count)],level,0);
        }
        public void SetCurrentTurn(BattleState state)
        {

            if (state == BattleState.PLAYERTURN)
            {
                battleManager.currentTurn = battleManager.player;

            }
            else if (state == BattleState.ENEMYTURN)
            {
                battleManager.currentTurn = battleManager.enemy;
            }
        }
        public void InitPlayerPackage(List<Spirit> playerSpirits,List<Spirit> enemySpirits)
        {
            foreach (var item in playerSpirits)
            {
                playerElfPackage.Add(item);
            }
            foreach (var item in enemySpirits)
            {
                enemyElfPackage.Add(item);
            }
        }
        public void SetPlayerElementAmount()
        {

        }


    }
}