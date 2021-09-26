using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class ElementController : MonoBehaviour,IController
    {
        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return ElfWizardArch.Instance;
        }
        List<Transform> ElementPlaces = new List<Transform>();
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                ElementPlaces[i] = transform.GetChild(i);
            }
        }
        public void SetupElement()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}