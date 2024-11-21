// using UnityEngine;
// using Reown.AppKit.Unity;
// using System.Threading.Tasks;

// public class AppKitAccountHandler : MonoBehaviour
// {
//     async void Start()
//     {
//         // Đăng ký sự kiện AccountConnected
//         AppKit.AccountConnected += async (sender, eventArgs) =>
//         {
//             try
//             {
//                 // Lấy tài khoản từ tác vụ không đồng bộ
//                 Account activeAccount = await eventArgs.GetAccount();
//                 if (activeAccount != null)
//                 {
//                     string walletAddress = activeAccount.Address; // Lấy địa chỉ ví
//                     PlayerPrefs.SetString("WalletAddress", walletAddress); // Lưu vào PlayerPrefs
//                     PlayerPrefs.Save(); // Lưu dữ liệu
//                     Debug.Log("Wallet connected successfully: " + walletAddress);
//                 }
//                 else
//                 {
//                     Debug.LogWarning("No account connected.");
//                 }
//             }
//             catch (System.Exception ex)
//             {
//                 Debug.LogError("Error retrieving account: " + ex.Message);
//             }
//         };
//     }
// }
