using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[Serializable]
public class MapData
{
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

        //후처리
        //MapControl에서 Map을 가져와서 타일바닥 업데이트 시키기

        //MapControl에서 Object 넣기

    }
}
