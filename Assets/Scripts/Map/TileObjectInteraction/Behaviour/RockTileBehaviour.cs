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
            chunkData = TileDataBase.Instance.GetChunkDataByID((int)ChunkType.None);
            
            return true;
        }
        chunkData = null;
        return false;
    }
}
