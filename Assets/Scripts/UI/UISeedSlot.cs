using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISeedSlot : MonoBehaviour
{
    public ItemData SlotSeedItem { get; private set; }
    Image image;
    TextMeshProUGUI quantityText;
   

    public void SetSeedSlot(ItemData itemData, int quantity)
    {
        if(image==null)
        {
            image=GetComponent<Image>();
        }
        if(quantityText==null)
        {
            quantityText=GetComponentInChildren<TextMeshProUGUI>();
        }

        SlotSeedItem = itemData;
        if(SlotSeedItem != null)
        {
            image.sprite = SlotSeedItem.itemIcon;
            quantityText.SetText(quantity.ToString());
        }
        else
        {
            image.sprite = null;
            quantityText.SetText(0.ToString());
        }
       
    }


 
}
