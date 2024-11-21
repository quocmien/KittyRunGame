using UnityEngine;
using UnityEngine.SceneManagement; // Để sử dụng SceneManager
using Reown.AppKit.Unity;

public class AppKitInitializer : MonoBehaviour
{
    private bool isInitialized = false; // Cờ kiểm tra trạng thái khởi tạo

    public async void Start()
    {
        try
        {
            // Tạo cấu hình AppKit
            var config = new AppKitConfig(
                projectId: "ec63387df47921cc025071e999ffd962", 
                metadata: new Metadata(
                    name: "My Game", 
                    description: "Short description of the game",
                    url: "https://example.com",
                    iconUrl: "https://example.com/logo.png"
                )
            );

            // Khởi tạo AppKit
            await AppKit.InitializeAsync(config);
            isInitialized = true; // Đánh dấu đã khởi tạo thành công
            Debug.Log("AppKit initialized successfully!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to initialize AppKit: " + ex.Message);
        }
    }

    public async void OpenAppKitModal()
    {
        if (!isInitialized)
        {
            Debug.LogError("AppKit is not initialized. Cannot open modal.");
            return;
        }

        try
        {
            // Mở modal để người dùng đăng nhập
            AppKit.OpenModal();

            // Lắng nghe sự kiện đăng nhập thành công
            AppKit.AccountConnected += async (sender, eventArgs) =>
            {
                var account = await eventArgs.GetAccount(); // Lấy thông tin tài khoản
                if (account != null)
                {
                    string walletAddress = account.Address;
                    Debug.Log("Wallet connected successfully: " + walletAddress);

                    // Lưu địa chỉ ví
                    PlayerPrefs.SetString("WalletAddress", walletAddress);
                    PlayerPrefs.Save();

                    // Chuyển sang cảnh Menu
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    Debug.LogWarning("Account not connected.");
                }
            };
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error during account connection: " + ex.Message);
        }
    }
}
