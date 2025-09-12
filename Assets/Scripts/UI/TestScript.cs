using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
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

    public void Test()
    {
        MapControl.Instance.player.inventory.AdditemsByID(1);
        MapControl.Instance.player.inventory.AdditemsByID(2);
        MapControl.Instance.player.inventory.AdditemsByID(3);
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
