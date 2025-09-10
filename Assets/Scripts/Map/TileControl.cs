using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//레이어 1번, 2번에 대한 정보 저장용
public class TileControl : Singleton<TileControl>
{
    [SerializeField] public TileFloor[] tileStates;
    [SerializeField] public TileObject[] tileObjects;

    public TileFloor GetTileFloorByType(FloorInteractionType type)
    {
        TileFloor result = tileStates.First(state => state.floorType == type);
        return result;
    }

    public TileObject GetTileObjectByType(ObjectInteractionType type)
    {
        TileObject result = tileObjects.First(state => state.objectType == type);
        return result;
    }
}
