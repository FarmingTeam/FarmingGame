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
            //item 드롭 로직


            tile.seed = new TileSeed();
            tileBase = null;
            return true;
        }
        tileBase = null;
        return false;
    }
}
