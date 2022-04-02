using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PbSpirit;
using PbBattle;
using System;
using ElfWizard;

namespace Framework
{
    public class ElementController : MonoBehaviour,IController
    {
        public IArchitecture getArchitecture()
        {
            return ElfWizardArch.Instance;
        }

        List<Transform> ElementPlaces = new List<Transform>();
        List<GameObject> elements = new List<GameObject>();
        void Start()
        {
            this.RegisterEvent<StartAttackEvent>(ClearElement);//当开始攻击时，将场上元素清除
            for (int i = 0; i < transform.childCount; i++)
            {
                ElementPlaces.Add( transform.GetChild(i));
            }
        }

        public void SetupElement(Google.Protobuf.Collections.RepeatedField<DiceInfo> diceInfos)
        {
            for (int i = 0; i < diceInfos.Count; i++)
            {
                string name = Enum.GetName(typeof(SpecialistType), diceInfos[i].DiceValue);
                Debug.Log(name);
                elements.Add(GameObjectPool.Instance.CreateObject(name, getArchitecture().GetUtility<IResourceUtility>().Load<GameObject>(name), ElementPlaces[i].position, Quaternion.identity));
            }
        }
        public void ClearElement(StartAttackEvent e)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                GameObjectPool.Instance.CollectObject(elements[i]);
            }
        }
        void OnDestroy()
        {
            this.UnregisterEvent<StartAttackEvent>(ClearElement);
        }

    }
}