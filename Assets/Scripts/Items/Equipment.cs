using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmentType
{
    None,
    Hoe,
    WateringCan,
    Axe,
    Pickaxe,
    SeedBasket
}


[Serializable]
public class Equipment : ScriptableObject
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

        if (equipmentType == "Hoe")
        {
            this.equipmentType = EquipmentType.Hoe;
        }
        else if (equipmentType == "WateringCan")
        {
            this.equipmentType = EquipmentType.WateringCan;
        }
        else if (equipmentType == "Axe")
        {
            this.equipmentType = EquipmentType.Axe;
        }
        else if (equipmentType == "Pickaxe")
        {
            this.equipmentType = EquipmentType.Pickaxe;
        }
        else if (equipmentType == "SeedBasket")
        {
            this.equipmentType = EquipmentType.SeedBasket;
        }
    }
}
