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
    [SerializeField] public Tile[,] tiles = new Tile[10,10];

    private void Awake()
    {
        MapControl.Instance.map = this;
    }

    //Debug Update 
    //Remove Later
    //EX) tiles[가로위치, 세로로위치]
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tiles[3, 5].OnInteract(EquipmentType.Hoe);
            SetTileFloor(new Vector2Int(3,5));
            SetTileObject(new Vector2Int(3, 5));
        }
    }

    public void OnPlayerInteract(Vector2Int lookPos, Equipment tool)
    {

    }

    public void SetTileFloor(Vector2Int index)
    {
        TileFloor.SetTile((Vector3Int)index, TileControl.Instance.GetTileFloorByType(tiles[index.x, index.y].floorInteractionType).tileBase);
    }

    public void SetTileObject(Vector2Int index)
    {
        TileObject.SetTile((Vector3Int)index, TileControl.Instance.GetTileObjectByType(tiles[index.x, index.y].objectInteractionType).tileBase);
    }
}
