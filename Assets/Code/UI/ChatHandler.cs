using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

//This Script is handling Player Chat.

public class ChatHandler : MonoBehaviour
{
    public TMP_InputField chatInputField;
    public GameObject messagePrefab;

    public GameObject contentPanel;

    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("ReceiveMessage", RpcTarget.All, chatInputField.text);
    }

    [PunRPC]
    public void ReceiveMessage(string message, PhotonMessageInfo info)
    {
        GameObject newMessage = Instantiate(messagePrefab, Vector2.zero, Quaternion.identity, contentPanel.transform);
        newMessage.GetComponent<ChatMessage>().myMessageText.text = info.Sender.NickName + ": " + message;
    }

}
