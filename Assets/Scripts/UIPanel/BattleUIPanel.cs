using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PbBattle;
using ElfWizard.Manager;
using ElfWizard.Events;
using Framework;
using DG.Tweening;


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
        public Slider countDownBar;
        public Image playerAvatar;
        public Image playerFrame;
        public Image enemyAvatar;
        public Image enemyFrame;
        Button EndTurnButton;

        BattleManager battleManager;
        IBattleModel battleModel;


        public override void OnEnter()
        {
            this.RegisterEvent<GameStartEvent>(OnGameStart);
            this.RegisterEvent<EndAttackEvent>(OnEndAttack);
            this.RegisterEvent<StartAttackEvent>(OnAttackStart);
            this.RegisterEvent<BattleRollEvent>(OnBattleRollEvent);
            this.RegisterEvent<BattleRollEvent>(OnCountDownChange);//开始倒计时
            battleModel = this.GetModel<IBattleModel>();
            EndTurnButton = transform.Find("Control Panel/EndTurn").GetComponent<Button>();
            EndTurnButton.onClick.AddListener(OnEndTurnClicked);
        }
        private void OnGameStart(GameStartEvent e)
        {
            playerHealthBar.maxValue = battleModel.GetPlayerBattleInfoByUid(battleModel.player.UID).Hp;
            enemyHealthBar.maxValue = battleModel.GetPlayerBattleInfoByUid(battleModel.enemy.UID).Hp;
            playerHealthBar.value = playerHealthBar.maxValue;
            enemyHealthBar.value = enemyHealthBar.maxValue;
            countDownBar.maxValue = battleModel.countDown;
            countDownBar.value = battleModel.countDown;

        }
        Coroutine countDown;//用于获取计时器开启的协程
        private void OnAttackStart(StartAttackEvent e)
        { 
            EndTurnButton.transform.DOLocalMove(new Vector3(700, -480, 0), 0.5f);
            countDownBar.transform.DOLocalMove(new Vector3(700, 0, 0), 0.5f);
            StopCoroutine(countDown);//开始攻击后停止计时
        }
        private void OnEndAttack(EndAttackEvent e)
        {
            Debug.Log("update hp");
            playerHealthBar.value = battleModel.PlayerHP.Value;
            enemyHealthBar.value = battleModel.EnemyHP.Value;
        }
        private void OnCountDownChange(BattleRollEvent e)
        {
            float cur = battleModel.countDown;
            countDown = StartCoroutine(CountDown(cur));

        }
        private void OnBattleRollEvent(BattleRollEvent e)
        {
            EndTurnButton.transform.DOLocalMove(new Vector3(380, -480, 0), 0.5f);
            countDownBar.transform.DOLocalMove(new Vector3(500, 0, 0), 0.5f);
        }
        private void OnEndTurnClicked()
        {
            this.SendCommand<SendAttackBattleActionCommand>();
            EndTurnButton.transform.DOLocalMove(new Vector3(700, -480, 0), 0.5f);
            countDownBar.transform.DOLocalMove(new Vector3(700, 0, 0), 0.5f);
        }
        IEnumerator GameStart()
        {
            GameObject GameStartUI = transform.parent.Find("GameStart").gameObject;
            Debug.Log(GameStartUI.name);
            GameStartUI.SetActive(true);
            yield return new WaitForSeconds(2);
            GameStartUI.SetActive(false);

        }

        public override void OnExit()
        {
            base.OnExit();
            this.UnregisterEvent<GameStartEvent>(OnGameStart);
            this.UnregisterEvent<EndAttackEvent>(OnEndAttack);
        }
        IEnumerator CountDown(float time)
        {
            Debug.Log("numerator countdown");
            while (time >= 0)
            {
                time -= 0.1f;
                countDownBar.value = time;
                if (time <= 0)
                    this.SendCommand<SendAttackBattleActionCommand>();
                yield return new WaitForSeconds(0.1f);
            }

        }
    }
}