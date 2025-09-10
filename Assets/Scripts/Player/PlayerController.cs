using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{
    [Header("Input System")] // 인풋시스템 연결
    public InputActionReference moveAction;
    public InputActionReference interactAction;

    [Header("Animation")]
    public Animator animator;

    [Header("Common")]
    public float moveSpeed = 2f; // 플레이어 이동속도

    public Rigidbody2D rigidbody;

    Vector2 moveInput;

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
    }

    private void Update()
    {
        if (moveAction == null) return;
        moveInput = moveAction.action.ReadValue<Vector2>(); // 이동 구현

        animator.SetBool("IsMoveFront", moveInput.y < 0);
        animator.SetBool("IsMoveBack", moveInput.y > 0);
        animator.SetBool("IsMoveLeft", moveInput.x < 0);
        animator.SetBool("IsMoveRight", moveInput.x > 0);
    }

    private void FixedUpdate()
    {
        if (rigidbody != null)
        {
            Vector2 v = moveInput;
            if (v.sqrMagnitude > 1f) v = v.normalized; // 대각선 이동 구현
            rigidbody.velocity = v * moveSpeed;
        }
    }

}
