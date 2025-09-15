using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UISeedBasket : UIBase
{

    PlayerInventory inventory;

    List<UISeedSlot> slots=new List<UISeedSlot>();
    Dictionary<ItemData, int> seedInventoryDic;


    [SerializeField] GameObject seedSlotPrefab;

    int SeedInventorySlotNum { get; } = 16;
 

    //여기 SelectedSlot.SlotSeedItem 으로 접근하면 현재 선택된 씨앗을 알 수 있음( 단 SelectedSlot이 null일수도 있음을 주의
    public UISeedSlot SelectedSlot { get; private set; }

    protected override void OnOpen()
    {

        //아래 슬롯들 소환
        if(slots.Count == 0)
        {
            for(int i = 0;i<SeedInventorySlotNum;i++)
            {
                Instantiate(seedSlotPrefab,transform,false);
            }
            slots = GetComponentsInChildren<UISeedSlot>(true).ToList();
        }

        inventory = MapControl.Instance.player.inventory;

        //인벤토리에서 씨앗인거만 가져옴
        
        inventory.SubscribeOnItemChange(SetSeedSlotUI);


        
    }
    //플레이어 인벤토리쪽을 이어두고

    protected override void OnClose()
    {
        inventory.UnsubscribeOnItemChange(SetSeedSlotUI);
    }


    public void SetSeedSlotUI()
    {

        seedInventoryDic = new Dictionary<ItemData, int>();
        foreach (var slot in inventory.slotDataList)
        {

            if(slot.slotItem==null)
            {
                continue;
            }
            if (slot.slotItem.itemData.itemType == ItemType.Seed)
            {
                

                //만약 씨앗이면 딕셔너리에 그 아이템 아이디가 있으면 quantity value만 더해서 다시넣어주기
                if (seedInventoryDic.TryGetValue(slot.slotItem.itemData, out int quantity))
                {
                    seedInventoryDic[slot.slotItem.itemData] = quantity + slot.slotItem.currentQuantity;
                }
                else
                {
                    //만약 딕셔너리에 없었으면 새로 등록
                    seedInventoryDic.Add(slot.slotItem.itemData, slot.slotItem.currentQuantity);
                }



            }
        }

        var list=seedInventoryDic.OrderBy(kv => kv.Key.itemID).ToList();
        
        for(int i=0;i<list.Count;i++)
        {
            slots[i].SetSeedSlot(list[i].Key, list[i].Value);
        }
        for(int i=list.Count; i<slots.Count;i++)
        {
            slots[i].EmptyOutSlot();
        }
        

        //만약 인벤토리 최대갯수를 초과하면

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

    public void SelectSlot(UISeedSlot uISeedSlot)
    {
        SelectedSlot = uISeedSlot;

        foreach (var slot in slots)
        {
            slot.outline.enabled = false;
        }
        if(SelectedSlot!=null)
        {
            SelectedSlot.outline.enabled = true;
        }
        
        
    }
}
