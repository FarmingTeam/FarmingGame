using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTileBehaviour : TileFloorInteraction
{
    public override TileFloor Interaction(EquipmentType tool)
    {
        //만약 물뿌리개를 들고있다면으로 수정
        if (tool == EquipmentType.WateringCan)
        {
            //pass
        }
        return null;
    }
}
