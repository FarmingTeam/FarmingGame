using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{

    [Header("Animation")]
    public Animator animator;

    [Header("Common")]
    public float moveSpeed = 2f; // 플레이어 이동속도

    public new Rigidbody2D rigidbody;

    Vector2 moveInput;

    private void Update()
    {

        AnimChange();
        
    }

    private void FixedUpdate()
    {
        OnMoving();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            moveInput = context.ReadValue<Vector2>(); // 이동 구현
        else if (context.phase == InputActionPhase.Canceled)
            moveInput = Vector2.zero;
    }

    private void OnMoving()
    {
        if (rigidbody != null)
        {
            Vector2 v = moveInput;
            if (v.sqrMagnitude > 1f) v = v.normalized; // 대각선 이동 구현
            rigidbody.velocity = v * moveSpeed;
        }
    }

    private void AnimChange()
    {
        animator.SetBool("IsMoveFront", moveInput.y < 0);
        animator.SetBool("IsMoveBack", moveInput.y > 0);
        animator.SetBool("IsMoveLeft", moveInput.x < 0);
        animator.SetBool("IsMoveRight", moveInput.x > 0);
    }

}
