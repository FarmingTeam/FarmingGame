using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//레이어 1번, 2번에 대한 정보 저장용
public class TileControl : Singleton<TileControl>
{
    [SerializeField] public TileFloor[] tileFloors;
    [SerializeField] public TileObject[] tileObjects;

    readonly public Dictionary<FloorInteractionType, TileFloorInteraction> FLOORACTIONPAIR = new Dictionary<FloorInteractionType, TileFloorInteraction>
    {
        { FloorInteractionType.None, new GrassTileBehaviour()},
        { FloorInteractionType.Dirt, new DirtTileBehaviour()},
        { FloorInteractionType.WetDirt, new WetDirtTileBehaviour()},
        { FloorInteractionType.Water, new WaterTileBehaviour()}
    };

    readonly public Dictionary<ObjectInteractionType, TileObjectInteraction> OBJECTACTIONPAIR = new Dictionary<ObjectInteractionType, TileObjectInteraction>
    {
        { ObjectInteractionType.None, null},
        { ObjectInteractionType.Tree, new TreeTileBehaviour()},
        { ObjectInteractionType.Rock, new RockTileBehaviour()}

    };


    public TileFloor GetTileFloorByType(FloorInteractionType type)
    {
        TileFloor result = tileFloors.First(state => state.floorType == type);
        return result;
    }

    public TileObject GetTileObjectByType(ObjectInteractionType type)
    {
        TileObject result = tileObjects.First(state => state.objectType == type);
        return result;
    }
}
