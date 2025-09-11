using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    UIToolBar toolBar;
    Image image;

    public UIQuickSlot quickSlot;
    public Equipment equipment;
    
    private void OnEnable()
    {
        toolBar = GetComponentInParent<UIToolBar>();
        
    }
    //기초 아이콘과 정보 세팅을 진행한다
    public void SetDragDrop(UIQuickSlot uIQuickSlot)
    {
        image = GetComponent<Image>();
        quickSlot = uIQuickSlot;
        if(quickSlot.slotEquipment != null )
        {
            image.sprite = quickSlot.slotEquipment.equipmentIcon;
            equipment = quickSlot.slotEquipment;
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(image.sprite ==null)
        {
            eventData.pointerDrag = null;
            return;
        }
        quickSlot=GetComponentInParent<UIQuickSlot>();

        transform.SetParent(toolBar.transform, false);
        transform.SetAsLastSibling();
        transform.position = eventData.position;

        image.raycastTarget = false;
        

    }

    public void OnDrag(PointerEventData eventData)
    {
       
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        var result = eventData.pointerCurrentRaycast;

        var dragDrop = result.gameObject.GetComponent<DragDrop>();
        if (result.gameObject==null)
        {
            transform.SetParent(quickSlot.transform);
            transform.localPosition = Vector3.zero;
        }
        else if ( dragDrop!= null)
        {

            //이 오브젝트랑 자리바꾸기(이미 데이터는 자리바꾸면서 자동으로 바꿔짐)
            result.gameObject.transform.SetParent(quickSlot.transform);  //기존에 그 슬롯에 있던건 이 슬롯에 자식으로 들어가서 자리세팅
            result.gameObject.transform.localPosition = Vector3.zero;
            quickSlot.slotEquipment = dragDrop.equipment;


        }
        else if (result.gameObject.GetComponent<DragDrop>() == null && result.gameObject.GetComponent<UIQuickSlot>() == null)
        {
            transform.SetParent(quickSlot.transform);
            transform.localPosition=Vector3.zero;
            
            
        }

        
        image.raycastTarget = true;

        

    }

    
}
