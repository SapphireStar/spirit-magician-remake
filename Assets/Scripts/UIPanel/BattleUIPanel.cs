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
            this.RegisterEvent<GameStartEvent>(OnGameStart);
            this.GetModel<IBattleModel>().ToString();

            this.GetModel<IBattleModel>().PlayerHP.OnValueChanged += (value) =>
            {
                playerHealthBar.value = value;
            };
            this.GetModel<IBattleModel>().EnemyHP.OnValueChanged += (value) =>
            {
                enemyHealthBar.value = value;
            };

        }
        private void OnGameStart(GameStartEvent e)
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

        public void UpdateHealthBar()
        {

        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}