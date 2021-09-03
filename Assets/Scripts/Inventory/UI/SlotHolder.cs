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
                itemUI.bag = InventoryManager.Instance.InventoryData;//InventoryData�洢�����������Ʒ���ݣ����Ե�SlotTypeΪBAGʱ����InventoryData��ֵ��itemUI.bag
                break;
            case SlotType.WEAPON:
                break;
            case SlotType.ARMOR:
                break;
            case SlotType.ACTION:
                break;
        }

        InventoryItem item = itemUI.bag.items[itemUI.index];//�Ӹ���Ʒ�����ı����л�ȡ�洢��Ʒ��Ϣ����InventoryItem
        itemUI.Setup(item.itemData, item.amount);//������Ʒ�ڱ����е�״̬��Ϣ���ֵ�UI��

    }
}
