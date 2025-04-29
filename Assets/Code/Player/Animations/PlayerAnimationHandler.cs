using UnityEngine;

//This Script is handling Player Animations.
public class PlayerAnimationHandler : MonoBehaviour
{
    [Header("Components")]
    private Animator playerAnimator;

    [Header("Animation Parameters")]
    public bool isDancingEnded;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void SetMovementAnimation(float horizontalInput, float verticalInput)
    {
        float moveSpeed = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        playerAnimator.SetFloat("MoveSpeed", moveSpeed);
    }

    public void SetSprintingAnimation(bool isSprinting)
    {
        playerAnimator.SetBool("IsSprinting", isSprinting);
    }

    public void SetDanceAnimation(PlayerInputManager playerInputManager)
    {
        isDancingEnded = false;

        if (playerInputManager.isDancing && !isDancingEnded)
        {
            playerAnimator.SetBool("IsDancingEnded", isDancingEnded);
            playerAnimator.SetBool("IsDancing", true);
        }
        else
        {
            playerAnimator.SetBool("IsDancing", false);
        }
    }

    public void SetJumpAnimation(bool isJumping)
    {
        playerAnimator.SetBool("IsJumping", isJumping);
    }

    public void OnDanceAnimationEnd()
    {
        isDancingEnded = true;
        playerAnimator.SetBool("IsDancingEnded", isDancingEnded);
    }

    public void OnJumpAnimationEnd()
    {
        SetJumpAnimation(false);
    }
}
