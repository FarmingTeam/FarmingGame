using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] List<ItemData> datas = new List<ItemData>();
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
        }
    }

    
}
