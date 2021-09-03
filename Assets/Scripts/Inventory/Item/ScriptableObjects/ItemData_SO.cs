using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Useable,Weapon,Armor}
[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item Data")]
public class ItemData_SO : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount;

    [TextArea]
    public string decription="";

    public bool Stackable;

    [Header("Weapon")]
    public GameObject WeaponPrefab;
}
