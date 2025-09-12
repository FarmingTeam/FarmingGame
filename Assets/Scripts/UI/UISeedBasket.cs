using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UISeedBasket : UIBase
{
    PlayerInventory inventory;
    public List<Item> playerInventoryList;
    List<UISeedSlot> slots=new List<UISeedSlot>();

 

    //씨앗 바구니를 열떄 정보들을 동기화함
    protected override void OnOpen()
    {
        if(slots.Count == 0)
        {
            slots = GetComponentsInChildren<UISeedSlot>(true).ToList();
        }

        inventory = MapControl.Instance.player.inventory;
        this.playerInventoryList = inventory.playerInventoryList;
        OpenSeedBasket();
    }
    //플레이어 인벤토리쪽을 이어두고


    public void OpenSeedBasket()
    {
        Debug.Log("바구니 열기");
        //플레이어 인벤토리에 있는 씨앗들 설정
        List<Item> seeds = new List<Item>();
        foreach (var item in playerInventoryList)
        {
            if (item.itemData.itemType == ItemType.Seed)
            {
                seeds.Add(item);
            }
            //그다음 씨앗들을 띄우고 그중에서 선택하게 한다
        }

        for(int i = 0; i < seeds.Count; i++)
        {
            slots[i].SetSeedSlot(seeds[i]);
        }
    }
}
