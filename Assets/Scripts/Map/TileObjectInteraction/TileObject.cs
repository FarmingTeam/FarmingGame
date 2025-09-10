using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum ObjectInteractionType
{
    None = 0,
    Tree,
    Rock
}
//레이어 2번
//타일 위에 올라가는 나무 씨앗 등 오브젝트
[System.Serializable]
[CreateAssetMenu(fileName = "TileObject", menuName = "TileInfo/Object")]
public class TileObject : ScriptableObject
{
    public ObjectInteractionType objectType;
    public TileBase tileBase;
    public bool isCollidable = true;
    public Color tileColor;
    public TileObjectInteraction interaction;

    private void Awake()
    {
        switch (objectType)
        {
            default:
                return;
        }
    }
}
