using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Manager;
using PbSpirit;

namespace ElfWizard
{
    public class ElementSlotHolder : MonoBehaviour
    {
        public List<Sprite> elementIcon = new List<Sprite>();
        public List<ElementSlot> ElementSlots = new List<ElementSlot>();//����Ԫ��UI��ʾ�Ļ�����λ
        bool[] SlotSet = new bool[5];
        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                ElementSlots.Add(transform.GetChild(i).gameObject.GetComponent<ElementSlot>());
            }

        }
        public void SetUp(SpecialistType type)
        {
            switch (type)
            {
                case SpecialistType.StNone:
                    break;
                case SpecialistType.StFire:
                    setElement(elementIcon[0], ElementType.ST_Fire);
                    break;
                case SpecialistType.StIce:
                    setElement(elementIcon[1], ElementType.ST_Ice);
                    break;
                case SpecialistType.StHoly:
                    setElement(elementIcon[2], ElementType.ST_Holy);
                    break;
                case SpecialistType.StEvil:
                    setElement(elementIcon[3], ElementType.ST_Evil);
                    break;
                case SpecialistType.StNatural:
                    setElement(elementIcon[4], ElementType.ST_Natural);
                    break;
                case SpecialistType.StMystery:
                    break;
                case SpecialistType.StMagician:
                    break;

            }
        }
        public void SetUp(ElementType type)
        {

            switch (type)
            {

                case ElementType.ST_Fire:
                    setElement(elementIcon[0], ElementType.ST_Fire);
                    break;
                case ElementType.ST_Ice:
                    setElement(elementIcon[1], ElementType.ST_Ice);
                    break;
                case ElementType.ST_Holy:
                    setElement(elementIcon[2], ElementType.ST_Holy);
                    break;
                case ElementType.ST_Evil:
                    setElement(elementIcon[3], ElementType.ST_Evil);
                    break;
                case ElementType.ST_Natural:
                    setElement(elementIcon[4], ElementType.ST_Natural);
                    break;
            }


        }
        private void setElement(Sprite icon, ElementType type)
        {

            for (int i = 0; i < 5; i++)
            {
                if (!SlotSet[i])//���������Ԫ��û�б�roll���������ø�Ԫ����ʾ���ҳ�ʼֵΪ1
                {

                    ElementSlots[i].Type = type;
                    ElementSlots[i].setup(icon, 1);
                    ElementSlots[i].amount++;
                    SlotSet[i] = true;

                    return;

                }
                else if (ElementSlots[i].Type == type && SlotSet[i])//��������Ԫ���Ѿ���roll������������Ԫ������+1
                {
                    int amount = int.Parse(ElementSlots[i].ShowAmount.text);
                    //Debug.Log("�����±�");
                    ElementSlots[i].setup(icon, amount + 1);
                    ElementSlots[i].amount++;
                    return;
                }



            }
        }
        public void ClearSlotSet()
        {
            for (int i = 0; i < SlotSet.Length; i++)
            {
                ElementSlots[i].Init();
                //ElementSlots[i].Type =(ElementType)999;//��Ҫ��ElementSlots������slot��type�������Ȼ�ڽ��жѵ�����ʱ�����������
                SlotSet[i] = false;
            }
        }
        public class Element
        {
            public Sprite icon;

        }
    }
}