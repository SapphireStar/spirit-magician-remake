using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class Gun : MonoBehaviour
    {
        public GameObject bulletPrefab;
        // Start is called before the first frame update
        void Start()
        {

        }
        public void Fire()
        {
            GameObjectPool.Instance.CreateObject("bullet", bulletPrefab, transform.position, transform.rotation);
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnGUI()
        {
            if (GUILayout.Button("开火"))
            {
                Fire();

            }
            if (GUILayout.Button("清空子弹"))
            {
                GameObjectPool.Instance.Clear("bullet");

            }
            if (GUILayout.Button("清空全部"))
            {
                GameObjectPool.Instance.ClearAll();

            }
        }
    }
}