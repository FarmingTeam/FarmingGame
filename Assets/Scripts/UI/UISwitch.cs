using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UISwitch : MonoBehaviour
{

    // 탭키 인풋 따로 참조
    public InputActionReference inventoryAction;
    public PlayerInput playerInput;

    // 액션맵 생성
    public string uiActionMap = "UI";
    public string playerActionMap = "Player";

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
        // 인벤토리 활성화
        if (inventoryAction != null)
        {
            inventoryAction.action.performed -= ActiveInventory;
        }
    }

    private void ActiveInventory(InputAction.CallbackContext ctx)
    {
        UIManager.Instance.ToggleUI<UIInventory>();
    }

    public void Toggle()
    {
        UIManager.Instance.OpenPopup<UIInventory>();

        if (playerInput)
        {
            if (HasActionMap(uiActionMap))
                playerInput.SwitchCurrentActionMap(uiActionMap);
            else if (HasActionMap(playerActionMap))
                playerInput.SwitchCurrentActionMap(playerActionMap);
        }
    }

    private bool HasActionMap(string mapName)
    {
        if (string.IsNullOrEmpty(mapName) || playerInput == null || playerInput.actions == null)
            return false;

        foreach (var map in playerInput.actions.actionMaps)
            if (map.name == mapName) return true;

        return false;
    }

    // Inventory 인풋을 받아서 인벤토리 창을 끄고 킵니다.
    // 인벤토리 창이 켜져있는 동안 플레이어의 동작은 멈춥니다.

}
