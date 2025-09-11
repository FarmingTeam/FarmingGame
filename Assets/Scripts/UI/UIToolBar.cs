using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIToolBar : UIBase
{
    [SerializeField] GameObject uiQuickSlotPrefab;
    List<UIQuickSlot> quickSlotList=new List<UIQuickSlot>();
    List<Equipment> equipmentList=new List<Equipment>();
    private void Start()
    {
        equipmentList.Add(ResourceManager.Instance.GetEquipment(1));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        //여기서 슬롯칸을 동적생성할예정(아직 안함)
        for (int i=0; i<6;  i++)
        {
            GameObject go= Instantiate(uiQuickSlotPrefab,this.transform,false);
            go.GetComponentInChildren<TextMeshProUGUI>().SetText((i+1).ToString());
            
        }

        quickSlotList = GetComponentsInChildren<UIQuickSlot>().ToList();

        //여기에서 슬롯에 아이템 넣어주기
        for(int i = 0; i < quickSlotList.Count; i++)
        {
            quickSlotList[i].Init();
            quickSlotList[i].slotEquipment = equipmentList[i];
            quickSlotList[i].dragDropEquipment.SetDragDrop(quickSlotList[i]);

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            MapControl.Instance.player.tool.CurrentEquip = quickSlotList[0].slotEquipment;
            Debug.Log(MapControl.Instance.player.tool.CurrentEquip.equipmentName);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            //MapControl.Instance.player.tool.CurrentEquip = quickSlotList[1].slotEquipment;
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //MapControl.Instance.player.tool.CurrentEquip = quickSlotList[2].slotEquipment;
        }
        else if( Input.GetKeyDown(KeyCode.Alpha4))
        {
            //MapControl.Instance.player.tool.CurrentEquip = quickSlotList[3].slotEquipment;
        }
        else if (!Input.GetKeyDown(KeyCode.Alpha5))
        {
            //MapControl.Instance.player.tool.CurrentEquip = quickSlotList[4].slotEquipment;
        }
        else if( !Input.GetKeyDown(KeyCode.Alpha6))
        {
            //MapControl.Instance.player.tool.CurrentEquip = quickSlotList[5].slotEquipment;
        }
        


    }


    public void SelectSlot(UIQuickSlot uIQuickSlot)
    {
        foreach(var slot in quickSlotList)
        {
            slot.outline.enabled = false;
            if(slot==uIQuickSlot)
            {
                slot.outline.enabled=true;
                MapControl.Instance.player.tool.CurrentEquip=slot.slotEquipment;
            }
        }
        
    }
    




    
    

    
}
