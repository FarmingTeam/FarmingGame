using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    UIInventory uiInventory;

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI quantityText;

    GameObject slotDescriptionPanel;
    public Item SlotItemData{ get; private set; }

    private void OnEnable()
    {
        uiInventory = GetComponentInParent<UIInventory>();
    }
    public void SetSlot(Item runtimeItemData)
    {
        
        SlotItemData = runtimeItemData;
        image.sprite=runtimeItemData.itemData.itemIcon;
        quantityText.SetText(runtimeItemData.currentQuantity.ToString());
        
    }

    public void RefreshSlot()
    {
        if(SlotItemData!=null)
        {
            image.sprite = SlotItemData.itemData.itemIcon;
            quantityText.SetText(SlotItemData.currentQuantity.ToString());
        }
        else
        {
            image.sprite = null;
            quantityText.SetText("0");
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("마우스 올림");
        if(SlotItemData!=null)
        {
            slotDescriptionPanel=uiInventory.DescriptionPanel;
            slotDescriptionPanel.SetActive(true);
            slotDescriptionPanel.transform.SetParent(transform,false);
            slotDescriptionPanel.transform.localPosition = new Vector3(150f, -250f, 0);
            TextMeshProUGUI text= slotDescriptionPanel.GetComponentInChildren<TextMeshProUGUI>();
            text.SetText(SlotItemData.itemData.itemDescription);
        }


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(slotDescriptionPanel!=null)
        {
            slotDescriptionPanel.SetActive(false);
        }
    }
}
