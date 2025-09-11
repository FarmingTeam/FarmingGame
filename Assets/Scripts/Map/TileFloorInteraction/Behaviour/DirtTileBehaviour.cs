using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtTileBehaviour : TileFloorInteraction
{
    public override TileFloor Interaction(int tool)
    {
        //만약 도구가 물뿌리개이고, 물이 차 있다면 물뿌려진타일로 변경
        if (tool == 0)
        {
            return TileControl.Instance.GetTileFloorByType(FloorInteractionType.WetDirt);
        }
        //만약 도구가 곡괭이라면, 안갈린 땅으로 변경
        else if (tool == 1)
        {
            return TileControl.Instance.GetTileFloorByType(FloorInteractionType.None);
        }
        return null;
    }
}
