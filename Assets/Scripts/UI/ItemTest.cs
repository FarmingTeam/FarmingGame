using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTest : MonoBehaviour
{
    [SerializeField] Button Button;
    [SerializeField] Button button2;
    [SerializeField] Button button3;

    // Start is called before the first frame update
    /*void Start()
    {
        UIManager.Instance.OpenUI<UIToolBar>();
        UIManager.Instance.OpenUI<UIInventory>();
        //UIManager.Instance.OpenUI<UISeedBasket>();
    }*/


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            MapControl.Instance.player.inventory.AdditemsByID(1001, 1);
            MapControl.Instance.player.inventory.AdditemsByID(2001, 6);
            MapControl.Instance.player.inventory.AdditemsByID(1002, 1);
            MapControl.Instance.player.inventory.AdditemsByID(2002, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            MapControl.Instance.player.inventory.SaveInventoryStatus();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            MapControl.Instance.player.inventory.LoadInventoryStatus();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            UIManager.Instance.ToggleUI<UIInventory>();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            UIManager.Instance.OpenUI<UIToolBar>();
        }
    }
    

    public void Test2()
    {

    }

    public void Test3()
    {
        UIManager.Instance.CloseUI<UISeedBasket>();
    }
    
}
