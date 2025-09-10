using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileControl : Singleton<TileControl>
{
    [SerializeField] public TileState[] tileStates;
}
