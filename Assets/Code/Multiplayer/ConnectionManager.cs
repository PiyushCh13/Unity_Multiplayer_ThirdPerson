using System.Collections;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    [Header("Player Settings")]
    public string playerName;

    [Header("Photon Settings")]
    public PhotonView photonViewComponent;

    [Header("UI Settings")]
    public GameObject lookingforPlayerUI;
    public GameObject inGameUI;

    public GameObject startGameObject;

    public Sprite connectionEstablishedImage;
    public Sprite connectionFailedImage;

    public Image connectionStatusImage;


    public void StartConnection()
    {
        PhotonNetwork.ConnectUsingSettings();                            // Connect to Photon server
        lookingforPlayerUI.SetActive(true);                              // Show looking for player UI
        connectionStatusImage.sprite = connectionFailedImage;            // Set connection status image to failed
        photonViewComponent = GetComponent<PhotonView>();                // Get the PhotonView component

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();                     // Join a random room
        PhotonNetwork.NickName = playerName;                //Set player name
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Game_Room_" + Random.Range(1000, 9999);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 2 };

        PhotonNetwork.CreateRoom(roomName, roomOptions);            // Create a new room if joining a random room fails
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.SendRate = 30;                                       // Set the send rate for PhotonNetwork
        PhotonNetwork.SerializationRate = 20;                             // Set the serialization rate for PhotonNetwork
        connectionStatusImage.sprite = connectionEstablishedImage;       // Set connection status image to established


        Vector3 randomSpawnPosition = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        GameObject character = PhotonNetwork.Instantiate
        ("Character", randomSpawnPosition, Quaternion.identity, 0);    // Instantiate the player character at a random position

        inGameUI.SetActive(true);
      
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            lookingforPlayerUI.SetActive(false);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            lookingforPlayerUI.SetActive(false);                           // Hide looking for player UI
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        startGameObject.SetActive(true);                    // Show start game object when a player leaves the room
        lookingforPlayerUI.SetActive(false);                // Hide looking for player UI
        inGameUI.SetActive(false);                         // Hide in-game UI
        connectionStatusImage.sprite = connectionFailedImage;         // Set connection status image to failed
    }
}
