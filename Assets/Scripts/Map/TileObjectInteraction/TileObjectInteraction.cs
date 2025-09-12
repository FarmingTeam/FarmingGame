using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileObjectInteraction
{
    public abstract bool Interaction(EquipmentType tool, Tile tile, out TileBase tileBase);
}
