using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUIManager : MonoBehaviour
{
    [SerializeField]
    private Button StartServerButton;

    [SerializeField]
    private Button StartHostButton;

    [SerializeField]
    private Button StartClientButton;

    [SerializeField]
    private TextMeshProUGUI PlayerInGameText;


    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        PlayerInGameText.text = $"Players in game: {PlayerRPC.Instance.PlayersInGame}";
    }

    private void Start()
    {
        StartServerButton.onClick.AddListener(() =>
        {
            if(NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Server Started...");
            }
            else
            {
                Debug.Log("Server Could Not Be Started...");
            }
        });

        StartHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Host Started...");
            }
            else
            {
                Debug.Log("Host Could Not Be Started...");
            }
        });

        StartClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client Started...");
            }
            else
            {
                Debug.Log("Client Could Not Be Started...");
            }
        });
    }
}
