using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Tile
{
    [SerializeField] public FloorInteractionType floorInteractionType = FloorInteractionType.None;
    [SerializeField] public ObjectInteractionType objectInteractionType = ObjectInteractionType.None;

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
        TileFloor tileFloor = TileControl.Instance.FLOORACTIONPAIR[floorInteractionType]?.Interaction(equipment);
        if (tileFloor != null)
            this.floorInteractionType = tileFloor.floorType;
        TileObject tileObject = TileControl.Instance.OBJECTACTIONPAIR[objectInteractionType]?.Interaction(equipment);
        if (tileObject != null)
            this.objectInteractionType = tileObject.objectType;
    }
}
