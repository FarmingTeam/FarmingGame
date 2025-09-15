using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[Serializable]
public class SlotData
{
    public Item slotItem;
}


public class PlayerInventory : MonoBehaviour
{
   
    

    public List<SlotData> slotDataList = new List<SlotData>();

    //여기 씨앗 리스트에는 인벤토리중 씨앗만(인벤토리는 씨앗포함 모두)
    public List<Item> seedList = new List<Item>();

    public int InventoryMaxNum { get; } = 20;


    //이 갯수 체인지가 될경우에는 슬롯들의 상황을 업데이트 해줍니다


    private Action onItemChange;

    public Equipment selectedEquipment;

    public Item CreateRuntimeItemData(ItemData itemData)
    {
        return new Item(itemData);
    }

    public Item CreateRuntimeItemData(ItemData itemData,int quantity)
    {
        return new Item(itemData,quantity);
    }


    //단일 아이템 더하기
    public void AdditemsByID(int itemID,int addQuantity)     
    {
        ItemData itemdata= ResourceManager.Instance.GetItem(itemID);

        //만약 쌓을 수 있는 아이템이라면 일단 모든 슬롯데이터 리스트를 확인한다

        int quantity=addQuantity;
        //근데 이 공식의 치명적인 문제는 결국 여유분 슬롯을 찾지못하면 quantity를 줄일 방법이 없어서 무한루프에 빠진다는 점이다. 

        if (itemdata.isStackable)
        {
            for (int i = 0; i < slotDataList.Count; i++)
            {



                //?. 를 통하여 slotitem이 null일경우를 대비
                if (slotDataList[i].slotItem?.itemData == itemdata)
                {
                    //만약 슬롯데이터의 아이템의 데이터가 들어온 아이템데이터와 같다면
                    //우선 현재 그 슬롯에 들어있는 수량을 확인한다. 
                    //그리고 그 아이템의 최대수량- 현재수량으로 더 넣을수 있는 갯수를 체크한다.


                    //사실 여기 if문으로 들어온 시점부터 이 슬롯의 슬롯아이템은 null일수없음
                    Item thisSlotItem = slotDataList[i].slotItem;
                    int canAddNum = CheckCanAddNum(thisSlotItem);
                    //만약 더 넣을 여유칸이 없다면 이 슬롯은 스킵한다
                    if (canAddNum == 0)
                    {
                        continue;
                    }
                    else
                    {
                        // 추가되는 양이 여유분보다 많은지 적은지 체킹한다


                        //만약 추가하려는 수량이 여유분보다 많다면

                        //이 슬롯의 아이템 수량은 그 아이템의 최대갯수가 된다
                        if (quantity > canAddNum)
                        {
                            thisSlotItem.currentQuantity = thisSlotItem.itemData.maxQuantity;
                            //그리고 남은 것들은 다시 또 딴슬롯을 찾아야한다. 

                            quantity -= canAddNum;

                            continue;
                        }
                        else
                        {
                            //만약 여유분이 더하려는 양보다 많다면
                            thisSlotItem.currentQuantity += quantity;
                            //다 넣었으니 이제 배정해야할 quantity가 없어
                            quantity = 0;
                            //여기에 이제 완료 업데이트
                            onItemChange.Invoke();
                            return;
                        }
                    }
                }
            }

            //만약 슬롯들 쭉 살펴봤는데 기존에 이거랑 같은 아이템이 없어 그럼 새로 더미를 만들어줘야겠지?
            PlaceRemainingQuantity(itemdata, quantity);
            //남은 수량 관리 메서드에 넘겨주고 이 메서드는 여기서 끝내기
            return;
            //더미는 언제까지 만들어? 남은 수량을 다 쓸때까지
            //여기까지는 그럼 이제 배정받지 못하고 남은 quantity가 있겠지



            //애초부터 빈슬롯이 있는지도 체크가 필요
        }
        else
        {
            //만약 쌓을수 없는 아이템이면 그냥 바로 남은 갯수 배치 메서드로 보내기
            PlaceRemainingQuantity(itemdata,quantity);
        }












    }
    void PlaceRemainingQuantity(ItemData itemData, int remainingQuantity)
    {
        while(remainingQuantity > 0)
        {

            //일단 슬롯이 비었는지 확인
            int slotIndex = FindFirstEmptyInventoryIndex();
            if (slotIndex == -1)
            {
                //아예 남은 슬롯이 없음
                Debug.Log("남은 슬롯 없음");
                return;
            }
            //만약 남은 갯수가 아이템 최대 수량보다 많다면
            if(remainingQuantity>itemData.maxQuantity)
            {
                Item newItem = CreateRuntimeItemData(itemData,itemData.maxQuantity);
                remainingQuantity-=itemData.maxQuantity;
                slotDataList[slotIndex].slotItem = newItem;
                

            }
            else
            {
                Item newItem = CreateRuntimeItemData(itemData, remainingQuantity);
                remainingQuantity = 0;
                slotDataList[slotIndex].slotItem = newItem;

                //여기에 완료 업데이트
                onItemChange.Invoke();
            }
            
        }
        
    }



    int FindFirstEmptyInventoryIndex()
    {
        for(int i=0; i<slotDataList.Count; i++)
        {
            if (slotDataList[i].slotItem==null)
            {
                return i;
            }
        }
        return -1;
    }


    // 이 메서드를 통해 현재 이 아이템은 몇개의 여유 수용 갯수( 최대수량으로 부터)가 있는지 알려줍니다
    int CheckCanAddNum(Item item)
    {
        return (item.itemData.maxQuantity) - (item.currentQuantity);
    }








    public void SubscribeOnItemChange(Action action)
    {
        onItemChange += action;
    }

    public void UnsubscribeOnItemChange(Action action)
    {
        onItemChange -= action;
    }


}
