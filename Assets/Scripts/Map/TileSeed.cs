using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSeed
{
    public int seedType = -1;
    private bool _isPlanted = false;
    public bool isPlanted { get { return _isPlanted; } }
    public bool isWatered = false;
    public int growTime = -1;
    public int plantedDate = -1;

    public ItemData itemData;

    //Refactor : 임시 데이터
    TileBase seedTileBase;
    TileBase cropTileBase;


    public void UpdateRemainingDate()
    {
        if (!_isPlanted)
        {
            if (plantedDate + growTime - TimeManager.Instance.GetActualUpdateDate() <= 0)
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
        seedType = seed.itemID;
        plantedDate =  TimeManager.Instance.GetActualUpdateDate();
        growTime = seed.growTime;
        seedTileBase = seed.seedTileBase;
        cropTileBase = seed.cropTileBase;
    }

    public bool Interaction(Equipment tool, Tile tile, out TileBase tileBase)
    {
        if (seedType == -1)
        {
            //씨앗 심기 로직
            if (tile.floorInteractionType == FloorInteractionType.Dirt && tool.equipmentType == EquipmentType.SeedBasket && tool.equipmentExtra != 0)
            {
                try
                {
                    SeedData seed = ResourceManager.Instance.GetItem(tool.equipmentExtra) as SeedData;
                    InitSeed(seed);
                    tileBase = SeedState();
                    return true;
                }
                catch
                {
                    Debug.LogError("올바르지 않은 씨앗 데이터입니다. : " + tool.equipmentExtra);
                    tileBase = null;
                    return false;
                }
            }
            tileBase = null;
            return false;
        }

        //씨앗 수확 로직
        //Refactor : 타입 낫 추가되는 대로 낫으로 변경
        else if (_isPlanted && tool.equipmentType == EquipmentType.Hoe)
        {
            // 드랍 오브젝트 생성(ItemData에서 작물 아이템의 데이터를 끌어와 타일 위에 띄우기)
            GameObject drop = new GameObject($"{itemData.itemName} Drop");

            // 드랍 아이템의 생김새는 아이콘과 같음.
            var spr = drop.AddComponent<SpriteRenderer>();
            spr.sprite = itemData.itemIcon;
            spr.sortingOrder = 10;

            tile.seed.ResetSeed();
            tileBase = null;
            return true;
        }

        //씨앗 파괴 로직
        else if (tool.equipmentType == EquipmentType.Pickaxe)
        {
            tile.seed.ResetSeed();
            tileBase = null;
            return true;
        }

        tileBase = null;
        return false;
    }

    void ResetSeed()
    {
        seedType = -1;
        _isPlanted = false;
        isWatered = false;
        growTime = -1;
        plantedDate = -1;
    }
}
