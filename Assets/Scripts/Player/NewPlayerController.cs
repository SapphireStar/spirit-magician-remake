using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Manager;

namespace ElfWizard
{
    public class NewPlayerController : MonoBehaviour, IDamagable, Spawnable,IBuffer
    {
        public Animator anim;
        //private float BlendSpeed = 0;
        public float health = 5;
        public bool isDead = false;
        public List<GameObject> PlayerElfs = new List<GameObject>();
        public List<GameObject> SpawnPoints = new List<GameObject>(3);
        private bool[] SPNotAvailable = new bool[3];//�����ж�ˢ�µ��Ƿ���ã����¾���ʱ�����жϾ���λ����Ƿ��п�λ�����п�λ�������о�������
        public List<IBuff> buffList = new List<IBuff>();
        int count = 0;
        [Header("Player Properties")]
        public float Defence;
        public int UID;

        void Start()
        {
/*            playerOriginalPosition = transform.position;
            currentTarget = GameObject.Find("Slime (1)");
            targetPoint = transform.position;*/
        }


        void Update()
        {
/*            if (Input.GetKeyDown(KeyCode.D))//ɱ���ض�����֮�󣬽������Ƴ����ص�����
            {
                GameObjectPool.Instance.CollectObject(PlayerElfs[0]);
                PlayerElfs.RemoveAt(0);
                GameObjectPool.Instance.CollectObject(PlayerElfs[0]);
                PlayerElfs.RemoveAt(0);//����ͬʱɱ���˵�һֻ�͵ڶ�ֻ����
                UpdateElfPos();//ɱ���������Ҫִ�иø��¾���λ�õķ���
            }*/


        }

        /*    public void moveToTarget()
            {

                if (!(Vector3.Magnitude(transform.position - targetPoint) < 0.01f) && targetPoint != null)
                {

                    transform.position = Vector3.MoveTowards(transform.position, targetPoint, PlayerSpeed * Time.deltaTime);
                    targetBlend = 1;
                }
                else if((Vector3.Magnitude(transform.position - targetPoint) < 0.01f)&&isAttacking)
                {

                    anim.SetTrigger("Attack");
                    targetBlend = 0;
                    isAttacking = false;//����Attack��Attack��isAttacking����ΪTRUE��Ȼ��������һ˲����ɣ������else if���ͨ���ж������ã�

                }
                else targetBlend = 0;
            }*/
        /*    public void setTargetPointToOrigin()//���������ã����س�ʼ��
            {
                targetPoint = playerOriginalPosition;

            }
            public void SetTarget(GameObject target)//���ù���Ŀ��
            {
                currentTarget.transform.position = target.transform.position;
            }*/
        /*    public void Attack()
            {
                if (!currentTarget.GetComponent<Enemy>().CheckisDead())
                {
                    targetPoint = currentTarget.GetComponent<Enemy>().GetBeAttackPlace().position;
                    isAttacking = true;
                }
            }*/

        public void GetHit(float health)
        {
            this.health -= health;
            if (this.health < 1)
            {
                isDead = true;
                anim.SetBool("isDead", true);

            }
            anim.SetTrigger("GetHit");
            GameFacade.Instance.HitUI(transform.position, health);
            GameFacade.Instance.battleUI.UpdateHealthBar(GameFacade.Instance.curRoundInfo);
        }

