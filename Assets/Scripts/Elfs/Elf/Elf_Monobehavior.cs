using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using ElfWizard.Manager;

namespace ElfWizard
{
    
    public class Elf_Monobehavior : MonoBehaviour, IDamagable,IBuffer
    {

        private Vector3 currentTarget;
        [Header("Elf Properties")]
        public float MoveSpeed = 5;
        public float health = 5;
        public float Damage = 1;
        public float FireDamage;
        public float IceDamage;
        public float DarkDamage;
        public float HolyDamage;
        public float NatureDamage;
        public float Defence;
        public int Level;
        public int AttackState;
        public Animator anim;
        public bool isAttacking;
        private Vector3 originPos;
        public bool isDead;
        [Header("ElfID")]
        public int ElfID;//TODOʹ��playercontroller�������ɵľ����ID

        public int[] skills;
        List<IBuff> buffList = new List<IBuff>();

        //[Header("Elf Attributes")]
        //public List<ElfAttributes> Attributes = new List<ElfAttributes>();

        // Start is called before the first frame update
        void Start()
        {


            AttackState = 0;
            originPos = transform.position;
            currentTarget = transform.position;
        }
        
        // Update is called once per frame
        void Update()
        {
            if (!isDead)
            {
               // gotoTarget();
            }

        }
/*        private void gotoTarget()
        {


            if (!(Vector3.Magnitude(transform.position - currentTarget) < 0.01) && currentTarget != null)
            {
                anim.SetFloat("AnimState", 3);
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, MoveSpeed * Time.deltaTime);

            }
            else if ((Vector3.Magnitude(transform.position - currentTarget) < 0.01) && isAttacking)
            {
                anim.SetTrigger("Attack");
                isAttacking = false;//��ʱisAttacking��Ϊfalse�����������ƶ�����Ҫ���������


            }

            else if ((Vector3.Magnitude(transform.position - currentTarget) < 0.01))
            {
                anim.SetFloat("AnimState", 1);
            }

        }*/
        public void BacktoOrigin()
        {
            currentTarget = originPos;
            isAttacking = false;//����Attack��Attack��isAttacking����ΪTRUE��Ȼ��������һ˲����ɣ������else if���ͨ���ж������ã�
        }
        public void SwitchTarget(Vector3 target)
        {
            if (!isDead)
            {
                anim.SetFloat("AnimState", 3);
                currentTarget = target;
            }
        }

        public void GetHit(float health)
        {
            if (health > 0)
            {
                this.health -= health;
                if (this.health <= 0)
                {
                    isDead = true;
                    this.health = 0;
                    anim.SetBool("isDead", true);
                    //Destroy(gameObject, 3);
                    StartCoroutine(removeThis());
                }
                anim.SetTrigger("GetHit");
            }
        }
        IEnumerator removeThis()
        {
            yield return new WaitForSeconds(2);
            transform.parent.parent.GetComponent<NewPlayerController>().RemoveElf(gameObject);
        }

        public Vector3 GetBeAttackPlace()
        {

            Vector3 pos = transform.position;
            if (GameFacade.Instance.turn == BattleState.PLAYERTURN)
            {
                return pos + new Vector3(0, 0, -1);
            }
            else return pos + new Vector3(0, 0, 1);
        }
        public void Init()
        {
            currentTarget = transform.position;
            originPos = transform.position;
        }

        public void UseSkill()
        {
            //׼������
            //���ɼ���
/*            attacked = false;
            if (GetComponentsInChildren<SkillDeployer>().Length > 0)//������ļ����ǳ�ʱ���ڳ��ģ��򵱸þ����ٴ�ʹ�ü���ʱ������Ҫ�ٴ�����һ������Ԥ����
            {
                attacked = true;
                return;
            }*/
            CharacterSkillSystem skillsystem = GetComponent<CharacterSkillSystem>();
                skillsystem.UseSkill(skills[0]);
            


        }
        IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
        }

        public void UpdateBuff()
        {
           IBuff[] buff= gameObject.GetComponents<IBuff>();
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
    }
}