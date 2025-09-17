using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;


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

public class MapSaveManager : Singleton<MapSaveManager>
{
    //저장데이터
    public const string RESOURCEINITIALPATH = "Json/InitialMapData/";
    public const string MAPTILEPATH = "MapData";
    public const string TILEDATA = "TileData";
    public MapData mapData;

    //Test DataBase
    public Map[] mapDataBase;

    //Refactor : Debug용 테스트 스크립트, 삭제 필요
    public Player playerPrefab; 
    public void Start()
    {
        LoadMap("TestFarm");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveMap("TestFarm");
    }
    //Refactor End


    public void LoadMap(string sceneName)
    {
        //맵 프리팹 불러오기
        //Refactor : MapControl과의 기능 비교 및 확인
        Map mapPrefab = mapDataBase.First(data => data.sceneName == sceneName);
        MapControl.Instance.map = Instantiate(mapPrefab);

        //Refactor : Debug용 테스트 스크립트, 삭제 필요
        MapControl.Instance.player = Instantiate(playerPrefab);
        MapControl.Instance.player.InitPos(new Vector3(1, 1, 0));
        //Refactor End

        //타일 File 경로 불러오기
        StringBuilder stringBuilder = new StringBuilder();
        //디렉토리 경로
        stringBuilder.Append(Application.persistentDataPath).Append("\\"). Append(MAPTILEPATH);
        Directory.CreateDirectory(stringBuilder.ToString());
        //파일 이름
        stringBuilder.Append("\\").Append(sceneName).Append(TILEDATA).Append(".json");

        //파일이 존재하지 않는다면, 기존 Resource내부의 InitialMap데이터를 가져온다.
        if (!File.Exists(stringBuilder.ToString()))
        {
            StringBuilder InitialFileString = new StringBuilder();
            var initialMap = Resources.Load(InitialFileString.Append(RESOURCEINITIALPATH).Append(sceneName).Append(TILEDATA).ToString()) as TextAsset;
            File.WriteAllText(stringBuilder.ToString(), initialMap.text);
        }
        var fileData = File.ReadAllText(stringBuilder.ToString());

        mapData = JsonUtility.FromJson<MapData>(fileData);
        MapControl.Instance.map.SetMap(mapData);

        //Refactor : Player 로드 로직이 여기에 필요한가
        //그렇다면 MapControl이 이것으로 대체 될 가능성이 있다.

    }

    public void SaveMap(string sceneName)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(MAPTILEPATH).Append("\\").Append(sceneName).Append(TILEDATA).Append(".json");
        string mapDataString = JsonUtility.ToJson(mapData, true);
        File.WriteAllText(stringBuilder.ToString(), mapDataString);
    }

    public void UpdateFloor(Vector2Int pos, FloorInteractionType type)
    {
        FloorData result = mapData.TileFloor.FirstOrDefault(data => data.Pos[0] == pos.x && data.Pos[1] == pos.y );
        //좌표에 데이터가 없을 경우 추가
        if (result == null)
        {
            if (type == FloorInteractionType.None)
                return;
            FloorData newdata = new FloorData();
            newdata.FloorType = (int)type;
            newdata.Pos = new int[] { pos.x, pos.y };
            mapData.TileFloor.Append(newdata);
        }
        //좌표에 데이터가 있을경우 수정
        else
        {
            //타일의 종류가 None이되면 좌표 데이터 삭제
            if (type == FloorInteractionType.None)
            {
                mapData.TileFloor.Remove(result);
                return;
            }
            result.FloorType = (int)type;
        }
    }
}
