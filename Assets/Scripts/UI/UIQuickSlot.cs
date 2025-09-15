using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQuickSlot : MonoBehaviour,IDropHandler,IPointerClickHandler
{
    public Equipment slotEquipment;
    UIToolBar toolbar;
    public DragDrop dragDropEquipment;
    public Outline outline;
    public Image waterBar;

    

    public void Init()
    {
        dragDropEquipment = GetComponentInChildren<DragDrop>();
        dragDropEquipment.SetDragDrop(this);
        outline = GetComponent<Outline>();
        toolbar=GetComponentInParent<UIToolBar>();
    }



    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            slotEquipment = eventData.pointerDrag.GetComponent<DragDrop>().equipment;
            toolbar.RefreshSelectedSlotEquipment();
            
        }
        

        //만약 기존에 이 칸에 오브젝트가 있으면 자리를 바꿔준다.
        //이제 여기에 더불어서 데이터도 바꿔주는 작업이 필요

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        toolbar.SelectSlot(this);
    }
}
