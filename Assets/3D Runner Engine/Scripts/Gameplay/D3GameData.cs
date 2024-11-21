using UnityEngine;

public static class D3GameData
{
    private static GameDataManager gameDataManager;

    // Khởi tạo tĩnh để đảm bảo gameDataManager được gán khi class được sử dụng lần đầu
    static D3GameData()
    {
        gameDataManager = Object.FindObjectOfType<GameDataManager>();
        if (gameDataManager == null)
        {
            Debug.LogError("GameDataManager script is missing in the scene.");
        }
        else
        {
            Debug.Log("GameDataManager initialized successfully.");
        }
    }

    public static void SaveCoin(int coin)
    {
        PlayerPrefs.SetInt("Coin", coin);

        if (gameDataManager != null)
        {
            gameDataManager.SaveGameDataToServer();
            Debug.Log($"Saved coin: {coin} and sent to server.");
        }
        else
        {
            Debug.LogError("GameDataManager is not initialized. Cannot save data to server.");
        }
    }

    public static int LoadCoin()
    {
        if (gameDataManager != null)
        {
            gameDataManager.LoadGameDataFromServer();
        }
        else
        {
            Debug.LogError("GameDataManager is not initialized. Cannot load data from server.");
        }

        int coin = PlayerPrefs.GetInt("Coin", 0);
        Debug.Log($"Loaded coin: {coin}");
        return coin;
    }

    public static void SaveLife(int life)
    {
        PlayerPrefs.SetInt("Life", life);

        if (gameDataManager != null)
        {
            gameDataManager.SaveGameDataToServer();
            Debug.Log($"Saved life: {life} and sent to server.");
        }
        else
        {
            Debug.LogError("GameDataManager is not initialized. Cannot save data to server.");
        }
    }

    public static int LoadLife()
    {
        if (gameDataManager != null)
        {
            gameDataManager.LoadGameDataFromServer();
        }
        else
        {
            Debug.LogError("GameDataManager is not initialized. Cannot load data from server.");
        }

        int life = PlayerPrefs.GetInt("Life", 0);
        Debug.Log($"Loaded life: {life}");
        return life;
    }

    public static void SaveHoveBoard(int hoveBoard)
    {
        PlayerPrefs.SetInt("HoveBoard", hoveBoard);

        if (gameDataManager != null)
        {
            gameDataManager.SaveGameDataToServer();
            Debug.Log($"Saved HoveBoard: {hoveBoard} and sent to server.");
        }
        else
        {
            Debug.LogError("GameDataManager is not initialized. Cannot save data to server.");
        }
    }

    public static int LoadHoveBoard()
    {
        if (gameDataManager != null)
        {
            gameDataManager.LoadGameDataFromServer();
        }
        else
        {
            Debug.LogError("GameDataManager is not initialized. Cannot load data from server.");
        }

        int hoveBoard = PlayerPrefs.GetInt("HoveBoard", 0);
        Debug.Log($"Loaded HoveBoard: {hoveBoard}");
        return hoveBoard;
    }

    public static void SaveBestScore(int score)
    {
        PlayerPrefs.SetInt("BestScore", score);

        if (gameDataManager != null)
        {
            gameDataManager.SaveGameDataToServer();
            Debug.Log($"Saved BestScore: {score} and sent to server.");
        }
        else
        {
            Debug.LogError("GameDataManager is not initialized. Cannot save data to server.");
        }
    }

    public static int LoadBestScore()
    {
        if (gameDataManager != null)
        {
            gameDataManager.LoadGameDataFromServer();
        }
        else
        {
            Debug.LogError("GameDataManager is not initialized. Cannot load data from server.");
        }

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        Debug.Log($"Loaded BestScore: {bestScore}");
        return bestScore;
    }
}
