using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileInteractionType
{
    None = 0,
    Dirt,
    WetDirt,
    Water,
    Tree,
    Rock
}


[System.Serializable]
[CreateAssetMenu(fileName = "TileInfo", menuName = "ScriptableObject/TileInfo")]
public class TileState : ScriptableObject
{
    public TileInteractionType tileType;
    public TileBase tileBase;
    public bool isCollidable;
    public Color tileColor;
    public ITileInteraction interaction;

    private void Awake()
    {
        switch (tileType)
        {
            default:
                return;
        }
    }
}
