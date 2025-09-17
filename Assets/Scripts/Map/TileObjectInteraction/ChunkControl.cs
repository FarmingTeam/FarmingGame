using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ChunkControl : Singleton<ChunkControl>
{
    public List<Vector2Int> SetTileObjectInMap(Vector2Int startPos, ChunkData chunkData)
    {
        List<Vector2Int> result = new List<Vector2Int>();
        List<Vector2Int> connected = new List<Vector2Int>();
        connected.Add(startPos);
        //연결된 오브젝트 그래프 분리 및 재할당
        if (MapControl.Instance.map.objectGraph.ContainsKey(startPos))
        {
            for (int i = 0; i < MapControl.Instance.map.objectGraph[startPos].Count; i++)
            {
                Vector2Int element = MapControl.Instance.map.objectGraph[startPos][i];
                MapControl.Instance.map.objectGraph.Remove(element);
                connected.Add(element);
            }
            MapControl.Instance.map.objectGraph.Remove(startPos);
        }
        for (int i = 0; i < connected.Count; i++)
        {
            if (UpdateTiles(connected[i], chunkData))
            {
                result.Add(connected[i]);
            }
        }
        return result;
    }

     bool UpdateTiles(Vector2Int startPos, ChunkData chunkData)
    {
        if (MapControl.Instance.map.objectGraph.ContainsKey(startPos))
            return false;
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

                MapControl.Instance.map.tiles[curx, cury].chunkData = chunkData;
                MapControl.Instance.map.objectGraph.Add(new Vector2Int(curx, cury), connectPos);
                MapControl.Instance.map.SetTileObject(new Vector2Int(curx, cury), chunkData.tileBases[i].Tiles[j]);
            }
        }
        return true;
    }
}
