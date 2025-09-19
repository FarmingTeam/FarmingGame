using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public enum ChunkInteractionType
{
    None = 0,
    Tree,
    Rock,
    Weed, 
    Branch,
    Door
}

[System.Serializable]
public enum ChunkType
{
    None = 0,
    Stone = 10,
    Branch = 20,
    Treetype1 = 30,
    Treetype2 = 31,
    Weed = 40,
    FarmDoor = 50,
    HouseDoor = 51,
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
    [SerializeField] public ChunkType chunkType;
    [SerializeField] public ChunkInteractionType interactionType;
    [SerializeField] public OneDimensionTileBase[] tileBases;
}
