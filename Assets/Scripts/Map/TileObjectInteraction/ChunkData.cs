using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public enum ObjectInteractionType
{
    None = 0,
    Tree,
    Rock,
    House,
    Weed, 
    Branch

}

[System.Serializable]
public enum ObjectType
{
    None = 0,
    Stone = 10,
    Branch = 20,
    Treetype1 = 30,
    Treetype2 = 31,
    Weed = 40

}

[System.Serializable]
public enum SeedType
{
    None = 0,
    Carrot,
}

[System.Serializable]
public struct OneDimensionTileBase
{
    public TileBase[] Tiles;
}

[System.Serializable]
[CreateAssetMenu(fileName = "ChunkData", menuName = "TileInfo/ChunkData")]
public class ChunkData : ScriptableObject
{
    [SerializeField] public ObjectType objectType;
    [SerializeField] public ObjectInteractionType interactionType;
    [SerializeField] public OneDimensionTileBase[] tileBases;
}
