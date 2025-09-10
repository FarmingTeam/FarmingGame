using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] List<ItemData> datas = new List<ItemData>();
    Dictionary<int, ItemData> itemDataDic=new Dictionary<int,ItemData>();
    void Start()
    {
        TextAsset itemCSVText = Resources.Load<TextAsset>("ItemData/ItemDataCSV/ItemTestCSV");
        string[] rows= itemCSVText.text.Split('\n');
        for(int i=1; i<rows.Length; i++)
        {
            if(string.IsNullOrEmpty(rows[i]))
            {
                return;
            }
            string[] columns= rows[i].Split(',');
            for(int j=0; j<columns.Length; j++)
            {
                columns[j] = columns[j].Trim();
            }
            int ID = int.Parse(columns[0]);
            ItemData itemData=new ItemData(ID, columns[1], columns[2], columns[3], columns[4]);
            datas.Add(itemData);
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
    

}
