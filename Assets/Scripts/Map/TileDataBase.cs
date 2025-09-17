using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//타일맵 데이터 베이스
//데이터 필요할 시 TileDataBase에서 찾아서 참조
public class TileDataBase : Singleton<TileDataBase>
{
    [SerializeField] public TileFloor[] tileFloors;
    [SerializeField] public ChunkData[] chunkDatas;
    [SerializeField] public SeedData[] seedDatas;

    readonly public Dictionary<FloorInteractionType, TileFloorInteraction> FLOORACTIONPAIR = new Dictionary<FloorInteractionType, TileFloorInteraction>
    {
        { FloorInteractionType.None, new GrassTileBehaviour()},
        { FloorInteractionType.Dirt, new DirtTileBehaviour()},
        { FloorInteractionType.WetDirt, new WetDirtTileBehaviour()},
        { FloorInteractionType.Water, new WaterTileBehaviour()}
    };

    readonly public Dictionary<ChunkInteractionType, TileObjectInteraction> OBJECTACTIONPAIR = new Dictionary<ChunkInteractionType, TileObjectInteraction>
    {
        { ChunkInteractionType.None, null},
        { ChunkInteractionType.Tree, new TreeTileBehaviour()},
        { ChunkInteractionType.Rock, new RockTileBehaviour()}
    };

    public TileFloor GetTileFloorByType(FloorInteractionType type)
    {
        TileFloor result = tileFloors.First(state => state.floorType == type);
        return result;
    }

    public ChunkData GetChunkDataByID(int id)
    {
        ChunkData result = chunkDatas.First(data => data.chunkType == (ChunkType)id);
        return result;
    }

    public SeedData GetSeedDataByID(int id)
    {
        SeedData result = seedDatas.First(data => data.itemID == id);
        return result;
    }

}
