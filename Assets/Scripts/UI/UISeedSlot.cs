using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISeedSlot : MonoBehaviour,IPointerClickHandler
{
    [field:SerializeField] public SeedData SlotSeedItem { get; private set; }
    public Image image;
    TextMeshProUGUI quantityText;
    public Outline outline;
    UISeedBasket seedBasket;
   

    public void SetSeedSlot(SeedData seedData, int quantity)
    {
        if(image==null)
        {
            image=GetComponent<Image>();
        }
        if(quantityText==null)
        {
            quantityText=GetComponentInChildren<TextMeshProUGUI>();
        }

        SlotSeedItem = seedData;
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


    public void EmptyOutSlot()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        if (quantityText == null)
        {
            quantityText = GetComponentInChildren<TextMeshProUGUI>();
        }

        SlotSeedItem = null;
        image.sprite = null;
        quantityText.SetText(0.ToString());
    }

    private void OnEnable()
    {
        
        outline.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(seedBasket==null)
        {
            seedBasket=GetComponentInParent<UISeedBasket>();
        }
        if(SlotSeedItem!=null)
        {
            seedBasket.SelectSlot(this);
        }
        
    }
}
