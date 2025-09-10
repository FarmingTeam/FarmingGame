using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    //왼쪽 위 기준 좌표계 타일
    //TestingTile[,] testingTiles;
    [Header("타일맵")] 
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase testTile;
    public void Start()
    {
        tilemap.SetTile(Vector3Int.zero, testTile);
    }
}
