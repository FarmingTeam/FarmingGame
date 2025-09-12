using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ChunkControl : Singleton<ChunkControl>
{
    public void SetTileObjectInMap(Vector2Int startPos, ChunkData chunkData)
    {
        int x = startPos.x;
        int y = startPos.y;
        List<Vector2Int> curentData = new List<Vector2Int>();

        for (int i = 0; i < chunkData.tileBases.Length; i++)
        {
            for (int j = 0; j < chunkData.tileBases[i].Tiles.Length; j++)
            {
                int curx = x + i;
                int cury = y + j;
                curentData.Add(new Vector2Int(curx, cury));
            }
        }


        for (int i = 0; i < chunkData.tileBases.Length; i++)
        {
            for (int j = 0; j < chunkData.tileBases[i].Tiles.Length; j++)
            {
                int curx = x + i;
                int cury = y + j;
                List<Vector2Int> connectPos = curentData.ToList<Vector2Int>();
                connectPos.Remove(new Vector2Int(curx, cury));

                MapControl.Instance.map.tiles[curx,cury].setObject(chunkData.interactionType);
                MapControl.Instance.map.objectGraph.Add(new Vector2Int(curx, cury), connectPos);
                MapControl.Instance.map.SetTileObject(new Vector2Int(curx, cury), chunkData.tileBases[i].Tiles[j]);
            }
        }
    }
}
