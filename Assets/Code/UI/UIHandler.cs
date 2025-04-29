using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [Header("Game Object")]
    public GameObject mainmenuUI;
    public TMP_InputField playerNameInputField;

    [Header("Managers")]
    public ConnectionManager connectionManager;

    public void ClickToStart()
    {
        connectionManager.playerName = playerNameInputField.text;
        mainmenuUI.SetActive(false);
    }

}
