using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTileBehaviour : TileFloorInteraction
{
    public override void Interaction(Equipment tool, Tile tile)
    {
        if (tool.equipmentType == EquipmentType.Hoe)
        {
            tile.floorInteractionType = FloorInteractionType.Dirt;
        }
    }
}
