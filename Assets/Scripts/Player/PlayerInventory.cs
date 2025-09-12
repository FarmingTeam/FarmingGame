using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> playerInventoryList = new List<Item>();
    

    //여기 씨앗 리스트에는 인벤토리중 씨앗만(인벤토리는 씨앗포함 모두)
    public List<Item> seedList = new List<Item>();

    public UIInventory uiInventory;

    public UISeedBasket uiSeedBasket;
    public int InventoryMaxNum { get; } = 20;


    //이 갯수 체인지가 될경우에는 슬롯들의 상황을 업데이트 해줍니다
    private Action onQuantityChange;




    public Equipment selectedEquipment;

    public Item CreateRuntimeItemData(ItemData itemData)
    {
        return new Item(itemData);
    }

    public void AdditemsByID(int itemID)     
    {
        ItemData itemdata= ResourceManager.Instance.GetItem(itemID);
        if(itemdata.isStackable)
        {
            foreach(var inventoryItem in playerInventoryList)
            {
                if(inventoryItem.itemData.itemID == itemID)
                {
                    if(inventoryItem.itemData.maxQuantity>inventoryItem.currentQuantity)
                    {
                        inventoryItem.currentQuantity++;                    
                        onQuantityChange?.Invoke();
                        //인벤토리가 열려있지 않으면 사실 수량 변화자체는 굳이 UI에 반영할 이유는 없어서
                        //이 이벤트를 구독하는 인벤토리ui들은 전부 Onenable떄 구독을 활성화하고, Disable떄 구독을 해지합니다
                        return;
                    }
                }
            }
            
        }
        if(playerInventoryList.Count>=InventoryMaxNum)
        {
            Debug.Log("인벤토리가 꽉 차 더이상 아이템을 담을 수 없습니다");
            return;
        }
        Item item= CreateRuntimeItemData(itemdata);
        playerInventoryList.Add(item);
        uiInventory.SetItemsUI(item);
        
        if(item.itemData.itemType == ItemType.Seed)
        {
            //만약 씨앗이면 씨앗 리스트에 추가(?)
            uiSeedBasket.SetSeedSlotUI(item);
        }
        


        
    }

    public void SubscribeOnQuantityChange(Action action)
    {
        onQuantityChange += action;
    }

    public void UnsubscribeOnQuantityChange(Action action)
    {
        onQuantityChange -= action;
    }


    
}
