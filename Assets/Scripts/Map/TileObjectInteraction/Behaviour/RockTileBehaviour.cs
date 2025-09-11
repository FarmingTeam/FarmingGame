using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTileBehaviour : TileObjectInteraction
{
    public override TileObject Interaction(EquipmentType tool)
    {
        //만약 곡괭이를 들고있다면으로 수정
        if (tool == EquipmentType.Pickaxe)
        {
            return TileControl.Instance.GetTileObjectByType(ObjectInteractionType.None);
        }
        return null;
    }
}
