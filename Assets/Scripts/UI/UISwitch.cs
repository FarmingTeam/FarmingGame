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

    [SerializeField] private UIInventory uiInventory;

    private bool isOpen;
    private InputActionMap _uiMap;
    private InputActionMap _playerMap;

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

        if (playerInput && playerInput.actions)
        {
            _uiMap = playerInput.actions.FindActionMap(uiActionMap, false);
            _playerMap = playerInput.actions.FindActionMap(playerActionMap, false);

            if (_uiMap != null && !_uiMap.enabled) _uiMap.Enable();
        }

        if (!uiInventory)
            uiInventory = FindObjectOfType<UIInventory>(true);
    }

    private void FindInventory()
    {
        // 씬에서 직접 탐색(비활성 포함)
        uiInventory = FindObjectOfType<UIInventory>(true);
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

        FindInventory();

        if (uiInventory)
        {
            isOpen = uiInventory.gameObject.activeInHierarchy;
        }

        // 플레이어 액션맵만 끄고켜기
            if (_playerMap != null)
        {
            if (isOpen)
            {
                _playerMap.Disable();
            }
            else _playerMap.Enable();
        }

        // UI 액션맵은 항상 켜두기(혹시 꺼져있으면 복구)
        if (_uiMap != null && !_uiMap.enabled) _uiMap.Enable();

        // 플레이어 스크립트 동작 및 정지
        if (PlayerController)
        {
            if (isOpen)
            {
                if (PlayerController.rigidbody) PlayerController.rigidbody.velocity = Vector2.zero;
                PlayerController.enabled = false;
            }
            else
            {
                PlayerController.enabled = true;
            }
        }
    }

    // Inventory 인풋을 받아서 인벤토리 창을 끄고 킵니다.
    // 인벤토리 창이 켜져있는 동안 플레이어의 동작은 멈춥니다.

}
