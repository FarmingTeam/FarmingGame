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

    public Animator animator;
    public enum  Facing { Up, Down, Left, Right }

    public Facing defaultFacing = Facing.Down;
    private Facing currentFacing;

    private void Awake()
    {
        currentFacing = defaultFacing;
    }

    private void Start()
    {
        InvokeRepeating(nameof(LogPosition), 0.5f, 0.5f);
        grid = MapControl.Instance.map.GetComponent<Grid>();
    }

    private void Update()
    {
        currentFacing = FacingFromAnimation();
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

    private Facing FacingFromAnimation()
    {
        if (!animator) return currentFacing;

        var infos = animator.IsInTransition(0)
            ? animator.GetNextAnimatorClipInfo(0)
            : animator.GetCurrentAnimatorClipInfo(0);

        if (infos == null || infos.Length == 0) return currentFacing;

        AnimationClip chosen = null;
        float best = -1f;
        for (int i = 0; i < infos.Length; i++)
        {
            if (infos[i].weight > best)
            {
                best = infos[i].weight;
                chosen = infos[i].clip;
            }
        }
        if (!chosen) return currentFacing;

        return NameToFacing(chosen.name, currentFacing);
    }

    private static Facing NameToFacing(string clipName, Facing defaultFacing)
    {
        // 클립명에 포함된 토큰으로 방향 결정
        if (ContainsToken(clipName, "Back")) return Facing.Up;
        if (ContainsToken(clipName, "Front")) return Facing.Down;
        if (ContainsToken(clipName, "Left")) return Facing.Left;
        if (ContainsToken(clipName, "Right")) return Facing.Right;

        // 토큰을 못 찾으면 이전 방향 유지
        return defaultFacing;
    }

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
        Debug.Log($"{CurrentCell} / {front}");
    }

}
