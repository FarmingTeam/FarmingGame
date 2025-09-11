using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [Header("타일맵 0번 레이어")]
    [SerializeField] Tilemap baseGroundGrass;
    [SerializeField] Tilemap baseGroundWater;
    [SerializeField] Tilemap baseGroundWall;

    [Header("타일맵 1번 레이어")]
    [SerializeField] Tilemap TileFloor;
    [Header("타일맵 2번 레이어")]
    [SerializeField] Tilemap TileObject;

    [Header("타일 정보")]
    [SerializeField] Tile[,] tiles = new Tile[10,10];

    private void Awake()
    {
        MapControl.Instance.map = this;
    }
    private void Start()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                tiles[i, j] = new Tile();
                SetInteractionTile(new Vector2Int(i, j));
            }
    }

    //Debug Update 
    //Remove Later
    //EX) tiles[가로위치, 세로로위치]
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tiles[3, 5].OnInteract(0);
            SetInteractionTile(new Vector2Int(3,5));
        }
    }

    public void OnPlayerInteract(Vector2Int lookPos, Equipment tool)
    {

    }


    public void SetInteractionTile(Vector2Int index)
    {
        TileFloor.SetTile((Vector3Int)index, TileControl.Instance.GetTileFloorByType(tiles[index.x, index.y].floorInteractionType).tileBase);
        TileObject.SetTile((Vector3Int)index, TileControl.Instance.GetTileObjectByType(tiles[index.x, index.y].objectInteractionType).tileBase);
    }
}
