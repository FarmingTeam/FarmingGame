using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> playerInventoryList = new List<Item>();

    [SerializeField] UIInventory uiInventory;
    [SerializeField] Button carrotbutton;
    [SerializeField] Button pumpkinButton;
    public int InventoryMaxNum { get; } = 10;

    private void Start()
    {
        carrotbutton.onClick.AddListener(() => AdditemsByID(1));
        pumpkinButton.onClick.AddListener(() => AdditemsByID(2));
        
    }

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
                        uiInventory.RefreshAllSlots();
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
        //빈 슬롯에 아이템 추가
    }

    public void OpenSeedBasket()
    {
        //플레이어 인벤토리에 있는 씨앗들 설정
        List<Item> seeds= new List<Item>();
        foreach(var item in playerInventoryList)
        {
            if(item.itemData.itemType==ItemType.Seed)
            {
                seeds.Add(item);
            }
            //그다음 씨앗들을 띄우고 그중에서 선택하게 한다
        }
    }
}