        public Vector3 GetBeAttackPlace()
        {
            return transform.position;
        }
        /*    private void setAnimBlend()
            {
                if (Mathf.Abs(currentBlend - targetBlend) < Constants.AccelerSpeed * Time.deltaTime)
                {
                    currentBlend = targetBlend;
                }
                else if (currentBlend > targetBlend)
                {
                    currentBlend -= Constants.AccelerSpeed * Time.deltaTime;
                }
                else currentBlend += Constants.AccelerSpeed * Time.deltaTime;
                anim.SetFloat("Speed", currentBlend);
            }*/
/*        public void AddElf(string elf, int level)
        {
            GameObject _elf;
            if (PlayerElfs.Count < 3)//����������δ��3��ʱ
            {
                foreach (var Elf in PlayerElfs)
                {

                    Elf.transform.localPosition = new Vector3(Elf.transform.localPosition.x + 3, Elf.transform.localPosition.y, Elf.transform.localPosition.z);//����Ҫʹ��local position����Ϊ���ɵ��˵ľ���ʱ�������Ǿ���ģ���Ҫ�þ����ƶ��ķ����෴
                    Elf.GetComponent<Elf_Monobehavior>().Init();
                }
                _elf = GameObjectPool.Instance.CreateObject(elf, ResourceManager.Load<GameObject>(elf), SpawnPoints[0].transform.position, SpawnPoints[0].transform.rotation);
                _elf.transform.SetParent(SpawnPoints[0].transform);
                //Instantiate(elf.ElfPrefab, SpawnPoints[0].transform.position, SpawnPoints[0].transform.rotation, SpawnPoints[0].transform);
                PlayerElfs.Insert(0, _elf);
                _elf.GetComponent<Elf_Monobehavior>().Level = level - 1;
                // _elf.GetComponent<Elf_Monobehavior>().Attributes = elf.elfAttributes;//���辫��Ĭ������
                count++;
            }
            else//������������3��ʱ
            {
                RemoveLastElf();
                foreach (var Elf in PlayerElfs)
                {
                    Elf.transform.localPosition = new Vector3(Elf.transform.localPosition.x + 3, Elf.transform.localPosition.y, Elf.transform.localPosition.z);
                    Elf.GetComponent<Elf_Monobehavior>().Init();
                }
                _elf = GameObjectPool.Instance.CreateObject(elf, ResourceManager.Load<GameObject>(elf), SpawnPoints[0].transform.position, SpawnPoints[0].transform.rotation);
                _elf.transform.SetParent(SpawnPoints[0].transform);
                PlayerElfs.Insert(0, _elf);

            }
            if (GameFacede.Instance.turn == BattleState.PLAYERTURN)
            {
                _elf.tag = "Player" + "Elf";

            }
            else if (GameFacede.Instance.turn == BattleState.ENEMYTURN)
            {
                _elf.tag = "Enemy" + "Elf";

            }

        }*/
        public void AddElf(string elf,int level,int uid)
        {
            GameObject _elf;
            if (PlayerElfs.Count < 3)//����������δ��3��ʱ
            {
                foreach (var Elf in PlayerElfs)
                {

                    Elf.transform.position = SpawnPoints[PlayerElfs.IndexOf(Elf) + 1].transform.position;
                    Elf.transform.SetParent(SpawnPoints[PlayerElfs.IndexOf(Elf) + 1].transform);
                }
                _elf = GameObjectPool.Instance.CreateObject(elf, ResourceManager.Load<GameObject>(elf), SpawnPoints[0].transform.position, SpawnPoints[0].transform.rotation);
                _elf.transform.SetParent(SpawnPoints[0].transform);
                    //Instantiate(elf.ElfPrefab, SpawnPoints[0].transform.position, SpawnPoints[0].transform.rotation, SpawnPoints[0].transform);
                PlayerElfs.Insert(0, _elf);
                _elf.GetComponent<Elf_Monobehavior>().Level = level - 1;
                // _elf.GetComponent<Elf_Monobehavior>().Attributes = elf.elfAttributes;//���辫��Ĭ������
                count++;
                _elf.GetComponent<Elf_Monobehavior>().ElfID = uid;
            }
            else//������������3��ʱ
            {
                RemoveLastElf();
                foreach (var Elf in PlayerElfs)
                {
                    Elf.transform.position = SpawnPoints[PlayerElfs.IndexOf(Elf) + 1].transform.position;
                    Elf.transform.SetParent(SpawnPoints[PlayerElfs.IndexOf(Elf) + 1].transform);
                }
                _elf = GameObjectPool.Instance.CreateObject(elf, ResourceManager.Load<GameObject>(elf), SpawnPoints[0].transform.position, SpawnPoints[0].transform.rotation);
                _elf.transform.SetParent(SpawnPoints[0].transform);
                PlayerElfs.Insert(0, _elf);

            }
            if (GameFacade.Instance.turn == BattleState.PLAYERTURN)
            {
                _elf.tag = "Player"+"Elf";

            }
            else if (GameFacade.Instance.turn == BattleState.ENEMYTURN)
            {
                _elf.tag = "Enemy"+"Elf";

            }

        }
        public void RemoveLastElf()//�����������������ޣ��Զ��������ٻ��ľ����Ƴ�
        {

            GameObjectPool.Instance.CollectObject(PlayerElfs[PlayerElfs.Count - 1]);
            PlayerElfs.RemoveAt(PlayerElfs.Count - 1);

        }

        public void UpdateElfPos()//���ڸ��µ��о�������ʱ�����������λ��
        {
            if (PlayerElfs.Count > 0)
            {
                foreach (var Elf in PlayerElfs)
                {
                    Elf.transform.position = SpawnPoints[PlayerElfs.IndexOf(Elf)].transform.position;
                    Elf.GetComponent<Elf_Monobehavior>().Init();
                }
            }
        }

        public void UpdateBuff()
        {
            for (int i = 0; i < buffList.Count; i++)
            {
                if (buffList[i] == null)
                    buffList.RemoveAt(i);
            }
            IBuff[] buff = gameObject.GetComponents<IBuff>();
            for (int i = 0; i < buff.Length; i++)
            {
                if (!buffList.Contains(buff[i]))
                    buffList.Add(buff[i]);

            }
            foreach (var item in buffList)
            {
                item.ApplyBuff();
            }
        }

        public void RemoveElf(GameObject elfToRemove)
        {
            int index = PlayerElfs.IndexOf(elfToRemove);
            GameObjectPool.Instance.CollectObject(elfToRemove);
            PlayerElfs.RemoveAt(index);
        }

    }
}