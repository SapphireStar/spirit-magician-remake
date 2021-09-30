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
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                ElementPlaces.Add( transform.GetChild(i));
            }
/*            DiceFormation formation = new DiceFormation();
            formation.DamageSpecialists.Add(new SpecialistType[] { SpecialistType.StFire, SpecialistType.StFire, SpecialistType.StFire,SpecialistType.StIce,SpecialistType.StIce });
            SetupElement(formation);*/
        }
        public void SetupElement(DiceFormation formation)
        {
            for (int i = 0; i < formation.DamageSpecialists.Count; i++)
            {
                string name = Enum.GetName(typeof(SpecialistType), formation.DamageSpecialists[i]);
                GameObjectPool.Instance.CreateObject(name, getArchitecture().GetUtility<IResourceUtility>().Load<GameObject>(name), ElementPlaces[i].position, Quaternion.identity);
            }
        }

    }
}