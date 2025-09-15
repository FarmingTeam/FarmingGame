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
    public int currentDate; // 현재 게임 내 날짜인데 나중에 시간관리하는 곳에서 가져오도록 수정필요

    //Refactor : 임시 데이터
    TileBase seedTileBase;
    TileBase cropTileBase;


    public void UpdateRemainingDate()
    {
        if (!_isPlanted)
        {
            if (PlantedDate + GrowTime - currentDate <= 0)
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
}
