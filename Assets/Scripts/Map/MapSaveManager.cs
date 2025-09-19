using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


[Serializable]
public class MapData
{
    public int[] MapSize;
    public List<MapFloorData> TileFloor;
    public List<MapChunkData> TileObject;
    public List<MapSeedData> TileSeed;
}

[Serializable]
public class MapFloorData
{
    public int[] Pos;
    public int FloorType;
}
[Serializable]
public class MapChunkData
{
    public int[] Pos;
    public int ChunkType;
}

[Serializable]
public class MapSeedData
{
    public int[] Pos;
    public int SeedType;
    public bool IsPlanted;
    public int PlantedDate;
}

public class MapSaveManager : Singleton<MapSaveManager>
{
    //저장데이터
    public const string RESOURCEINITIALPATH = "MapData/InitialMapJson/";
    public const string MAPTILEPATH = "MapData";
    public const string TILEDATA = "TileData";
    public MapData mapData;
    public const string PLAYERPREFABPATH = "PlayerData/TsetPlayer";

    public void LoadMap(string sceneName)
    {
        //맵 프리팹 불러오기

        if (MapControl.Instance.map == null)
        {
            Map mapPrefab = MapDataBase.Instance.Maps.First(data => data.sceneName == sceneName);
            MapControl.Instance.map = Instantiate(mapPrefab);
        }

        //타일 File 경로 불러오기
        StringBuilder stringBuilder = new StringBuilder();
        //디렉토리 경로
        stringBuilder.Append(Application.persistentDataPath).Append("/"). Append(MAPTILEPATH);
        Directory.CreateDirectory(stringBuilder.ToString());
        //파일 이름
        stringBuilder.Append("/").Append(sceneName).Append(TILEDATA).Append(".json");

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


        //플레이어가 없다면 플레이어 생성 및 로드
        //Refactor : 인벤토리 로드 필요할지도
        if (MapControl.Instance.player == null)
        {
            GameObject playerObject = Instantiate(Resources.Load<MonoBehaviour>(PLAYERPREFABPATH)).gameObject;
            MapControl.Instance.player = playerObject.GetComponent<Player>();
        }
        MapControl.Instance.player.InitPos(new Vector3(1, 1, 0));
        //Refactor End

    }

    public void SaveMap(string sceneName)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(Application.persistentDataPath).Append("/").Append(MAPTILEPATH).Append("/").Append(sceneName).Append(TILEDATA).Append(".json");
        string mapDataString = JsonUtility.ToJson(mapData, true);
        Debug.Log(stringBuilder.ToString());
        File.WriteAllText(stringBuilder.ToString(), mapDataString);
    }

    public void UpdateFloor(Vector2Int pos, FloorInteractionType type)
    {
        MapFloorData result = mapData.TileFloor.FirstOrDefault(data => data.Pos[0] == pos.x && data.Pos[1] == pos.y );
        //좌표에 데이터가 없을 경우 추가
        if (result == null)
        {
            if (type == FloorInteractionType.None)
                return;
            MapFloorData newdata = new MapFloorData();
            newdata.FloorType = (int)type;
            newdata.Pos = new int[] { pos.x, pos.y };
            mapData.TileFloor.Add(newdata);
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

    public void UpdateChunk(Vector2Int pos, ChunkData data)
    {
        MapChunkData result = mapData.TileObject.FirstOrDefault(data => data.Pos[0] == pos.x && data.Pos[1] == pos.y);
        //좌표에 데이터가 없을 경우 추가
        if (result == null)
        {
            if (data.chunkType == ChunkType.None)
                return;
            MapChunkData newdata = new MapChunkData();
            newdata.ChunkType = (int)data.chunkType;
            newdata.Pos = new int[] { pos.x, pos.y };
            mapData.TileObject.Append(newdata);
        }
        //좌표에 데이터가 있을경우 수정
        else
        {
            //오브젝트의 종류가 None이되면 좌표 데이터 삭제
            if (data.chunkType == ChunkType.None)
            {
                mapData.TileObject.Remove(result);
                return;
            }
            result.ChunkType = (int)data.chunkType;
        }
    }

    public void UpdateSeed(Vector2Int pos, TileSeed seed)
    {
        MapSeedData result = mapData.TileSeed.FirstOrDefault(data => data.Pos[0] == pos.x && data.Pos[1] == pos.y);
        //좌표에 데이터가 없을 경우 추가
        if (result == null)
        {
            if (seed.seedType == -1)
                return;
            MapSeedData newdata = new MapSeedData();
            newdata.SeedType = seed.seedType;
            newdata.Pos = new int[] { pos.x, pos.y };
            newdata.PlantedDate = seed.plantedDate;
            seed.UpdateRemainingDate();
            newdata.IsPlanted = seed.isPlanted;
            mapData.TileSeed.Add(newdata);
        }
        //좌표에 데이터가 있을경우 수정
        else
        {
            //씨앗의 종류가 None이되면 좌표 데이터 삭제
            if (seed.seedType == -1)
            {
                mapData.TileSeed.Remove(result);
                return;
            }
            result.SeedType = (int)seed.seedType;
            result.PlantedDate = seed.plantedDate;
            seed.UpdateRemainingDate();
            result.IsPlanted = seed.isPlanted;
        }
    }
}
