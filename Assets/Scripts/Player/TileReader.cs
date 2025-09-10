using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReader : MonoBehaviour
{
    public Transform pivot; // 플레이어 중심점
    public GridLayout grid; // 그리드 가져오기
    public Tilemap tilemap; // 좌표 읽어올 타일맵

    public Vector3Int CurrentCell => grid.WorldToCell(pivot.position);

    public TileBase CurrentTile()
    {
        return tilemap.GetTile(CurrentCell);
    }

    // 현재 좌표 타일맵에서 끌어오는 역할
}
