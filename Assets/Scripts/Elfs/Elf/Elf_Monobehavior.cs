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
        public int ElfID;//TODO使用playercontroller更改生成的精灵的ID

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
                isAttacking = false;//这时isAttacking变为false，精灵会继续移动，需要解决该问题


            }

            else if ((Vector3.Magnitude(transform.position - currentTarget) < 0.01))
            {
                anim.SetFloat("AnimState", 1);
            }

        }*/
        public void BacktoOrigin()
        {
            currentTarget = originPos;
            isAttacking = false;//触发Attack后，Attack将isAttacking设置为TRUE，然后由于在一瞬间完成，这里的else if语句通过判定被调用，
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
            //准备技能
            //生成技能
/*            attacked = false;
            if (GetComponentsInChildren<SkillDeployer>().Length > 0)//若精灵的技能是长时间在场的，则当该精灵再次使用技能时，不需要再次生成一个技能预制体
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