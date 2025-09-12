using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISeedSlot : MonoBehaviour
{
    public Item slotSeedItem;
    Image image;
    TextMeshProUGUI quantityText;
   

    public void SetSeedSlot(Item item)
    {
        if(image==null)
        {
            image=GetComponent<Image>();
        }
        if(quantityText==null)
        {
            quantityText=GetComponentInChildren<TextMeshProUGUI>();
        }

        slotSeedItem = item;
        if(slotSeedItem != null)
        {
            image.sprite = item.itemData.itemIcon;
            quantityText.SetText(item.currentQuantity.ToString());
        }
       

    }
}
