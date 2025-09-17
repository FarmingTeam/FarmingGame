using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Tile
{
    public Vector2Int pos;
    public FloorInteractionType floorInteractionType { get; set; }
    public ChunkData chunkData { get; set; }
    public TileSeed seed { get; set; } = new TileSeed ();

    public Tile()
    {
        this.floorInteractionType = FloorInteractionType.None;
        this.chunkData = TileControl.Instance.GetChunkDataByID(0);
    }
}
