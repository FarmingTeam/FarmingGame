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

    public TileBase NearTile()
    {
        // 주변 셀들의 좌표를 생성 및 갱신(상하좌우 1칸)
        Vector3Int c = CurrentCell;
        var neartiles = new Vector3Int[]
        {
            new Vector3Int(0, 1),
            new Vector3Int(0, -1),
            new Vector3Int(-1, 0),
            new Vector3Int(1, 0)
        };

        foreach (var off in neartiles)
        {
            var cell = c + off;
            var t = tilemap.GetTile(cell);
            if (t != null) return t;
        }

        return null;
    }

    private void Start()
    {
        InvokeRepeating(nameof(LogPosition), 0.5f, 0.5f);
    }

    private void LogPosition()
    {
        Debug.Log($"{CurrentCell}");
    }

}
