using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemUI : MonoBehaviour
{
    public Image Icon = null;
    public Text amount = null;

    public InventoryData_SO bag { get; set; }
    public int index { get; set; }
    public void Setup(ItemData_SO item, int itemAmount)
    {
        if (item != null)
        {
            Icon.sprite = item.itemIcon;
            amount.text = itemAmount.ToString();
            Icon.gameObject.SetActive(true);
        }
        else Icon.gameObject.SetActive(false);
    }
}
