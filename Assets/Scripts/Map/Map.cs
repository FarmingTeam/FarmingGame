using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[Serializable]
public class MapData
{
    public int[] MapSize;
    public FloorData[] TileFloor;
    public ObjectData[] TileObject;
    public SeedTileData[] TileSeed;
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

    public void TileObjectAction(Tile tile, EquipmentType equipment)
    {
        TileBase tileBase = null;
        Vector2Int currentPos = tile.pos;
        bool? isChanged = TileControl.Instance.OBJECTACTIONPAIR[tiles[currentPos.x, currentPos.y].objectInteractionType]
            ?.Interaction(equipment, tile, out tileBase);
        if (isChanged == true)
            SetTileObject(currentPos, tileBase);
        if (!objectGraph.ContainsKey(currentPos))
            return;

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
        for (int i = 0; i < mapData.TileFloor.Length; i++)
        {
            FloorData t = mapData.TileFloor[i];
            tiles[t.Pos[0], t.Pos[1]].floorInteractionType = (FloorInteractionType)t.FloorType;
            MapControl.Instance.map.SetTileFloor(new Vector2Int(t.Pos[0], t.Pos[1]));
        }
        //Object 깔기
        for (int i = 0; i < mapData.TileObject.Length; i++)
        {
            ObjectData t = mapData.TileObject[i];
            ChunkData data = TileControl.Instance.GetChunkDataByID(t.ObjectType);
            ChunkControl.Instance.SetTileObjectInMap(new Vector2Int(t.Pos[0], t.Pos[1]), data);
        }
        //Seed 깔기
        for (int i = 0; i < mapData.TileSeed.Length; i++)
        {
            SeedTileData t = mapData.TileSeed[i];
            //Refactor : 나중에 단순화
            tiles[t.Pos[0], t.Pos[1]].seed.InitSeed(TileControl.Instance.GetSeedDataByID(t.SeedType));
            tiles[t.Pos[0], t.Pos[1]].seed.PlantedDate = t.PlantedDate;
            SetTileSeed(new Vector2Int(t.Pos[0], t.Pos[1]), tiles[t.Pos[0], t.Pos[1]].seed.SeedState());
            
        }
    }
}
