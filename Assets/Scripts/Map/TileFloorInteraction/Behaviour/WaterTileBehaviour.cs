using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTileBehaviour : TileFloorInteraction
{
    public override void Interaction(EquipmentType tool, Tile tile)
    {
        //만약 물뿌리개를 들고있다면으로 수정
        if (tool == EquipmentType.WateringCan)
        {
            //pass
        }
    }
}
