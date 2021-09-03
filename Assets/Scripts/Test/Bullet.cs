using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class Bullet : MonoBehaviour, IResetable
    {
        public Vector3 Target;
        public float speed;

        public void OnReset()
        {
            Target = transform.TransformPoint(new Vector3(0, 0, 50));
        }

        // Start is called before the first frame update
        void Start()
        {
            speed = 50;

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, Time.deltaTime * speed);
            if (Mathf.Abs(transform.position.magnitude - Target.magnitude) < 0.1)
            {
                GameObjectPool.Instance.CollectObject(gameObject);
            }
        }
    }
}