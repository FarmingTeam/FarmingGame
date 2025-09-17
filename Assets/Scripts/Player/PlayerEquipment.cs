using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEquipment : MonoBehaviour
{
    public List<Equipment> equipList=new List<Equipment>();

   

    private void Start()
    {
        equipList.Add(ResourceManager.Instance.GetEquipment(1));
        equipList.Add(ResourceManager.Instance.GetEquipment(2));
        equipList.Add(ResourceManager.Instance.GetEquipment(3));
        equipList.Add(ResourceManager.Instance.GetEquipment(4));

        equipList.Add(ResourceManager.Instance.GetEquipment(1));

        equipList.Add(ResourceManager.Instance.GetEquipment(1));


    }


 
    public void ChangeEquipmentExtra(EquipmentType equipmentType,int equipmentExtra)
    {
        foreach(Equipment equip in equipList)
        {
            if(equip.equipmentType == equipmentType)
            {
                if(equipmentType==EquipmentType.SeedBasket)
                {
                    //현재 SeedID를 알려줌
                    equip.equipmentExtra = equipmentExtra;
                }
                else if(equipmentType == EquipmentType.WateringCan)
                {
                    //현재 물 게이지를 알려줌
                    equip.equipmentExtra += equipmentExtra;
                    equip.equipmentExtra=Mathf.Clamp(equip.equipmentExtra,0,equip.equipmentMaxRate);
                }
                
            }
        }
    }

    public int CheckEquipmentExtra(EquipmentType equipmentType)
    {
        foreach (Equipment equip in equipList)
        {
            if(equip.equipmentType==equipmentType)
            {
                return equip.equipmentExtra;
            }
            
        }
        return -1;
    }

    
}
