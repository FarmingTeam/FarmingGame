using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeTileBehaviour : TileObjectInteraction
{
    public override bool Interaction(Equipment tool, Tile tile, out ChunkData chunkData)
    {
        if (tool.equipmentType == EquipmentType.Axe)
        {
            chunkData = TileDataBase.Instance.GetChunkDataByID((int)ChunkType.None);
            //Drop Wood Item
            return true;
        }
        chunkData = null;
        return false;
    }
}
