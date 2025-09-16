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
        if (itemData == null) return;

        // 타일에 작물이 있는지를 판정
        // var seed = itemData as tileSeed.SeedType;
        // if (seed == null) return;

        // 성장이 되었는가를 판정
        // if (tileBase != seed.cropTileBase) return;

        Vector2 dropPos = transform.position;
        var tilemap = GetComponentInParent<Tilemap>();
        if (tilemap != null)
        {
            Vector2Int cell = (Vector2Int)tilemap.WorldToCell(transform.position);
            dropPos = tilemap.GetCellCenterWorld((Vector3Int)cell);
        }

        // 드랍 오브젝트 생성(ItemData에서 작물 아이템의 데이터를 끌어와 타일 위에 띄우기)
        GameObject drop = new GameObject($"{itemData.itemName} Drop");
        drop.transform.position = dropPos;

        // 드랍 아이템의 생김새는 아이콘과 같음.
        var spr = drop.AddComponent<SpriteRenderer>();
        spr.sprite = itemData.itemIcon;
        spr.sortingOrder = 10;

        // 드랍 데이터
        // var data = drop.AddComponent<DroppedItem>();
        // data.Init(itemData, 1);
    }

}
