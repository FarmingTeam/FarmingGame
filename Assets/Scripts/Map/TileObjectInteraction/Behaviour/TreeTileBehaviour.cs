using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeTileBehaviour : TileObjectInteraction
{
    public override bool Interaction(EquipmentType tool, Tile tile, out TileBase tileBase)
    {
        //만약 도끼를 들고있다면으로 수정
        if (tool == EquipmentType.Axe)
        {
            tile.setObject(ObjectInteractionType.None);
            tileBase = null;
            return true;
        }
        tileBase = null;
        return false;
    }
}
