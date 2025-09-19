using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    UIInventory uiInventory;

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI quantityText;

    GameObject slotDescriptionPanel;
    

    public SlotData SlotData { get; private set; } = null;

    public int slotIndex;

    GameObject dragObject;

    private void OnEnable()
    {
        uiInventory = GetComponentInParent<UIInventory>();
    }

    
    public void SetSlot(SlotData slotData)
    {
        
        this.SlotData = slotData;
        if(SlotData.slotItem==null)
        {
            image.sprite = null;
            quantityText.SetText("");
            return;
        }
        image.sprite=slotData.slotItem.itemData.itemIcon;
        quantityText.SetText(slotData.slotItem.currentQuantity.ToString());
        
    }

    

    public void OnPointerEnter(PointerEventData eventData)
    {

        if(SlotData!=null&& SlotData.slotItem!=null)
        {

            slotDescriptionPanel=uiInventory.DescriptionPanel;
            slotDescriptionPanel.SetActive(true);
            slotDescriptionPanel.transform.SetParent(transform,false);
            slotDescriptionPanel.transform.localPosition = new Vector3(150f, -250f, 0);
            TextMeshProUGUI text= slotDescriptionPanel.GetComponentInChildren<TextMeshProUGUI>();
            text.SetText(SlotData.slotItem.itemData.itemDescription);
        }


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(slotDescriptionPanel!=null)
        {
            slotDescriptionPanel.SetActive(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        if (SlotData == null||SlotData.slotItem == null)
        {
            return;
        }
        dragObject = new GameObject("DragObject",typeof(Image));
        Image dragImage = dragObject.GetComponent<Image>();
        dragImage.sprite=SlotData.slotItem.itemData.itemIcon;
        dragImage.raycastTarget=false;
        //일단 진짜 드래그처럼 보이기위한 꼼수로 슬롯 비어있는척
        image.sprite = null;
        quantityText.SetText("");

        //아이템 이미지의 복사본을 만들기 그리고 마우스 포인터 따로오게하기
        //레이캐스트 false로
        dragObject.transform.SetParent(uiInventory.transform,false);
        dragObject.transform.SetAsLastSibling();
        dragObject.transform.localPosition = Vector3.zero;
        dragObject.transform.position=eventData.position;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (SlotData == null||SlotData.slotItem == null)
        {
            return;
        }
        dragObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (SlotData == null||SlotData.slotItem == null)
        {
            return;
        }


        if(eventData.pointerCurrentRaycast.gameObject==null)
        {
            //그냥 결과가 null이면 볼것도 없이 다시 원상복구 세팅  (근데 만약 아예 바닥에 떨구기면 여기 로직 넣으면됨;
            uiInventory.StartSettingItem();
            Destroy(dragObject);
            return;
        }



        UISlot slot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UISlot>();
        if (slot!=null)
        {
            slot.ChangeItem(slotIndex);
            Destroy(dragObject);
            return;
        }
        else
        {
            //만약 쓰레기통에 떨구면(이건 그냥 쓰레기통측이서 처리해도...
            if(eventData.pointerCurrentRaycast.gameObject.GetComponent<UITrashCan>())
            {
                Debug.Log("쓰레기통 버리기");
                MapControl.Instance.player.inventory.EmptyOutSlot(SlotData);
            }
            Destroy(dragObject);
        }

        uiInventory.StartSettingItem();
        //슬롯인지 확인하기
        //레이캐스트 다시 true로
        //만약 슬롯이면 
    }



    //이 슬롯위로 떨궈질때 호출될 예정
    public void ChangeItem(int slotIdx)
    {
        MapControl.Instance.player.inventory.SwitchItemPlaces(slotIndex, slotIdx);
    }
   
}
