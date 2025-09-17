using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSeed
{
    public int SeedType = -1;
    private bool _isPlanted = false;
    public bool isPlanted { get { return _isPlanted; } }
    public bool isWatered = false;
    public int GrowTime = -1;
    public int PlantedDate = -1;

    public ItemData itemData;

    //Refactor : 임시 데이터
    TileBase seedTileBase;
    TileBase cropTileBase;


    public void UpdateRemainingDate()
    {
        if (!_isPlanted)
        {
            if (PlantedDate + GrowTime - TimeManager.Instance.GetActualUpdateDate() <= 0)
            {
                _isPlanted = true;
            }
        }
    }

    public TileBase SeedState()
    {
        UpdateRemainingDate();
        if (_isPlanted)
            return cropTileBase;
        return seedTileBase;
    }

    public void InitSeed(SeedData seed)
    {
        //Planted Date = Today
        GrowTime = seed.growTime;
        seedTileBase = seed.seedTileBase;
        cropTileBase = seed.cropTileBase;
    }

    public bool Interaction(EquipmentType tool, Tile tile, out TileBase tileBase)
    {
        if (SeedType == -1)
        {
            tileBase = null;
            return false;
        }

        if (_isPlanted)
        {
            // 드랍 오브젝트 생성(ItemData에서 작물 아이템의 데이터를 끌어와 타일 위에 띄우기)
            GameObject drop = new GameObject($"{itemData.itemName} Drop");

            // 드랍 아이템의 생김새는 아이콘과 같음.
            var spr = drop.AddComponent<SpriteRenderer>();
            spr.sprite = itemData.itemIcon;
            spr.sortingOrder = 10;

            tile.seed = new TileSeed();
            tileBase = null;
            return true;
        }
        tileBase = null;
        return false;
    }
}
