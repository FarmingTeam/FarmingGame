using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RockTileBehaviour : TileObjectInteraction
{
    public override bool Interaction(EquipmentType tool, Tile tile, out ChunkData chunkData)
    {
        //만약 곡괭이를 들고있다면으로 수정
        if (tool == EquipmentType.Pickaxe)
        {
            chunkData = TileControl.Instance.GetChunkDataByID((int)ObjectType.None);
            
            return true;
        }
        chunkData = null;
        return false;
    }
}
