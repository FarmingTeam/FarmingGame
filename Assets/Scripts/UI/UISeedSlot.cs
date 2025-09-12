using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISeedSlot : MonoBehaviour
{
    public Item SlotSeedItem { get; private set; }
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
        Debug.Log(item);
        SlotSeedItem = item;
        if(SlotSeedItem != null)
        {
            image.sprite = item.itemData.itemIcon;
            quantityText.SetText(item.currentQuantity.ToString());
        }
        else
        {
            image.sprite = null;
            quantityText.SetText(0.ToString());
        }
       
    }


    public void RefreshSeedSlot()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        if (quantityText == null)
        {
            quantityText = GetComponentInChildren<TextMeshProUGUI>();
        }


        if (SlotSeedItem!=null&&SlotSeedItem.itemData!=null)
        {
            image.sprite= SlotSeedItem.itemData.itemIcon;
            quantityText.SetText(SlotSeedItem.currentQuantity.ToString());
        }
        else
        {
            image.sprite = null;
            quantityText.SetText(0.ToString());
        }
    }
}
