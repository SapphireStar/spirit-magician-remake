using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Manager;
using Framework;

namespace ElfWizard
{

    public class NewPlayerController : MonoBehaviour, IDamagable,IBuffer,IController
    {
        public Animator anim;
        //private float BlendSpeed = 0;
        public float health = 5;
        public bool isDead = false;
        public List<GameObject> PlayerElfs = new List<GameObject>(3);
        public List<GameObject> SpawnPoints = new List<GameObject>(3);
        public List<IBuff> buffList = new List<IBuff>();
        int count = 0;
        [Header("Player Properties")]
        public float Defence;
        public int UID;


        public void GetHit(float health)
        {
            this.health -= health;
            if (this.health < 1)
            {
                isDead = true;
                anim.SetBool("isDead", true);

            }
            anim.SetTrigger("GetHit");

            this.SendCommand<GetHitCommand>(new GetHitCommand() { position = transform.position,health = health});
        }

        public Vector3 GetBeAttackPlace()
        {
            return transform.position;
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

        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
    }
}