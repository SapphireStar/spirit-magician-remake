using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using PbBattle;
using Newtonsoft.Json;

namespace ElfWizard.Manager
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOSE }
    public class BattleManager: BaseManager
    {
        public GameObject GameStartUI;
        public static bool attacked;//���ڽ��ܾ���Ĺ����Ƿ��Ѿ�����
        //int roundCount;//��¼�غ����������ж��Ƿ���Ҫ����buff�غ���
        public int roundIndex;
        private int activeUID;
        private List<DiceInfo> diceInfo = new List<DiceInfo>();
        private DiceFormation diceFormation;
        private List<int> specialEffects;
        private List<BattleUnitCarriedSkill> buCarriedSkill = new List<BattleUnitCarriedSkill>();
        public int countDown=10;
        public const int RemainTime = 20;

        
        private void Start()
        {

        }
        public override void OnInit()
        {
            base.OnInit();
            GameFacade.Instance.turn = BattleState.START;
            GameFacade.Instance.uiManager.PushPanel(UIPanelType.BattleUI);
            GameFacade.Instance.battleUI = GameObject.FindObjectOfType<BattleUIPanel>();

        }


        public void StartGame()
        {
            StartCoroutine(GameStart());
        }
        IEnumerator GameStart()
        {
            GameStartUI.SetActive(true);
            yield return new WaitForSeconds(2);
            GameStartUI.SetActive(false);
            GameFacade.Instance.SetInitialState(BattleState.PLAYERTURN);
            StartCoroutine(CountingDown());
/*            GameFacade.Instance.turn = Random.Range(0, 1) < 0.5f ? BattleState.PLAYERTURN : BattleState.ENEMYTURN;
            GameFacade.Instance.SetCurrentTurn(GameFacade.Instance.turn);
            if (GameFacade.Instance.turn == BattleState.PLAYERTURN)
                GameFacade.Instance.currentTurn = GameFacade.Instance.player;
            else GameFacade.Instance.currentTurn = GameFacade.Instance.enemy;   */
        }
        public void StartAttack()
        {
            StopAllCoroutines();
            StartCoroutine(SequenceAttack());

        }

        public void UpdateBattleRoundInfo(BattleRoundInfo info, S2C_UpdateBattleAction battleAction)
        {
            Debug.Log(JsonConvert.SerializeObject(info));
            try
            {
                foreach (var item in battleAction.PlayerBattleInfos)
                {
                    if (item.Uid == GameFacade.Instance.playerBattleInfo.Uid)
                    {
                        translateInformation(item, GameFacade.Instance.playerBattleInfo);
                    }
                    if (item.Uid == GameFacade.Instance.enemyBattleInfo.Uid)
                    {
                        translateInformation(item, GameFacade.Instance.enemyBattleInfo);
                    }

                }
            }
            catch (System.Exception e)
            {
                Debug.Log("S2C_battleAction��Ϣ����,�޷�����ս����Ϣ: " + e);
            }

            //try
            //{
            try
            {
                GameFacade.Instance.playerBattleInfo.Dices = info.PlayerBattleInfos[0].Dices;
                GameFacade.Instance.enemyBattleInfo.Dices = info.PlayerBattleInfos[0].Dices;
            }
            catch (System.Exception e)
            {
                Debug.Log("�������������ʧ��: " + e);
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
                Debug.Log("���³�������Ч��ʧ��: " + e);
            }
                foreach (var item in info.BuCarriedSkill)
                {
                    buCarriedSkill.Add(item);
                }
            //}
/*            catch (System.Exception e)
            {
                Debug.Log("BattleRoundInfo��Ϣ�����޷����µ�ǰ�غ�ս����Ϣ: " + e);
            }*/


        }
        private static void translateInformation(PlayerBattleInfo from, PlayerBattleInfo target)
        {
            target.Uid = from.Uid;
            target.Hp = from.Hp;
            target.Dices = from.Dices;
        }
        /*        void sequenceAttack(Elf_Monobehavior[] playerElfs)
                {
                    NewPlayerController player = GameFacede.Instance.currentTurn;

                    int i = 0;
                    while (i < player.PlayerElfs.Count)
                    {

                        playerElfs[i].UseSkill();
                        while (!attacked)
                        {
                            Debug.Log(attacked);
                            Thread.Sleep(10);
                        }
                        attacked = false;
                        i++;


                    }
                }*/
        /// <summary>
        /// ����������Ϳ�ʼ��������Ϣ
        /// </summary>
        /// <returns></returns>
        IEnumerator SequenceAttack()//���ݾ���˳����й���������Э�̺��õ�һ������ʹ�ü��ܣ����ݾ�̬����attacked���ж���һ������Ĺ����Ƿ��Ѿ���ɣ�attacked������skilldeployer�����޸ģ���attacked��skilldeployer
                                    //���ú�ֵΪtrue����ʱ������0.5���������һ��������й���
        {
            
            //yield return new WaitForSeconds(0.5f);
           NewPlayerController  player = GameFacade.Instance.currentTurn.GetComponent<NewPlayerController>();
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
            StartCoroutine(waitForNextRound());
        }

        IEnumerator waitForNextRound()
        {
            GameFacade.Instance.uiManager.PushPanel(UIPanelType.SwitchSide);

            yield return new WaitForSeconds(0.8f);
            //TODO�������һ���������˳�ս���غϣ����ֽ��㻭��
            GameFacade.Instance.SwitchRound();
            countDown = RemainTime;
            StartCoroutine(CountingDown());
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
                StartCoroutine(CountingDown());
            }
        }

    }
}