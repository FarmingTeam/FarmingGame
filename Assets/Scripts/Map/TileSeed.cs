using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSeed : MonoBehaviour
{
    public int SeedType;
    private bool _isPlanted = false;
    public bool isPlanted { get { return _isPlanted; } }
    public bool isWatered = false;
    public int GrowTime;
    public int PlantedDate;
    public int RemainingDate;
    public int currentDate; // 현재 게임 내 날짜인데 나중에 시간관리하는 곳에서 가져오도록 수정필요

    public void UpdateRemainingDate()
    {
        if (_isPlanted)
        {
            RemainingDate = PlantedDate + GrowTime - currentDate;
            if (RemainingDate <= 0)
            {
                RemainingDate = 0;
                _isPlanted = false;
            }
        }
    }
    public void InitSeed()
    {
    }
}
