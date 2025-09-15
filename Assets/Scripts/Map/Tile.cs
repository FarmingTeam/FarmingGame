using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Tile
{
    public Vector2Int pos;
    public FloorInteractionType floorInteractionType { get; private set; }
    public ObjectInteractionType objectInteractionType { get; private set; }
    bool canFloorEnter = true;
    bool canObjectEnter = true;

    public void SetFloor(FloorInteractionType type)
    {
        floorInteractionType = type;
        if (floorInteractionType == FloorInteractionType.Water)
            canFloorEnter = false;
        else
            canFloorEnter = true;
    }

    public void SetObject(ObjectInteractionType type)
    {
        objectInteractionType = type;
        switch (objectInteractionType)
        {
            case ObjectInteractionType.None:
                //Add case
                canObjectEnter = true;
                break;
            default:
                canObjectEnter = false;
                break;
        }
    }

    public Tile()
    {
        this.floorInteractionType = FloorInteractionType.None;
        this.objectInteractionType = ObjectInteractionType.None;
    }
    public Tile(FloorInteractionType floorInteractionType, ObjectInteractionType objectInteraction)
    {
        this.floorInteractionType = floorInteractionType;
        this.objectInteractionType = objectInteraction;
    }
    public void OnInteract(EquipmentType equipment)
    { 
        TileControl.Instance.FLOORACTIONPAIR[floorInteractionType]?.Interaction(equipment, this);
    }

    public bool CanEnter()
    {
        return canFloorEnter && canObjectEnter;
    }
}
