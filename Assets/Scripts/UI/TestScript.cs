using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUI<UIToolBar>();
        UIManager.Instance.OpenUI<UIInventory>();
    }

    
}
