using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private SocketIOGameData socketManager;

    private void Start()
    {
        // Tìm SocketIOGameData trong scene
        socketManager = FindObjectOfType<SocketIOGameData>();
        if (socketManager == null)
        {
            Debug.LogError("SocketIOGameData script is missing in the scene.");
        }
    }

    public void SaveGameDataToServer()
    {
        if (socketManager != null)
        {
            Debug.Log("Saving game data to server...");
            socketManager.SaveData(
                "player123",                     // playerId
                D3GameData.LoadCoin(),           // Coin
                D3GameData.LoadLife(),           // Life
                D3GameData.LoadHoveBoard(),      // HoveBoard
                D3GameData.LoadBestScore()       // BestScore
            );
        }
        else
        {
            Debug.LogError("SocketIOGameData is not initialized. Cannot save data to server.");
        }
    }

    public void LoadGameDataFromServer()
    {
        if (socketManager != null)
        {
            Debug.Log("Loading game data from server...");
            socketManager.LoadData("player123");
        }
        else
        {
            Debug.LogError("SocketIOGameData is not initialized. Cannot load data from server.");
        }
    }
}
