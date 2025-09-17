using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAccepter : MonoBehaviour, DroppedItem
{
    // 플레이어 인벤토리
    public PlayerInventory inventory;

    public bool Collect(ItemData data, int amount)
    {
        if (data == null || amount <= 0) return false;
        return AddInventory((int)data.itemID, amount);
    }

    private bool AddInventory(int id, int amount)
    {
        inventory.AdditemsByID(id, amount);
        return true;
    }
}
