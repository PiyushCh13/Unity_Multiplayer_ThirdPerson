using Photon.Pun;
using Photon.Voice.PUN;
using TMPro;
using UnityEngine;

//This Script is base class of the players call all functions at one place.


public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    protected Rigidbody playerRigidbody;
    public PhotonView photonView;

    public PhotonVoiceView photonVoiceView;

    [Header("Managers")]
    protected PlayerInputManager playerInputManager;
    protected PlayerAnimationHandler playerAnimationHandler;
    public CameraFollow cameraFollow;
    public PlayerVoiceHandler playerVoiceHandler;
    

    [Header("Online UI")]
    public TMP_Text playerNameText;

    protected virtual void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        photonView = GetComponent<PhotonView>();
        photonVoiceView = GetComponent<PhotonVoiceView>();
        playerVoiceHandler = GetComponent<PlayerVoiceHandler>();
    }

    private void Start()
    {
        if (photonView != null && photonView.Owner != null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }
        else
        {
            Debug.LogError("PhotonView or Owner is missing!");
        }
    }

    void Update()
    {
        playerInputManager.HandleInputs();
        HandleAnimation();
        playerVoiceHandler.PushToTalk(photonView, playerInputManager.pushToTalk);
    }

    void FixedUpdate()
    {
        HandlePhysics();
    }

    private void LateUpdate()
    {
        cameraFollow.PlayerFollow();
    }

    private void HandlePhysics()
    {
        HandleMovement();
        HandleRotation();
        HandleJump(playerAnimationHandler);
    }

    private void HandleAnimation()
    {
        playerAnimationHandler.SetMovementAnimation(playerInputManager.horizontalInput, playerInputManager.verticalInput);
        playerAnimationHandler.SetSprintingAnimation(playerInputManager.isSprinting);
        playerAnimationHandler.SetDanceAnimation(playerInputManager);
    }

    protected virtual void HandleMovement() { }
    protected virtual void HandleRotation() { }
    protected virtual void HandleJump(PlayerAnimationHandler playerAnimationHandler) { }
}
