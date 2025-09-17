using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileObjectInteraction
{
    public abstract bool Interaction(Equipment tool, Tile tile, out ChunkData chunkData);
}
