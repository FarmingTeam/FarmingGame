using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

using UnityEngine;
using System;




public class UIToolBar : UIBase
{
    [SerializeField] GameObject uiQuickSlotPrefab;
    List<UIQuickSlot> quickSlotList=new List<UIQuickSlot>();
    [SerializeField] List<Equipment> equipList=new List<Equipment>();


    private void Start()
    {
       
        equipList=MapControl.Instance.player.equipment.equipList;
        

        for (int i=0; i<6;  i++)
        {
            GameObject go= Instantiate(uiQuickSlotPrefab,this.transform,false);
                       
        }

        quickSlotList = GetComponentsInChildren<UIQuickSlot>().ToList();

        //여기에서 슬롯에 아이템 넣어주기
        for(int i = 0; i < quickSlotList.Count; i++)
        {
            quickSlotList[i].slotNumber = i + 1;
            quickSlotList[i].Init();
            quickSlotList[i].SetQuickSlot(equipList[i]);
            
            

        }
    }
    void SetToolBar()
    {
        equipList = MapControl.Instance.player.equipment.equipList;
        for (int i = 0; i < quickSlotList.Count; i++)
        {
            quickSlotList[i].SetQuickSlot(equipList[i]);

        }
    }





    private void OnEnable()
    {
        MapControl.Instance.player.tool.SubscribeToSelectionChange(SelectSlot);
        MapControl.Instance.player.equipment.SubscribeEquipmentChange(SetToolBar);
    }
    private void OnDisable()
    {
        MapControl.Instance.player.tool.UnsubscribeToSelectionChange(SelectSlot);
        MapControl.Instance.player.equipment.UnsubscribeEquipmentChange(SetToolBar);
    }




    public void SelectSlot()
    {

        
        foreach(var slot in quickSlotList)
        {
            slot.outline.enabled = false;
            if(slot.slotEquipment==MapControl.Instance.player.tool.CurrentEquip)
            {
                if(slot.slotEquipment.equipmentType==EquipmentType.SeedBasket)
                {
                    UIManager.Instance.OpenUI<UISeedBasket>();
                }
                else
                {
                    UIManager.Instance.CloseUI<UISeedBasket>();
                }
                slot.outline.enabled = true;
            }
            
        }
        
    }
    




    
    

    
}
