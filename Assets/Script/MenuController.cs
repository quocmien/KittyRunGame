using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Text walletAddressText; // Text hiển thị địa chỉ ví
    public GameObject roundedContainer; // Hình nền bo tròn
    private string fullWalletAddress; // Địa chỉ đầy đủ

    void Start()
    {
        fullWalletAddress = PlayerPrefs.GetString("WalletAddress", "No Address Found");

        // Rút gọn địa chỉ ví để hiển thị
        string displayWalletAddress = fullWalletAddress.Length > 10
            ? fullWalletAddress.Substring(0, 6) + "..." + fullWalletAddress.Substring(fullWalletAddress.Length - 4)
            : fullWalletAddress;

        // Hiển thị trên UI và thêm sự kiện click
        if (walletAddressText != null)
        {
            walletAddressText.text = displayWalletAddress;
            Button button = walletAddressText.gameObject.AddComponent<Button>();
            button.onClick.AddListener(CopyToClipboard);
        }

        // Ẩn container bo tròn ban đầu
        if (roundedContainer != null)
        {
            roundedContainer.SetActive(false);
        }
    }

    void CopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = fullWalletAddress;

        if (roundedContainer != null)
        {
            roundedContainer.SetActive(true); // Hiển thị khi copy thành công
            Invoke("HideRoundedContainer", 2f); // Ẩn sau 2 giây
        }
    }

    void HideRoundedContainer()
    {
        if (roundedContainer != null)
        {
            roundedContainer.SetActive(false);
        }
    }

    // Phương thức công khai để lấy địa chỉ ví
    public string GetWalletAddress()
    {
        return fullWalletAddress;
    }
}
