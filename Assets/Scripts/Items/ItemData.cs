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
    Pickaxe,
    SeedBasket
}


public enum ItemType
{
    Others,
    Seed,
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
    

    public ItemData(int itemID, string itemName, string itemDescription, string itemPath, string itemType,int maxNum)
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
        else if(itemType=="Seed")
        {
            this.itemType = ItemType.Seed;
        }
        this.maxQuantity = maxNum;
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

    public Item(ItemData itemData, int currentQuantity)
    {
        this.itemData = itemData;
        this.currentQuantity = currentQuantity;
    }
}

[Serializable]
public class Equipment:ScriptableObject
{
    public int equipmentID;
    public string equipmentName;
    public string equipmentDescription;
    public EquipmentType equipmentType;
    public string equipmentPath;
    public Sprite equipmentIcon;
    public Equipment(int equipmentID, string equipmentName, string equipmentDescription, string equipmentType, string equipmentPath)
    {
        this.equipmentID = equipmentID;
        this.equipmentName = equipmentName;
        this.equipmentDescription = equipmentDescription;     
        this.equipmentPath = equipmentPath;

        if(equipmentType=="Hoe")
        {
            this.equipmentType = EquipmentType.Hoe;
        }
        else if(equipmentType=="WateringCan")
        {
            this.equipmentType = EquipmentType.WateringCan;
        }
        else if(equipmentType=="Axe")
        {
            this.equipmentType = EquipmentType.Axe;
        }
        else if( equipmentType=="Pickaxe")
        {
            this.equipmentType= EquipmentType.Pickaxe;
        }
        else if(equipmentType=="SeedBasket")
        {
            this.equipmentType = EquipmentType.SeedBasket;
        }
    }
}


