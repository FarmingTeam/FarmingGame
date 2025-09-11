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

    public void Awake()
    {
        MapControl.Instance.player = this;
    }
}
