using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIInventory : UIBase
{
    PlayerInventory playerInventory;

    public List<UISlot> uISlots = new List<UISlot>();

    public GameObject uiSlotPrefab;

    [SerializeField] GameObject DescriptionPanelPrefab;
    public GameObject DescriptionPanel;
    

    // Start is called before the first frame update
    void Start()
    {

        playerInventory = MapControl.Instance.player.inventory; //맵컨트롤 참조

        DescriptionPanel= Instantiate(DescriptionPanelPrefab, transform,false);
        DescriptionPanel.SetActive(false);
        for(int i = 0; i<playerInventory.InventoryMaxNum; i++)
        {
            Instantiate(uiSlotPrefab,this.transform,false);
        }

        uISlots = GetComponentsInChildren<UISlot>().ToList();
    }

    protected override void OnOpen()
    {
        if(playerInventory == null)
        {
            playerInventory = MapControl.Instance.player.inventory; 
        }
        playerInventory.SubscribeOnItemChange(RefreshAllSlots);
        RefreshAllSlots();
    }

    protected override void OnClose()
    {
        playerInventory.UnsubscribeOnItemChange(RefreshAllSlots);
    }


    public void SetItemsUI(Item runtimeItemData,int slotIndex)
    {
        var slot=uISlots[slotIndex];
        slot.SetSlot(runtimeItemData);
            
    }
    //UISlot FindEmptySlot()
    //{
    //    foreach (var slot in uISlots)
    //    {
    //        if (slot.SlotItemData == null)
    //        {
    //            return slot;
    //        }
    //    }
    //    return null;
    //}

    public void RefreshAllSlots()
    {
        foreach(var slot in uISlots)
        {
            slot.RefreshSlot();
        }
    }

    
    
}
