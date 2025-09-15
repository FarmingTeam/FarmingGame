using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReader : MonoBehaviour
{
    public Transform pivot; // 플레이어 중심점
    public GridLayout grid; // 그리드 가져오기
    public Tilemap tilemap; // 좌표 읽어올 타일맵

    public enum  Facing { Up, Down, Left, Right }

    public Facing defaultFacing = Facing.Down;
    private Facing currentFacing;

    // 타일 충돌 읽기 초안
    [SerializeField]
    private string[] obstacleTileNames =
        { "TileObject", "BaseGround_Water", "BaseGround_Wall" };

    private HashSet<string> obstacleNameSet;

    private void Awake()
    {
        currentFacing = defaultFacing;
        obstacleNameSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (obstacleTileNames != null)
        {
            foreach (var n in obstacleTileNames)
            {
                if (!string.IsNullOrWhiteSpace(n))
                    obstacleNameSet.Add(n.Trim());
            }
        }
    }

    private void Start()
    {
        grid = MapControl.Instance.map.GetComponent<Grid>();
        //InvokeRepeating(nameof(LogPosition), 0.5f, 0.5f);
    }

    public void SetFacing(Facing facing)
    {
        currentFacing = facing;
    }

    public Vector3Int CurrentCell => grid.WorldToCell(pivot.position);

    public TileBase CurrentTile()
    {
        return tilemap.GetTile(CurrentCell);
    }

    public Vector3Int FrontCell()
    {
        return CurrentCell + ToOffset(currentFacing);
    }

    public TileBase FrontTile()
    {
        return tilemap.GetTile(FrontCell());
    }

    /*
    public bool IsObstacleCell(Vector3Int cell)
    {
        var t = tilemap.GetTile(cell);
        if (!t) return false; // 타일이 없으면 통과 가능
        // 타일 에셋 이름으로 판정
        return obstacleNameSet.Contains(t.name);
    }
    */

    private static bool ContainsToken(string s, string token)
        => s?.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0;

    private static Vector3Int ToOffset(Facing f)
    {
        switch (f)
        {
            case Facing.Up: return new Vector3Int(0, 1, 0);
            case Facing.Down: return new Vector3Int(0, -1, 0);
            case Facing.Left: return new Vector3Int(-1, 0, 0);
            case Facing.Right: return new Vector3Int(1, 0, 0);
            default: return Vector3Int.zero;
        }
    }

    // 씬 로드 할때 집어넣기
    public void ReadTilemap()
    {
        // pivot이 없을 경우 스스로 지정
        if (!pivot) pivot = transform;

        if (!grid)
        {
            var mapRoot = MapControl.Instance ? MapControl.Instance.map : null;
            if (mapRoot)
                grid = mapRoot.GetComponent<GridLayout>() ?? mapRoot.GetComponentInChildren<GridLayout>(true);

        }

        if (!tilemap)
        {
            tilemap.GetComponent<Tilemap>();
            if (!grid)
                grid = tilemap.GetComponentInParent<GridLayout>()
                    ?? tilemap.GetComponent<GridLayout>()
                    ?? tilemap.GetComponentInChildren<GridLayout>(true);
        }
        
    }


    private void LogPosition()
    {
        var front = FrontCell();
        Debug.Log($"{CurrentCell} / {front} / {/*IsObstacleCell(front)*/ null}");
    }

}
