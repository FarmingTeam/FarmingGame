using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeTileBehaviour : TileObjectInteraction
{
    public override bool Interaction(EquipmentType tool, Tile tile, out ChunkData chunkData)
    {
        //만약 도끼를 들고있다면으로 수정
        if (tool == EquipmentType.Axe)
        {
            chunkData = TileControl.Instance.GetChunkDataByID((int)ObjectType.None);
            return true;
        }
        chunkData = null;
        return false;
    }
}
