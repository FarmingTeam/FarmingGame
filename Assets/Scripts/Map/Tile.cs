using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Tile
{
    public Vector2Int pos;
    [SerializeField] public FloorInteractionType floorInteractionType = FloorInteractionType.None;
    [SerializeField] public ObjectInteractionType objectInteractionType = ObjectInteractionType.None;
    public bool canEnter = true;

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
}
