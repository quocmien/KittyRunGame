using UnityEngine;
using SocketIOClient;
using System.Threading.Tasks;

public class SocketIOGameData : MonoBehaviour
{
    private SocketIO client;
    private bool isConnected = false;

    private async void Start()
    {
        client = new SocketIO("http://localhost:3000");

        client.OnConnected += (sender, e) =>
        {
            isConnected = true;
            Debug.Log("Connected to Socket.IO server.");
        };

        client.On("saveDataResponse", response =>
        {
            Debug.Log("Save Data Response: " + response);
        });

        client.On("loadDataResponse", response =>
        {
            Debug.Log("Raw Response: " + response.ToString());
        });

        await client.ConnectAsync();
    }

    public async void SaveData(string playerId, int coin, int life, int hoveBoard, int bestScore)
    {
        if (!isConnected) return;

        var data = new PlayerData
        {
            playerId = playerId,
            Coin = coin,
            Life = life,
            HoveBoard = hoveBoard,
            BestScore = bestScore
        };

        await client.EmitAsync("saveData", data);
    }

    public async void LoadData(string playerId)
    {
        if (!isConnected) return;
        await client.EmitAsync("loadData", playerId);
    }
}

[System.Serializable]
public class PlayerData
{
    public string playerId;
    public int Coin;
    public int Life;
    public int HoveBoard;
    public int BestScore;
}
