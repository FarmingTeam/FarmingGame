using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum FloorInteractionType
{
    None = 0,
    Dirt,
    WetDirt,
    Water,
    Tree,
    Rock
}

//레이어 1번
//타일 바닥 자체의 기능들
[System.Serializable]
[CreateAssetMenu(fileName = "TileFloor", menuName = "TileInfo/Floor")]
public class TileFloor : ScriptableObject
{
    public FloorInteractionType floorType;
    public TileBase tileBase;
    public Color tileColor;
    public TileFloorInteraction interaction;

    private void Awake()
    {
        switch (floorType)
        {
            default:
                return;
        }
    }
}
