using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",menuName ="Inventory/Inventory Data")]
public class InventoryData_SO : ScriptableObject
{
    public List<InventoryItem> items= new List<InventoryItem>(30);
    public void AddItem(ItemData_SO newItemData, int amount)
    {
        bool found = false;
        if (newItemData.Stackable)//如果物品可以堆叠
        {
            foreach (InventoryItem item in items)
            {
                if (item.itemData == newItemData)//寻找背包中是否存在相同的物品
                {
                    found = true;
                    item.amount += amount;//找到后将新捡到的物品数量添加入背包
                    break;
                }
            }
        }
        if (!found)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (items[i].itemData == null)//如果没有找到，则从背包第一个开始找空缺的背包栏位,如果找到了就在这个格子添加新物品
                {
                    items[i].itemData = newItemData;
                    items[i].amount += amount;
                    break;
                }
            }
        }
    }
    public void ClearBag()
    {
        foreach (var item in items)
        {
            item.itemData = null;
            item.amount = 0;
        }
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemData_SO itemData;
    public int amount;
}

