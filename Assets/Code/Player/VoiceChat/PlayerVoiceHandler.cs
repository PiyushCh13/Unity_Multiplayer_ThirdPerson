using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


//This Script is handling Player Voice.
public class PlayerVoiceHandler : MonoBehaviour
{
    private Recorder recorder;
    public Sprite micOnImage;
    public Sprite micOffImage;

    public Image micImage;

    void Start()
    {
        recorder = FindFirstObjectByType<Recorder>();
        micImage = GameObject.Find("Mic").GetComponent<Image>();
        micImage.sprite = micOffImage;
    }

    public void PushToTalk(PhotonView photonView, bool isPushingToTalk)
    {
        if (photonView.IsMine)
        {
            if (isPushingToTalk)
            {
                recorder.TransmitEnabled = true;
                micImage.sprite = micOnImage;
            }

            else
            {
                recorder.TransmitEnabled = false;
                micImage.sprite = micOffImage;
            }
        }
    }
}
