using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DroppedItem
{
    // 아이템을 인벤토리로 전달
    bool Collect(ItemData data, int amount);
}
