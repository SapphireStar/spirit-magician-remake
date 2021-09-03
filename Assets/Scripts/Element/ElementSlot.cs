using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ElfWizard.Manager;

namespace ElfWizard
{
    public class ElementSlot : MonoBehaviour, IPointerClickHandler
    {
        public Image icon = null;
        public Text ShowAmount = null;
        public int amount;
        public ElementType Type;

        public void OnPointerClick(PointerEventData eventData)
        {

            GameFacade.Instance.AskIfSpawn(Type,amount);
        }


        public void setup(Sprite image, int amount)
        {
            if (image != null)
            {
                icon.sprite = image;
                this.ShowAmount.text = amount.ToString();
                icon.gameObject.SetActive(true);
            }
            else icon.gameObject.SetActive(false);
        }
        public void Init()//½«Slot³õÊ¼»¯
        {
            Type = (ElementType)999;
            amount = 0;
            gameObject.SetActive(false);

        }
    }
}