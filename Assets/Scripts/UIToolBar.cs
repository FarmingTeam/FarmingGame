using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIToolBar : MonoBehaviour
{
    [SerializeField] GameObject uiQuickSlotPrefab;
    List<UIQuickSlot> quickSlotList=new List<UIQuickSlot>();
    List<Equipment> equipmentList=new List<Equipment>();
    private void Start()
    {
        equipmentList.Add(ResourceManager.Instance.GetEquipment(1));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        equipmentList.Add(ResourceManager.Instance.GetEquipment(2));
        //여기서 슬롯칸을 동적생성할예정(아직 안함)
        for (int i=0; i<6;  i++)
        {
            GameObject go= Instantiate(uiQuickSlotPrefab,this.transform,false);
            go.GetComponentInChildren<TextMeshProUGUI>().SetText((i+1).ToString());
        }

        quickSlotList = GetComponentsInChildren<UIQuickSlot>().ToList();

        //여기에서 슬롯에 아이템 넣어주기
        for(int i = 0; i < quickSlotList.Count; i++)
        {
            quickSlotList[i].slotEquipment = equipmentList[i];
            quickSlotList[i].dragDropEquipment.SetDragDrop(quickSlotList[i]);

        }
    }

    
}
