using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtTileBehaviour : TileFloorInteraction
{
    public override void Interaction(EquipmentType tool, Tile tile)
    {
        //만약 도구가 물뿌리개이고, 물이 차 있다면 물뿌려진타일로 변경
        if (tool == EquipmentType.WateringCan)
        {
            tile.setFloor(FloorInteractionType.WetDirt);
        }
        //만약 도구가 곡괭이라면, 안갈린 땅으로 변경
        else if (tool == EquipmentType.Pickaxe)
        {
            tile.setFloor(FloorInteractionType.None);
        }
    }
}
