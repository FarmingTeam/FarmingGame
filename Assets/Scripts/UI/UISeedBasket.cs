using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UISeedBasket : UIBase
{

    PlayerInventory inventory;
    public List<Item> seedList;
    List<UISeedSlot> slots=new List<UISeedSlot>();

    [SerializeField] GameObject seedSlotPrefab;

    int seedInventorySlotNum = 16;
 


    protected override void OnOpen()
    {

        //아래 슬롯들 소환
        if(slots.Count == 0)
        {
            for(int i = 0;i<seedInventorySlotNum;i++)
            {
                Instantiate(seedSlotPrefab,transform,false);
            }
            slots = GetComponentsInChildren<UISeedSlot>(true).ToList();
        }

        inventory = MapControl.Instance.player.inventory;
        seedList= inventory.seedList;
        inventory.SubscribeOnQuantityChange(RefreshAllSeedSLots);

        RefreshAllSeedSLots();
        
    }
    //플레이어 인벤토리쪽을 이어두고

    protected override void OnClose()
    {
        inventory.UnsubscribeOnQuantityChange(RefreshAllSeedSLots);
    }
    private void Start()
    {
        inventory.uiSeedBasket = this;
    }


    public void SetSeedSlotUI(Item seed)
    {
        var slot = FindEmptySeedSlot();
        slot.SetSeedSlot(seed);

    }

    UISeedSlot FindEmptySeedSlot()
    {
        foreach (var slot in slots)
        {
            if(slot.SlotSeedItem == null)
            {
                return slot;
            }
        }
        Debug.Log("칸이 없습니다");
        return null;
    }



    public void RefreshAllSeedSLots()
    {
        
        foreach (var slot in slots)
        {
            slot.RefreshSeedSlot();
        }
    }
}
