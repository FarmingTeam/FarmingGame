using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIQuickSlot : MonoBehaviour,IDropHandler
{
    public Equipment slotEquipment;
    UIToolBar toolbar;
    public DragDrop dragDropEquipment;

    private void OnEnable()
    {
        dragDropEquipment = GetComponentInChildren<DragDrop>();
        dragDropEquipment.SetDragDrop(this);
        toolbar=GetComponentInParent<UIToolBar>();
    }


    public void RefreshSlotEquipment() //이건 나중에 어디서 일괄처리해주는게 나을듯
    {
        //dragDropEquipment= GetComponentInChildren<DragDrop>();
        //slotEquipment= dragDropEquipment.equipment;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            toolbar.RefreshAllEquipmentSlots();

            
        }
        

        //만약 기존에 이 칸에 오브젝트가 있으면 자리를 바꿔준다.
        //이제 여기에 더불어서 데이터도 바꿔주는 작업이 필요

    }
}
