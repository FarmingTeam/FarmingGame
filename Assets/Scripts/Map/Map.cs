using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [Header("ID")]
    public string sceneName;
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

    public void OnPlayerInteract(Vector2Int lookPos, EquipmentType tool)
    {
        Debug.Log("플레이어 아이템" + tool);
        //범위 밖 예외처리
        if(lookPos.x < 0 || lookPos.y < 0) return;
        if (lookPos.x >= tiles.GetLength(0) || lookPos.y >= tiles.GetLength(1)) return;

        //지역변수 설정
        Tile currentTile = tiles[lookPos.x, lookPos.y];

        //1번 레이어 인터렉션
        TileControl.Instance.FLOORACTIONPAIR[currentTile.floorInteractionType]?.Interaction(tool, currentTile);
        MapSaveManager.Instance.UpdateFloor(lookPos, currentTile.floorInteractionType);
        SetTileFloor(lookPos);

        //2번 레이어 인터렉션
        TileObjectAction(currentTile, tool);

        //3번 레이어 인터렉션
        if (tiles[lookPos.x, lookPos.y].seed.Interaction(tool, tiles[lookPos.x, lookPos.y], out TileBase tileBase))
        {
            SetTileSeed(lookPos, tileBase);
        }
    }

    public void SetMap(MapData mapData)
    {
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
            tiles[t.Pos[0], t.Pos[1]].chunkData = data;
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

    //Chunk의 경우 Tile이 Chunk 데이터 포함 해야함
    //설치의 경우를 고려할것 -> None에서도 인터렉션이 가능하도록 설정
    //2*2 설치의 경우를 추가로 고려할것 -> 플레이어의 위치 파악 필요
    public void TileObjectAction(Tile tile, EquipmentType equipment)
    {
        //연결된 시작지점 탐색
        Vector2Int startPos = tile.pos;
        if (objectGraph.ContainsKey(tile.pos))
        {
            for (int i = 0; i < objectGraph[tile.pos].Count; i++)
            {
                Vector2Int connectedpos = objectGraph[tile.pos][i];

                if (connectedpos.x < startPos.x)
                    startPos.x = connectedpos.x;
                if (connectedpos.y < startPos.y)
                    startPos.y = connectedpos.y;
            }
        }

        Tile InteractionTile = tiles[startPos.x, startPos.y];
        ChunkData changedChunk = null;
        //시작지점 타일 오브젝트 업데이트
        bool? isChanged = TileControl.Instance.OBJECTACTIONPAIR[InteractionTile.chunkData.interactionType]
            ?.Interaction(equipment, tile, out changedChunk);
        //만약 변경사항이 없다면 종료
        if (isChanged != true)
            return;

        //변경사항 반영
        List<Vector2Int> updatedpos = ChunkControl.Instance.SetTileObjectInMap(startPos, changedChunk);

        //시작지점 기점으로 저장정보 업데이트
        for(int i = 0;i < updatedpos.Count;i++)
            MapSaveManager.Instance.UpdateChunk(updatedpos[i], changedChunk);
    }
}
