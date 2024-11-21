using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerRank
    {
        public Sprite avatar;
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class RankData
    {
        public List<PlayerRankJson> ranks;
    }

    [System.Serializable]
    public class PlayerRankJson
    {
        public string playerName;
        public int score;
        public string avatarPath; // Đường dẫn avatar
    }

    public List<PlayerRank> playerRanks = new List<PlayerRank>();
    public GameObject rankItemPrefab; // Prefab UI cho từng mục
    public Transform contentParent;   // Nội dung trong Scroll View
    public Sprite top1Icon;    
    public Sprite top2Icon;    
    public Sprite top3Icon;    
    public Sprite defaultIcon; 

    public GameObject rankWindow; 
    public float zoomDuration = 1f; 
    public Vector3 targetScale = Vector3.one;

    void Start()
    {

        string path = Application.streamingAssetsPath + "/rank.json";
        StartCoroutine(LoadRankData(path));
    }

    IEnumerator LoadRankData(string path)
    {
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonContent = request.downloadHandler.text;
            RankData rankData = JsonUtility.FromJson<RankData>(jsonContent);

            foreach (var rank in rankData.ranks)
            {
                PlayerRank player = new PlayerRank
                {
                    playerName = rank.playerName,
                    score = rank.score,
                    avatar = Resources.Load<Sprite>(rank.avatarPath)
                };

                if (player.avatar == null)
                {
                    Debug.LogWarning($"Avatar not found for {rank.playerName}, path: {rank.avatarPath}");
                }

                playerRanks.Add(player);
            }

            GenerateRankList();
        }
        else
        {
            Debug.LogError("Error loading JSON: " + request.error);
        }
    }

    void GenerateRankList()
    {
        int rank = 1; // Bắt đầu từ hạng 1
        foreach (var player in playerRanks)
        {
            // Tạo item từ prefab
            GameObject rankItem = Instantiate(rankItemPrefab, contentParent);

            // Gán dữ liệu cho từng item
            RankItemController controller = rankItem.GetComponent<RankItemController>();
            if (controller != null)
            {
                // Xác định biểu tượng hạng
                Sprite rankIcon = null;
                switch (rank)
                {
                    case 1: rankIcon = top1Icon; break;
                    case 2: rankIcon = top2Icon; break;
                    case 3: rankIcon = top3Icon; break;
                    default: rankIcon = defaultIcon; break;
                }

                // Gán dữ liệu cho RankItemController
                controller.SetRankItem(rankIcon, player.avatar, player.playerName, player.score, rank);
            }
            else
            {
                Debug.LogError("RankItemController not found on prefab!");
            }

            rank++; // Tăng thứ hạng
        }
    }

    public void OpenRankWindow()
    {
        rankWindow.SetActive(true);

    }

    public void CloseRankWindow()
    {
        rankWindow.SetActive(false);
    }



}
