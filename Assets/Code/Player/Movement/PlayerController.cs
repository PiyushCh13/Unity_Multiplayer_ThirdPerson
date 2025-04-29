using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

//This Script is handling Player Physics.


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputManager))]
public class PlayerController : PlayerManager
{

    [Header("Movement Settings")]
    Vector3 movementDirection;
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float rotationSpeed;

    [Header("Jump Settings")]
    public bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float groundCheckDistance = 0.2f;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void HandleMovement()
    {
        if (playerInputManager != null)
        {
            movementDirection = cameraFollow.transform.forward * playerInputManager.verticalInput;
            movementDirection += cameraFollow.transform.right * playerInputManager.horizontalInput;

            movementDirection.Normalize();
            movementDirection.y = 0f;

            if (playerInputManager.isSprinting)
            {
                movementDirection *= sprintSpeed;
            }
            else
            {
                movementDirection *= moveSpeed;
            }


            Vector3 moveVelocity = movementDirection;
            moveVelocity.y = playerRigidbody.linearVelocity.y;

            playerRigidbody.linearVelocity = moveVelocity;
        }
    }

    protected override void HandleRotation()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        if (mouseDelta.x > 0.1f || mouseDelta.x < -0.1f)
        {
            float mouseX = mouseDelta.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, mouseX);
        }

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraFollow.transform.forward * playerInputManager.verticalInput;
        targetDirection = targetDirection + cameraFollow.transform.right * playerInputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0f;

        if (targetDirection == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    protected override void HandleJump(PlayerAnimationHandler playerAnimationHandler)
    {

        isGrounded = Physics.CheckSphere(transform.position + Vector3.up * 0.1f, groundCheckDistance, groundLayer);

        if (playerInputManager.isJumping && isGrounded)
        {
            playerAnimationHandler.SetJumpAnimation(true);
            playerRigidbody.linearVelocity = new Vector3(
                playerRigidbody.linearVelocity.x,
                0f,
                playerRigidbody.linearVelocity.z
            );

            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            playerInputManager.isJumping = false;
        }

        if (playerRigidbody.linearVelocity.y < 0)
        {
            playerRigidbody.linearVelocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
        }
    }

}
