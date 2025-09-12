using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[Serializable]
public class MapData
{
    public int[] MapSize;
    public FloorData[] TileFloor;
    public ObjectData[] TileObject;
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

    [Header("타일 정보")]
    [SerializeField] public Tile[,] tiles;

    //Debug Start
    //Refactor : Remove Later , move to Scene Loader
    private void Start()
    {
        LoadMap("TestMap");
    }

    //Debug Update 
    //Refactor : Remove Later
    //EX) tiles[가로위치, 세로로위치]
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            
        }
    }

    public void OnPlayerInteract(Vector2Int lookPos, Equipment tool)
    {
        tiles[lookPos.x, lookPos.y].OnInteract(tool.equipmentType);
        SetTileFloor(lookPos);
    }

    public void SetTileFloor(Vector2Int index)
    {
        TileFloor.SetTile((Vector3Int)index, TileControl.Instance.GetTileFloorByType(tiles[index.x, index.y].floorInteractionType).tileBase);
    }

    public void SetTileObject(Vector2Int index, TileBase tile)
    {
        TileObject.SetTile((Vector3Int)index, tile);
    }

    public void LoadMap(string sceneName)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(MAPTILEPATH).Append(sceneName).Append(TILEDATA);
        var file = Resources.Load(stringBuilder.ToString()) as TextAsset;
        if (file == null)
        {
            throw new System.Exception(stringBuilder.Append("Is not Valid").ToString());
        }
        mapData = JsonUtility.FromJson<MapData>(file.text);

        tiles = new Tile[mapData.MapSize[0], mapData.MapSize[1]];
        for (int i = 0; i < mapData.MapSize[0]; i++)
            for (int j = 0; j < mapData.MapSize[1]; j++)
                tiles[i, j] = new Tile();

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
    }
}
