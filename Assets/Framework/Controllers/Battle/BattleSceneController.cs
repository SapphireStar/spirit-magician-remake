using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace ElfWizard
{
    public class BattleSceneController : MonoBehaviour,IController
    {
        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
        private void Start()
        {
            Transform player = transform.Find("Player/PlayerSpawnPoint");
            Transform enemy = transform.Find("Player/EnemySpawnPoint");
            GameObject playerGO = GameObject.Instantiate<GameObject>(ResourceManager.LoadObsolete<GameObject>("Player"), player.position, Quaternion.identity, player);
            GameObject enemyGO = GameObject.Instantiate<GameObject>(ResourceManager.LoadObsolete<GameObject>("Player"), enemy.position , Quaternion.Euler(new Vector3(0, 180, 0)), enemy);
            playerGO.tag = "Player";
            enemyGO.tag = "Enemy";
            playerGO.GetComponent<NewPlayerController>().UID = 2;
            enemyGO.GetComponent<NewPlayerController>().UID = -1;
            this.SendCommand<UpdateBattleInfoCommand>(new UpdateBattleInfoCommand() { player = playerGO.GetComponent<NewPlayerController>(), enemy= enemyGO.GetComponent<NewPlayerController>() });
        }


    }
}