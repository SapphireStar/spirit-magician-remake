using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData_SO itemData;
    // Start is called before the first frame update
/*    void Start()//因为是测试，所以将代码放在了start中，实际制作时，应该设计为接触或交互时再运行代码
    {
        //TODO:添加物品到背包
        InventoryManager.Instance.InventoryData.AddItem(itemData, itemData.itemAmount);
        InventoryManager.Instance.inventoryUI.RefreshUI();
        Destroy(gameObject);
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            InventoryManager.Instance.InventoryData.AddItem(itemData, itemData.itemAmount);
            InventoryManager.Instance.inventoryUI.RefreshUI();
            //Destroy(gameObject);
        }
    }


}
