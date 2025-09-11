using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTileBehaviour : TileObjectInteraction
{
    public override TileObject Interaction(EquipmentType tool)
    {
        //만약 도끼를 들고있다면으로 수정
        if (tool == EquipmentType.Axe)
        {
            return TileControl.Instance.GetTileObjectByType(ObjectInteractionType.None);
        }
        return null;
    }
}
