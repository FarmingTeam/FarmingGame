using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTileBehaviour : TileFloorInteraction
{
    public override void Interaction(EquipmentType tool, Tile tile)
    {
        //만약 괭이를 들고있다면으로 수정
        if (tool == EquipmentType.Hoe)
        {
            tile.SetFloor(FloorInteractionType.Dirt);
        }
    }
}
