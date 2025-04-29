using UnityEngine;
using UnityEngine.InputSystem;

//This Script is handling Player Inputs.

public class PlayerInputManager : MonoBehaviour
{

    public Vector2 moveInput { get; private set; }
    public float horizontalInput;
    public float verticalInput;

    public bool isJumping;
    public bool isSprinting { get; private set; }
    public bool isDancing { get; private set; }

    public bool pushToTalk { get; private set; }



    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    public void HandleInputs()
    {
        horizontalInput = moveInput.x;
        verticalInput = moveInput.y;
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJumping = true;
        }
        else if (context.canceled)
        {

        }
    }
    public void HandleSprintInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }

    public void HandleDanceInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDancing = true;
        }
        else if (context.canceled)
        {
            isDancing = false;
        }
    }

    public void PushToTalk(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pushToTalk = true;
        }
        else if (context.canceled)
        {
            pushToTalk = false;
        }

    }
}
