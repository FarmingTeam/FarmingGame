using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    [Header("Animation")]
    public Animator animator;

    [Header("Common")]
    public float moveSpeed = 2f; // 플레이어 이동속도

    public new Rigidbody2D rigidbody;

    [SerializeField] private TileReader tileReader;
    [SerializeField] private Player player;

    Vector2 moveInput;

    private void Awake()
    {
        // 인스펙터에 비어 있으면 Player 컴포넌트에서 가져오기
        if (!player) player = GetComponentInParent<Player>();

        if (!tileReader)
        {
            var player = GetComponentInParent<Player>();
            tileReader = player ? player.tileReader : null;
        }
    }

    private void FixedUpdate()
    {
        OnMove();

    }

    private void Update()
    {

        AnimChange();
        UpdateFacingFromInput();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            moveInput = context.ReadValue<Vector2>(); // 이동 구현
        else if (context.phase == InputActionPhase.Canceled)
            moveInput = Vector2.zero;
    }

    public void OnInteractionAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MapControl.Instance.map.OnPlayerInteract((Vector2Int)player.tileReader.FrontCell(), player.tool.CurrentEquip.equipmentType); ;
            return;
        }
    }

    public void OnQuickSlot(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        int slot = ResolveQuickslotFromContext(context);
        if (slot < 1 || slot > 6) return;

        HandleQuickslot(slot);
    }

    private int ResolveQuickslotFromContext(InputAction.CallbackContext context)
    {
        var action = context.action;
        int binding = action?.GetBindingIndexForControl(context.control) ?? -1;
        if (binding >= 0 && binding < action.bindings.Count)
        {
            var number = action.bindings[binding].name;
            if (int.TryParse(number, out int slotByNumber)) return slotByNumber;
        }

        if (context.control is KeyControl key)
        {
            switch (key.keyCode)
            {
                case Key.Digit1: return 1;
                case Key.Digit2: return 2;
                case Key.Digit3: return 3;
                case Key.Digit4: return 4;
                case Key.Digit5: return 5;
                case Key.Digit6: return 6;
            }
        }

        return 0;
    }

    protected virtual void HandleQuickslot(int slot)
    {
        player.HandleQuickslot(slot);
    }

    private void OnMove()
    {
        if (rigidbody != null)
        {
            Vector2 v = moveInput;
            if (v.sqrMagnitude > 1f) v = v.normalized; // 대각선 이동 구현
            rigidbody.velocity = v * moveSpeed;
            //이동 전 위치
            //이동 후 위치
            //만약 이동 후 위치가 collisioncheck에 실패했다면
                //이동 전 위치로 조정
        }
    }

    private void CollisionCheck()
    {
        if (!MapControl.Instance.map.tiles[tileReader.CurrentCell.x, tileReader.CurrentCell.y].CanEnter())
        {
            //충돌 로직 처리
        }
    }

    private void AnimChange()
    {
        animator.SetBool("IsMoveFront", moveInput.y < 0);
        animator.SetBool("IsMoveBack", moveInput.y > 0);
        animator.SetBool("IsMoveLeft", moveInput.x < 0);
        animator.SetBool("IsMoveRight", moveInput.x > 0);
    }

    private void UpdateFacingFromInput()
    {
        if (!tileReader) return;

        // 입력이 없으면 마지막 방향 유지
        if (moveInput.sqrMagnitude == 0) return;

        TileReader.Facing facing;
        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            facing = moveInput.x > 0 ? TileReader.Facing.Right : TileReader.Facing.Left;
        else
            facing = moveInput.y > 0 ? TileReader.Facing.Up : TileReader.Facing.Down;

        tileReader.SetFacing(facing);
    }

}
