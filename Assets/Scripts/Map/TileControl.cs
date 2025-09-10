using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileControl : Singleton<TileControl>
{
    [SerializeField] public TileState[] tileStates;

    public TileState GetTileStateByType(TileInteractionType type)
    {
        TileState result = tileStates.First(state => state.tileType == type);
        return result;
    }
}
