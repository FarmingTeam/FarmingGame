using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemDropper : MonoBehaviour
{
    public ItemData itemData;
    public TileBase tileBase;
    public TileSeed tileSeed;

    public void ItemDropping()
    {
        if (itemData == null || tileSeed == null) return;

        //타일의 정보 얻기
        Vector2 dropPos = transform.position;
        Vector2Int cell = default;
        var tilemap = GetComponentInParent<Tilemap>();
        if (tilemap != null)
        {
            cell = (Vector2Int)tilemap.WorldToCell(transform.position);
            dropPos = tilemap.GetCellCenterWorld((Vector3Int)cell);
        }
        TileBase currentTile = tileBase;
        if (currentTile == null && tilemap != null)
            currentTile = tilemap.GetTile((Vector3Int)cell);

        // 수확가능 시기인지 확인
        // if (currentTile != tileSeed.cropTileBase) return;
        // cropTileBase 퍼블릭으로 바꿔주신 후 주석 해제해주시면 됩니다.

        // 드랍 오브젝트 생성(ItemData에서 작물 아이템의 데이터를 끌어와 타일 위에 띄우기)
        GameObject drop = new GameObject($"{itemData.itemName} Drop");
        drop.transform.position = dropPos;

        // 드랍 아이템의 생김새는 아이콘과 같음.
        var spr = drop.AddComponent<SpriteRenderer>();
        spr.sprite = itemData.itemIcon;
        spr.sortingOrder = 10;

        // 드랍 데이터
        var data = drop.AddComponent<DroppedItem>();
        data.Init(itemData, 1);
    }

}
