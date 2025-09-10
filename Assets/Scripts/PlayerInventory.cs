using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemData> itemdatalist = new List<ItemData>();

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Additems()
        
    {
        ItemData itemData = ResourceManager.Instance.GetItem(1);
        ItemData itemData1 = ResourceManager.Instance.GetItem(2);

        itemdatalist.Add(itemData);
        itemdatalist.Add(itemData1);
    }
}
