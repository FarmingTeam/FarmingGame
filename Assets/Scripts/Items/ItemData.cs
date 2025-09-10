using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;



public enum EquipmentType
{
    None,
    Hoe,
    WateringCan,
    Axe,
    Pickaxe
}


public enum ItemType
{
    Others,
    Potion

}

[Serializable]
public class ItemData:ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public string itemPath;
    public ItemType itemType;
    public Sprite itemIcon;
    public int maxQuantity;
    public bool isStackable;
    

    public ItemData(int itemID, string itemName, string itemDescription, string itemPath, string itemType)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemPath = itemPath;
        if(itemType=="Others")
        {
            this.itemType=ItemType.Others;
        }
        else if(itemType=="Potions")
        {
            this.itemType = ItemType.Potion;
        }
        this.maxQuantity = 5;
        isStackable=maxQuantity>1? true: false;
    }
}

[Serializable]
public class Item
{
    public ItemData itemData;
    public int currentQuantity;

    public Item( ItemData itemData)
    {
        this.itemData = itemData;
        this.currentQuantity = 1;
        
    }
}
