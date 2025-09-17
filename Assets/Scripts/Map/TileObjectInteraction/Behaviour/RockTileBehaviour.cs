using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RockTileBehaviour : TileObjectInteraction
{
    public override bool Interaction(Equipment tool, Tile tile, out ChunkData chunkData)
    {
        if (tool.equipmentType == EquipmentType.Pickaxe)
        {
            chunkData = TileDataBase.Instance.GetChunkDataByID((int)ChunkType.None);
            //Drop Rock Item here
            return true;
        }
        chunkData = null;
        return false;
    }
}
