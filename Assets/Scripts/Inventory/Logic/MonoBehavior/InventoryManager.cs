using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [Header("Inventory Data")]
    public InventoryData_SO InventoryData;

    [Header("Containers")]
    public ContainerUI inventoryUI;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

    }
    void Start()
    {
        inventoryUI.RefreshUI();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            InventoryData.ClearBag();
            inventoryUI.RefreshUI();
        }
    }

}
