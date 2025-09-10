using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    PlayerInventory playerInventory;

    public List<UISlot> uISlots = new List<UISlot>();

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        uISlots = GetComponentsInChildren<UISlot>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
