using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[Serializable]
public class MapData
{
    public int[] MapSize;
    public List<FloorData> TileFloor;
    public List<ObjectData> TileObject;
    public List<SeedTileData> TileSeed;
}

[Serializable]
public class FloorData
{
    public int[] Pos;
    public int FloorType;
}
[Serializable]
public class ObjectData
{
    public int[] Pos;
    public int ObjectType;
}

[Serializable]
public class SeedTileData
{
    public int[] Pos;
    public int SeedType;
    public bool IsPlanted;
    public int PlantedDate;
}
public class Map : MonoBehaviour
{
    //저장데이터
    public const string MAPTILEPATH = "Json/MapData/";
    public const string TILEDATA = "TileData";
    public MapData mapData;

    [Header("타일맵 0번 레이어")]
    [SerializeField] Tilemap baseGroundGrass;
    [SerializeField] Tilemap baseGroundWater;
    [SerializeField] Tilemap baseGroundWall;

    [Header("타일맵 1번 레이어")]
    [SerializeField] Tilemap TileFloor;
    [Header("타일맵 2번 레이어")]
    [SerializeField] Tilemap TileObject;
    public Dictionary<Vector2Int, List<Vector2Int>> objectGraph = new Dictionary<Vector2Int, List<Vector2Int>>();
    [Header("타일맵 3번 레이어")]
    [SerializeField] Tilemap TileSeed;

    [Header("타일 정보")]
    [SerializeField] public Tile[,] tiles;

    //Debug Start
    //Refactor : Remove Later , move to Scene Loader
    private void Start()
    {
        LoadMap("TestMap");
    }

    public void OnPlayerInteract(Vector2Int lookPos, EquipmentType tool)
    {
        if(lookPos.x < 0 || lookPos.y < 0) return;
        if (lookPos.x >= tiles.GetLength(0) || lookPos.y >= tiles.GetLength(1)) return;
        tiles[lookPos.x, lookPos.y].OnInteract(tool);
        SetTileFloor(lookPos);
        TileObjectAction(tiles[lookPos.x, lookPos.y], tool);
    }

    public void SetTileFloor(Vector2Int index)
    {
        FloorData currentData = mapData.TileFloor.FirstOrDefault(data => data.Pos[0] == index.x && data.Pos[1] == index.y);
        if (currentData == null)
        {
            currentData = new FloorData();
            currentData.Pos[0] = index.x;
            currentData.Pos[1] = index.y;
            currentData.FloorType = (int)tiles[index.x, index.y].floorInteractionType;
        }
        else
        {
            currentData.FloorType = (int)tiles[index.x, index.y].floorInteractionType;
        }
        TileFloor.SetTile((Vector3Int)index, TileControl.Instance.GetTileFloorByType(tiles[index.x, index.y].floorInteractionType).tileBase);
    }

    public void SetTileObject(Vector2Int index, TileBase tile)
    {
        TileObject.SetTile((Vector3Int)index, tile);
    }

    public void SetTileSeed(Vector2Int index, TileBase tile)
    {
        TileSeed.SetTile((Vector3Int)index, tile);
    }

    //Chunk의 경우 Tile이 Chunk 데이터 포함 해야함
    //설치의 경우를 고려할것 -> None에서도 인터렉션이 가능하도록 설정
    //2*2 설치의 경우를 추가로 고려할것 -> 플레이어의 위치 파악 필요
    public void TileObjectAction(Tile tile, EquipmentType equipment)
    {
        TileBase tileBase = null;
        Vector2Int currentPos = tile.pos;
        bool? isChanged = TileControl.Instance.OBJECTACTIONPAIR[tiles[currentPos.x, currentPos.y].objectInteractionType]
            ?.Interaction(equipment, tile, out tileBase);
        if (isChanged == true)
        {
            // Refactor : Need to Add Object Save Data management
            SetTileObject(currentPos, tileBase);
        }
        if (!objectGraph.ContainsKey(currentPos))
        {
            return;
        }

        for (int i = 0; i < objectGraph[currentPos].Count; i++)
        {
            Vector2Int connectedpos = objectGraph[currentPos][i];
            Tile connectedTile = tiles[connectedpos.x, connectedpos.y];
            isChanged = TileControl.Instance.OBJECTACTIONPAIR[connectedTile.objectInteractionType]
                ?.Interaction(equipment, connectedTile, out tileBase);
            if (isChanged == true)
                SetTileObject(connectedpos, tileBase);
        }

    }

    public void LoadMap(string sceneName)
    {
        //File 경로 불러오기
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(MAPTILEPATH).Append(sceneName).Append(TILEDATA);
        var file = Resources.Load(stringBuilder.ToString()) as TextAsset;
        if (file == null)
        {
            throw new System.Exception(stringBuilder.Append("Is not Valid").ToString());
        }
        mapData = JsonUtility.FromJson<MapData>(file.text);
        
        //타일 크기 지정
        tiles = new Tile[mapData.MapSize[0], mapData.MapSize[1]];
        for (int i = 0; i < mapData.MapSize[0]; i++)
            for (int j = 0; j < mapData.MapSize[1]; j++)
            {
                tiles[i, j] = new Tile();
                tiles[i, j].pos = new Vector2Int(i, j);
            }

        //Floor 깔기
        for (int i = 0; i < mapData.TileFloor.Count; i++)
        {
            FloorData t = mapData.TileFloor[i];
            tiles[t.Pos[0], t.Pos[1]].floorInteractionType = (FloorInteractionType)t.FloorType;
            MapControl.Instance.map.SetTileFloor(new Vector2Int(t.Pos[0], t.Pos[1]));
        }
        //Object 깔기
        for (int i = 0; i < mapData.TileObject.Count; i++)
        {
            ObjectData t = mapData.TileObject[i];
            ChunkData data = TileControl.Instance.GetChunkDataByID(t.ObjectType);
            ChunkControl.Instance.SetTileObjectInMap(new Vector2Int(t.Pos[0], t.Pos[1]), data);
        }
        //Seed 깔기
        for (int i = 0; i < mapData.TileSeed.Count; i++)
        {
            SeedTileData t = mapData.TileSeed[i];
            //Refactor : 나중에 단순화
            tiles[t.Pos[0], t.Pos[1]].seed.InitSeed(TileControl.Instance.GetSeedDataByID(t.SeedType));
            tiles[t.Pos[0], t.Pos[1]].seed.PlantedDate = t.PlantedDate;
            SetTileSeed(new Vector2Int(t.Pos[0], t.Pos[1]), tiles[t.Pos[0], t.Pos[1]].seed.SeedState());
        }
    }

}
