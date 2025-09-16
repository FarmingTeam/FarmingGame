using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEquipment : MonoBehaviour
{
    public List<Equipment> equipList=new List<Equipment>();

   

    private void Start()
    {
        equipList.Add(ResourceManager.Instance.GetEquipment(1));
        equipList.Add(ResourceManager.Instance.GetEquipment(2));
        equipList.Add(ResourceManager.Instance.GetEquipment(3));
        equipList.Add(ResourceManager.Instance.GetEquipment(4));

        equipList.Add(ResourceManager.Instance.GetEquipment(1));

        equipList.Add(ResourceManager.Instance.GetEquipment(1));


    }

    
}
