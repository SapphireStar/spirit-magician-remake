using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PbBattle;
using ElfWizard.Manager;
using ElfWizard.Events;
using Framework;

namespace ElfWizard {
    public interface IBattleUIController:IController
    {

    }
    /// <summary>
    /// 用于显示战斗信息
    /// </summary>
    public class BattleUIPanel : BasePanel, IBattleUIController
    {
        public Slider playerHealthBar;
        public Slider enemyHealthBar;
        public Image playerAvatar;
        public Image playerFrame;
        public Image enemyAvatar;
        public Image enemyFrame;
        BattleManager battleManager;

        private void Start()
        {
            
        }
        public override void OnEnter()
        {
            base.OnEnter();
            GameStartEvent.Register(OnGameStart);
            battleManager = GameFacade.Instance.battleManager;
            playerHealthBar.maxValue = battleManager.playerBattleInfo.Hp;
            enemyHealthBar.maxValue = battleManager.enemyBattleInfo.Hp;
            playerHealthBar.value = playerHealthBar.maxValue;
            enemyHealthBar.value = enemyHealthBar.maxValue;
            //playerAvatar.sprite= ResourceManager.Load<Sprite>("playerAvatarName");
            //playerFrame.sprite= ResourceManager.Load<Sprite>("playerFrameName");
            
        }
        private void OnGameStart()
        {
            StartCoroutine(GameStart());
        }
        IEnumerator GameStart()
        {
              GameObject GameStartUI = transform.parent.Find("GameStart").gameObject;
              Debug.Log(GameStartUI.name);
              GameStartUI.SetActive(true);
              yield return new WaitForSeconds(2);
              GameStartUI.SetActive(false);

        }
        public void InitBattlePanel(float playerHp,float enemyHp)
        {
            playerHealthBar.maxValue = battleManager.playerBattleInfo.Hp;
            enemyHealthBar.maxValue = battleManager.enemyBattleInfo.Hp;
            playerHealthBar.value = playerHealthBar.maxValue;
            enemyHealthBar.value = enemyHealthBar.maxValue;
        }
        public void UpdateHealthBar(BattleRoundInfo info)
        {
            /*            if (player.tag == "Player")
                        {
                            playerHealthBar.value = info.;
                        }
                        else if (player.tag == "Enemy")
                        {
                            enemyHealthBar.value -= health;
                        }*/
            foreach (var item in info.PlayerBattleInfos)
            {
                if (item.Uid == battleManager.playerBattleInfo.Uid)
                {
                    playerHealthBar.value = item.Hp;
                }
                else if (item.Uid == battleManager.enemyBattleInfo.Uid)
                {
                    enemyHealthBar.value = item.Hp;
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}