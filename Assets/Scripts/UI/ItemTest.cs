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
    void Start()
    {
        UIManager.Instance.OpenUI<UIToolBar>();
        UIManager.Instance.OpenUI<UIInventory>();
        UIManager.Instance.OpenUI<UISeedBasket>();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            MapControl.Instance.player.inventory.AdditemsByID(1, 1);
            MapControl.Instance.player.inventory.AdditemsByID(2, 10);
            MapControl.Instance.player.inventory.AdditemsByID(3, 1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            MapControl.Instance.player.inventory.SwitchItemPlaces(5, 0);
        }
        if( Input.GetKeyDown(KeyCode.Alpha9))
        {
            MapControl.Instance.player.inventory.SubtractItemQuantity(1, 1);
        }
    }
    public void Test()
    {
        
    }

    public void Test2()
    {
        UIManager.Instance.OpenUI<UISeedBasket>();
    }

    public void Test3()
    {
        UIManager.Instance.CloseUI<UISeedBasket>();
    }
    
}
