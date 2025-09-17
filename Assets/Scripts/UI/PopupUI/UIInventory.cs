using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIPopup
{
    PlayerInventory playerInventory;

    public List<UISlot> uISlots = new List<UISlot>();

    public GameObject uiSlotPrefab;
    [SerializeField] Button sortButton;
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
            var go= Instantiate(uiSlotPrefab,this.transform,false);
            var slot = go.GetComponent<UISlot>();
            uISlots.Add(slot);
            uISlots[i].slotIndex = i;
        }

        
        
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        if(playerInventory == null)
        {
           playerInventory = MapControl.Instance.player.inventory; 
        }
        playerInventory.SubscribeOnItemChange(StartSettingItem);

        sortButton.onClick.AddListener(playerInventory.SortInventory);
        
        
    }

    protected override void OnClose()
    {
        MapControl.Instance.player.inventory.UnsubscribeOnItemChange(StartSettingItem);
        sortButton.onClick.RemoveListener(MapControl.Instance.player.inventory.SortInventory);
    }

    public void StartSettingItem()
    {
        for (int i = 0; i < uISlots.Count; i++)
        {

            SetItemsUI(playerInventory.slotDataList[i], i);
        }
    }



    public void SetItemsUI(SlotData slotData,int slotIndex)
    {
        var slot=uISlots[slotIndex];
        slot.SetSlot(slotData);
            
    }

    



}
