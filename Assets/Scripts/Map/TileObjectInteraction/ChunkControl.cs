using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ChunkControl : Singleton<ChunkControl>
{
    public void SetTileObjectInMap(Vector2Int startPos, ChunkData chunkData)
    {
        int x = startPos.x;
        int y = startPos.y;

        for (int i = 0; i < chunkData.tileBases.Length; i++)
        {
            for (int j = 0; j < chunkData.tileBases[i].Tiles.Length; j++)
            {
                int curx = x + i;
                int cury = y + j;

                MapControl.Instance.map.tiles[curx,cury].objectInteractionType = chunkData.interactionType;
                MapControl.Instance.map.SetTileObject(new Vector2Int(curx, cury), chunkData.tileBases[i].Tiles[j]);
            }
        }
    }
}
