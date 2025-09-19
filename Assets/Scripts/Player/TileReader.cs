using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReader : MonoBehaviour
{
    public Transform pivot; // 플레이어 중심점
    public GridLayout grid; // 그리드 가져오기

    public enum  Facing { Up, Down, Left, Right }

    public Facing defaultFacing = Facing.Down;
    public Facing currentFacing;

    private HashSet<string> obstacleNameSet;

    private void Awake()
    {
        currentFacing = defaultFacing;
    }

    public void SetFacing(Facing facing)
    {
        currentFacing = facing;
    }

    public Vector3Int CurrentCell => grid.WorldToCell(pivot.position);

    public Vector3Int FrontCell()
    {
        return CurrentCell + ToOffset(currentFacing);
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


    private void LogPosition()
    {
        var front = FrontCell();
        Debug.Log($"{CurrentCell} / {front} / {/*IsObstacleCell(front)*/ null}");
    }

}
