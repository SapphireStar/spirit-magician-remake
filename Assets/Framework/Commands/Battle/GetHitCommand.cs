using System.Collections;
using System.Collections.Generic;
using ElfWizard;
using UnityEngine;

namespace Framework
{
    public class GetHitCommand : AbstractCommand,ICommand
    {
        public Vector3 position;
        public float health;

        protected override void OnExecute()
        {

                Vector3 offset = new Vector3(300, 0, 0);
                Vector3 _position = Camera.main.WorldToScreenPoint(position);
                GameObject hitUI = GameObjectPool.Instance.CreateObject("HitUI", ResourceManager.LoadObsolete<GameObject>("HitUI"), _position + offset, Quaternion.identity);
                hitUI.GetComponent<HitAnim>().SetDamage(health);
            
        }

    }
}