using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQuickSlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
{
    
    public Equipment slotEquipment;
    public int slotNumber;
    [SerializeField] TextMeshProUGUI slotNumberText;
    [SerializeField] Image image;

    UIToolBar toolbar;

    public Outline outline;
    public Image waterBar;

     //이건 기존 이미지
    GameObject dragObject;


    public void Init()
    {

        slotNumberText.SetText(slotNumber.ToString());
        
    }
    public void SetQuickSlot(Equipment equipment)
    {
        image.sprite=equipment.equipmentIcon;
        slotEquipment=equipment;
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        MapControl.Instance.player.tool.SelectQuickslot(slotNumber);
        Debug.Log("클릭");
    }




    

    private void OnEnable()
    {
        toolbar= GetComponentInParent<UIToolBar>();
    }




    public void OnBeginDrag(PointerEventData eventData)
    {

        if (slotEquipment == null)
        {
            return;
        }
        dragObject = new GameObject("DragObject", typeof(Image));
        Image dragImage = dragObject.GetComponent<Image>();
        dragImage.sprite = slotEquipment.equipmentIcon;
        dragImage.raycastTarget = false;
        //일단 진짜 드래그처럼 보이기위한 꼼수로 슬롯 비어있는척
        image.gameObject.SetActive(false);


        //아이템 이미지의 복사본을 만들기 그리고 마우스 포인터 따로오게하기
        //레이캐스트 false로
        dragObject.transform.SetParent(toolbar.transform, false);
        dragObject.transform.SetAsLastSibling();
        dragObject.transform.localPosition = Vector3.zero;
        dragObject.transform.position = eventData.position;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (slotEquipment == null)
        {
            return;
        }
        dragObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slotEquipment == null)
        {
            return;
        }
        image.gameObject.SetActive(true);

        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            //그냥 결과가 null이면 볼것도 없이 다시 원상복구 세팅  (근데 만약 아예 바닥에 떨구기면 여기 로직 넣으면됨;
            //장비 동가화
            Destroy(dragObject);
            return;
        }

        UIQuickSlot slot = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<UIQuickSlot>();
        Debug.Log(slotNumber);
        if (slot != null)
        {
            slot.ChangeItem(slotNumber-1);
            Destroy(dragObject);
            return;
        }
        else
        {
            
            Destroy(dragObject);
        }

        
        //동기화
        //슬롯인지 확인하기
        //레이캐스트 다시 true로
        //만약 슬롯이면 
    }



    //이 슬롯위로 떨궈질때 호출될 예정
    public void ChangeItem(int slotIdx)
    {
        Debug.Log("아이템바꾸기");

        MapControl.Instance.player.equipment.SwitchEquipmentPlaces(slotNumber-1,slotIdx);
    }

}









    
