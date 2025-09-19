using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEquipment : MonoBehaviour
{
    public List<Equipment> equipList=new List<Equipment>();


    private Action onEquipmentChange;
   

    private void Start()
    {
        equipList.Add(ResourceManager.Instance.GetEquipment(1));
        equipList.Add(ResourceManager.Instance.GetEquipment(2));
        equipList.Add(ResourceManager.Instance.GetEquipment(3));
        equipList.Add(ResourceManager.Instance.GetEquipment(4));

        equipList.Add(ResourceManager.Instance.GetEquipment(5));

        equipList.Add(ResourceManager.Instance.GetEquipment(6));


    }

    public void SwitchEquipmentPlaces(int firstIndex, int secondIndex)
    {
        int CurrentSelectedIndex = -1;
        //그냥 정말 쉽게 두번째꺼랑 첫번쨰 자리를 바꾸면 된다.
        for(int i=0; i<equipList.Count; i++)
        {
            if (equipList[i]==MapControl.Instance.player.tool.CurrentEquip)
            {
                CurrentSelectedIndex = i; //현재 골라진 슬롯 번호 저장
                break;
            }
        }
        var temp=equipList[firstIndex];
        equipList[firstIndex]=equipList[secondIndex];
        equipList[secondIndex]=temp;
        onEquipmentChange?.Invoke();
        if(firstIndex==CurrentSelectedIndex)
        {
            MapControl.Instance.player.tool.SelectQuickslot(firstIndex+1);
        }
        else if(secondIndex==CurrentSelectedIndex)
        {
            MapControl.Instance.player.tool.SelectQuickslot(secondIndex+1);
        }
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

    public void SubscribeEquipmentChange(Action action)
    {
        onEquipmentChange += action;
    }

    public void UnsubscribeEquipmentChange(Action action)
    {
        onEquipmentChange-= action; 
    }
}
