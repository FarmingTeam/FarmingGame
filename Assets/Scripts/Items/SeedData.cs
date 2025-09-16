using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class SeedData : ItemData
{
    public int growTime;

    public string seedTilePath;

    public TileBase seedTileBase;
    public TileBase cropTileBase;
    public SeedData(int itemID, string itemName, string itemDescription, string itemPath, string itemType, int maxNum, int growTime,string seedTilePath) : base(itemID, itemName, itemDescription, itemPath, itemType, maxNum)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemPath = itemPath;
        if (itemType == "Others")
        {
            this.itemType = ItemType.Others;
        }
        else if (itemType == "Potions")
        {
            this.itemType = ItemType.Potion;
        }
        else if (itemType == "Seed")
        {
            this.itemType = ItemType.Seed;
        }
        this.maxQuantity = maxNum;
        isStackable = maxQuantity > 1 ? true : false;
        this.growTime = growTime;
        this.seedTilePath = seedTilePath;

    }
    ///이 생성자는 처음에 씨앗 데이터를 로드할때 채워넣는것, 후에 위에있는 생성자로 병합예정
    
}
