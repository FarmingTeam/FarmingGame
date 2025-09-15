using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceManager : Singleton<ResourceManager>
{
    
    Dictionary<int, ItemData> itemDataDic=new Dictionary<int,ItemData>();
    Dictionary<int, Equipment> equipmentDataDIc = new Dictionary<int, Equipment>();
    Dictionary<int,SeedData> seedDataDic = new Dictionary<int,SeedData>();
    protected override void Awake()
    {
        base.Awake();
        SetItem();
        SetEquipment();
    } 
    


    void SetItem()
    {
        TextAsset itemCSVText = Resources.Load<TextAsset>("ItemData/ItemDataCSV/ItemTestExcel");
        string[] rows = itemCSVText.text.Split('\n');
        for (int i = 1; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                return;
            }
            string[] columns = rows[i].Split(',');
            for (int j = 0; j < columns.Length; j++)
            {
                columns[j] = columns[j].Trim();
            }
            int ID = int.Parse(columns[0]);
            int maxNum=int.Parse(columns[5]);

            //미리 씨앗데이터나 기타 데이터들은 로드후에
            //여기에 딕셔너리에 이 아이템의 ID로 trygetvalue를 해보고
            //그게 가능하면 SeedData로 넣고 아니면 아이템데이터로 생성자 만드는 식으로
            ItemData itemData;
            if(seedDataDic.TryGetValue(ID, out var seedData))
            {
                itemData = new SeedData(ID, columns[1], columns[2], columns[3], columns[4], maxNum,seedData.growTime); //여기에 더 추가
            }
            else
            {
                itemData = new ItemData(ID, columns[1], columns[2], columns[3], columns[4], maxNum);
            }
                
            itemDataDic.Add(ID, itemData);


        }
    }


    public ItemData GetItem(int itemID)
    {
        if(itemDataDic.TryGetValue(itemID, out ItemData itemData))
        {
            itemData.itemIcon = Resources.Load<Sprite>($"ItemData/{itemData.itemPath}");
            return itemData;
        }
        else
        {
            Debug.Log("등록되지 않은 아이템");
            return null;
        }    
    }
    


    void SetEquipment()
    {
        TextAsset itemCSVText = Resources.Load<TextAsset>("EquipmentData/EquipmentDataCSV/EquipmentExcel");

        string[] rows = itemCSVText.text.Split('\n');
        for (int i = 1; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                return;
            }
            string[] columns = rows[i].Split(',');
            for (int j = 0; j < columns.Length; j++)
            {
                columns[j] = columns[j].Trim();
            }
            int ID = int.Parse(columns[0]);
            Equipment equipment = new Equipment(ID, columns[1], columns[2], columns[3], columns[4]);
            equipmentDataDIc.Add(ID, equipment);


        }
    }
    public Equipment GetEquipment(int equipmentID)
    {
        if (equipmentDataDIc.TryGetValue(equipmentID, out Equipment equipment))
        {
            equipment.equipmentIcon= Resources.Load < Sprite > ($"EquipmentData/{equipment.equipmentPath}"); //테스트용 추후 지울예정
            //equipment.equipmentIcon = Resources.Load<Sprite>($"ItemData/{equipment.equipmentPath}");
            return equipment;
        }
        else
        {
            Debug.Log("등록되지 않은 장비");
            return null;
        }
    }


    public void SetSeed()
    {
        TextAsset itemCSVText = Resources.Load<TextAsset>("EquipmentData/EquipmentDataCSV/EquipmentExcel");

        string[] rows = itemCSVText.text.Split('\n');
        for (int i = 1; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                return;
            }
            string[] columns = rows[i].Split(',');
            for (int j = 0; j < columns.Length; j++)
            {
                columns[j] = columns[j].Trim();
            }
            int ID = int.Parse(columns[0]);
            Equipment equipment = new Equipment(ID, columns[1], columns[2], columns[3], columns[4]);
            equipmentDataDIc.Add(ID, equipment);


        }
    }



}
