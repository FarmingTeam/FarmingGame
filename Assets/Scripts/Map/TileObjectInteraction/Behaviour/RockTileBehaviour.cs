using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RockTileBehaviour : TileObjectInteraction
{
    public override bool Interaction(EquipmentType tool, Tile tile, out TileBase tileBase)
    {
        //만약 곡괭이를 들고있다면으로 수정
        if (tool == EquipmentType.Pickaxe)
        {
            tile.chunkData = TileControl.Instance.GetChunkDataByID((int)ObjectType.None);
            tileBase = null;
            return true;
        }
        tileBase = null;
        return false;
    }
}
