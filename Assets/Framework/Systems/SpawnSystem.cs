using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard;
using PbBattle;

namespace Framework
{
    public interface ISpawnSystem : ISystem 
    {
        void SpawnElf(int activeID, SkillEffect Elf);
    }

    public class SpawnSystem : AbstractSystem, ISpawnSystem
    {
        IResourceUtility resourceUtility;
        IBattleModel battleModel;
        NewPlayerController player;
        NewPlayerController enemy;
        List<Elf_Monobehavior> playerElfList;
        List<Elf_Monobehavior> enemyElfList;
        bool[] PlayerElfReminder = new bool[3];
        bool[] EnemyElfReminder = new bool[3];

        protected override void OnInit()
        {
            this.RegisterEvent<SetupBattleSceneEvent>((e) => { player = battleModel.player;enemy = battleModel.enemy; });
            playerElfList = new List<Elf_Monobehavior>(3);
            enemyElfList = new List<Elf_Monobehavior>(3);
            battleModel = this.GetModel<IBattleModel>();
            resourceUtility = this.GetUtility<IResourceUtility>();

        }

        public void SpawnElf(int activeID,SkillEffect Elf)
        {
            
            if (activeID == battleModel.player.UID)
            {
                int index = checkInsertPlace(PlayerElfReminder);
                if (index == -1)
                {
                    GameObjectPool.Instance.CollectObject(playerElfList[0].gameObject);
                    playerElfList.RemoveAt(0);
                    for (int i = playerElfList.Count-1; i >= 0 ; i--)
                    {
                        playerElfList[i].transform.position = battleModel.PlayerSpawnPoints[i];
                    }
                    createElfAtIndex(2, Elf, activeID);
                }
                else
                {

                    createElfAtIndex(index, Elf, activeID);
                }
                player.PlayerElfs.Clear();
                for (int i = 0; i < playerElfList.Count; i++)
                {
                    player.PlayerElfs.Add(playerElfList[i].gameObject);
                }

            }
            else
            {
                int index = checkInsertPlace(EnemyElfReminder);
                if (index == -1)
                {
                    GameObjectPool.Instance.CollectObject(enemyElfList[0].gameObject);
                    enemyElfList.RemoveAt(0);
                    for (int i = enemyElfList.Count - 1; i >= 0; i--)
                    {
                        enemyElfList[i].transform.position = battleModel.EnemySpawnPoints[i];
                    }
                    createElfAtIndex(2, Elf, activeID);
                }
                else
                {
                    createElfAtIndex(index, Elf, activeID);
                }
                enemy.PlayerElfs.Clear();
                for (int i = 0; i < enemyElfList.Count; i++)
                {
                    enemy.PlayerElfs.Add(enemyElfList[i].gameObject);
                }
            }
        }
        void createElfAtIndex(int index, SkillEffect Elf,int activeID)
        {
            if (activeID == battleModel.player.UID)
            {
                GameObject newElf = GameObjectPool.Instance.CreateObject(Elf.SkillID, resourceUtility.Load<GameObject>(Elf.SkillID), battleModel.PlayerSpawnPoints[index], Quaternion.identity);
                playerElfList.Insert(index,newElf.GetComponent<Elf_Monobehavior>());
                PlayerElfReminder[playerElfList.IndexOf(newElf.GetComponent<Elf_Monobehavior>())] = true;
            }
            else
            {
                GameObject newElf = GameObjectPool.Instance.CreateObject(Elf.SkillID, resourceUtility.Load<GameObject>(Elf.SkillID), battleModel.EnemySpawnPoints[index], Quaternion.identity);
                enemyElfList.Insert(index,newElf.GetComponent<Elf_Monobehavior>());
                EnemyElfReminder[enemyElfList.IndexOf(newElf.GetComponent<Elf_Monobehavior>())] = true;
            }
        }
        int checkInsertPlace(bool[] Reminder)
        {
            for (int i = 0; i < Reminder.Length; i++)
            {
                if (!Reminder[i])
                {
                    return i;
                }
            }
            return -1;
        }
        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }


    }
}