using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ElfWizard
{
    public class HitAnim : MonoBehaviour,IResetable
    {
        Text text;
        RectTransform rectTran;
        public void OnReset()
        {
            transform.SetParent(GameObject.Find("Canvas").transform);
            text = GetComponent<Text>();
            rectTran = GetComponent<RectTransform>();
            text.CrossFadeAlpha(1, 0, false);
            text.CrossFadeAlpha(0, 1, false);
            Collect();
        }
        public void SetDamage(float damage)
        {
            text.text = damage.ToString();
        }

        void Update()
        {
           rectTran.anchoredPosition += new Vector2(0, Time.deltaTime*100);
        }
        void Collect()
        {
            GameObjectPool.Instance.CollectObject(gameObject, 2);
        }
    }
}