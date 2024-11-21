using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankItemController : MonoBehaviour
{
    public Image rankIcon;              // Biểu tượng xếp hạng (ví dụ: Top 1, 2, 3)
    public Image avatarImage;           // Hình đại diện của người chơi
    public TextMeshProUGUI playerNameText; // Tên người chơi
    public TextMeshProUGUI playerScoreText; // Điểm số của người chơi
    public TextMeshProUGUI rankText;       // Thứ hạng của người chơi

    public void SetRankItem(Sprite icon, Sprite avatar, string playerName, int score, int rank)
    {
        // Gán biểu tượng xếp hạng (nếu có)
        if (icon != null)
        {
            rankIcon.sprite = icon;
            rankIcon.gameObject.SetActive(true);
        }
        else
        {
            rankIcon.gameObject.SetActive(false);
        }

        // Gán dữ liệu còn lại
        avatarImage.sprite = avatar;
        playerNameText.text = playerName;
        playerScoreText.text = score.ToString();
        rankText.text = rank.ToString(); // Hiển thị thứ hạng
    }
}
