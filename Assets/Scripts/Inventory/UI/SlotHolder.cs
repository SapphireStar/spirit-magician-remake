using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType { BAG, WEAPON, ARMOR, ACTION }
public class SlotHolder : MonoBehaviour
{
    public SlotType slotType;
    public ItemUI itemUI;

    public void UpdateItem()
    {
        switch(slotType)
        {
            case SlotType.BAG:
                itemUI.bag = InventoryManager.Instance.InventoryData;//InventoryData存储背包对象的物品数据，所以当SlotType为BAG时，将InventoryData赋值给itemUI.bag
                break;
            case SlotType.WEAPON:
                break;
            case SlotType.ARMOR:
                break;
            case SlotType.ACTION:
                break;
        }

        InventoryItem item = itemUI.bag.items[itemUI.index];//从该物品所属的背包中获取存储物品信息的类InventoryItem
        itemUI.Setup(item.itemData, item.amount);//将该物品在背包中的状态信息体现到UI中

    }
}
