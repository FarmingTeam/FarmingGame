using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

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
    public int[] Size;
    public string Name;
}

public class MapLoadManager : Singleton<MapLoadManager>
{
    public const string MAPTILEPATH = "Json/MapData/";
    public const string TILEDATA = "TileData";

    public MapData mapData;
    //Debug Code
    //Refactor : Remove later
    private void Start()
    {
        LoadMap(SceneChangeManager.FARMSCENE);
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

        Tile[,] mapControlMapTile = MapControl.Instance.map.tiles;
        mapControlMapTile = new Tile[mapData.MapSize[0], mapData.MapSize[1]];
        for (int i = 0; i < mapData.MapSize[0]; i++)
            for (int j = 0; j < mapData.MapSize[1]; j++)
                mapControlMapTile[i, j] = new Tile();

        //후처리
        //MapControl에서 Floor 깔기
        for (int i = 0; i < mapData.TileFloor.Length; i++)
        {
            FloorData t = mapData.TileFloor[i];
            mapControlMapTile[t.Pos[0], t.Pos[1]].floorInteractionType = (FloorInteractionType)t.FloorType;
            MapControl.Instance.map.SetTileFloor(new Vector2Int(t.Pos[0], t.Pos[i]));
        }
        //MapControl에서 Object 넣기


    }
}
