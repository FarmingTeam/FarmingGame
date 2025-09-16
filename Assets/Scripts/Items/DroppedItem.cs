using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public ItemData data;
    public int quantity = 1;

    public void Init(ItemData data, int amount = 1)
    {
        this.data = data;
        this.quantity = Mathf.Max(1, amount);
    }
}
