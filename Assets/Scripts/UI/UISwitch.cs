using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UISwitch : MonoBehaviour
{

    // 탭키 인풋 따로 참조
    public InputActionReference inventoryAction;
    public PlayerInput playerInput;

    public PlayerController PlayerController;

    private bool isOpen;

    private void Awake()
    {

        if (!PlayerController)
        {
            PlayerController = FindObjectOfType<PlayerController>();
        }

        if (!playerInput)
        {
            playerInput = GetComponentInParent<PlayerInput>();
        }
    }

    private void OnEnable()
    {
        // 인벤토리 활성화
        if (inventoryAction != null)
        {
            inventoryAction.action.performed += ActiveInventory;
        }
    }

    private void OnDisable()
    {
        // 인벤토리 비활성화
        if (inventoryAction != null)
        {
            inventoryAction.action.performed -= ActiveInventory;
        }
    }

    private void ActiveInventory(InputAction.CallbackContext ctx)
    {
        UIManager.Instance.ToggleUI<UIInventory>();
    }

   

    // Inventory 인풋을 받아서 인벤토리 창을 끄고 킵니다.
    // 인벤토리 창이 켜져있는 동안 플레이어의 동작은 멈춥니다.

}
