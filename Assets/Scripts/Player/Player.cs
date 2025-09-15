using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] public Animator playerAnimator;
    [SerializeField] public TileReader tileReader;
    [SerializeField] public ToolPivot tool;
    [SerializeField] public PlayerInventory inventory;

    

    public void HandleQuickslot(int slot)
    {
        tool.SelectQuickslot(slot);
    }

    public void InitPos(Vector3 pos)
    {
        tileReader.grid = MapControl.Instance.map.GetComponent<Grid>();
        this.transform.position = pos;
    }

}
