// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using WalletConnect.Web3Modal;
// using System.Threading.Tasks;
// using System.Numerics;
// using Nethereum.Util;
// using Nethereum.Web3;

// public class Web3Connect : MonoBehaviour
// {
//     public bool IsAccountConnected = false;
//     public GameObject uiCanvas;

//     // RPC endpoint của Binance Smart Chain
//     const string bscRpcUrl = "https://special-burned-sky.bsc.quiknode.pro/de871b0cbc0b8feed0229ee21332597fca5b15c5";

//     // Địa chỉ hợp đồng BEP20 (ví dụ token USDT trên BSC)
//     const string contractAddress = "0xa66cD1C4d890Faa7C1a09A54a254d33d809ba3b5";
//     const string ownerAddress = "0xb63C9e9Cdf4F72F6330e35Df371e27f31f22B02b";

//     // ABI cơ bản của token BEP20
//     const string abi = @"[
//         {""inputs"":[{""internalType"":""address"",""name"":""account"",""type"":""address""}],
//          ""name"":""balanceOf"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],
//          ""stateMutability"":""view"",""type"":""function""},
//         {""inputs"":[],""name"":""decimals"",""outputs"":[{""internalType"":""uint8"",""name"":"""",""type"":""uint8""}],
//          ""stateMutability"":""view"",""type"":""function""}
//     ]";

//     private Web3 web3;

//     async void Start()
//     {
//         try
//         {
//             // Khởi tạo Web3 với Binance Smart Chain RPC
//             web3 = new Web3(bscRpcUrl);
//             Debug.Log("Kết nối tới Binance Smart Chain...");

//             // Khởi tạo Web3Modal
//             await Web3Modal.InitializeAsync();

//             // Đăng ký sự kiện khi tài khoản được kết nối
//             Web3Modal.AccountConnected += async (sender, eventArgs) =>
//             {
//                 try
//                 {
//                     // Lấy tài khoản đã kết nối
//                     Account activeAccount = await eventArgs.GetAccount();
//                     Debug.Log("Account connected: " + activeAccount.Address);

//                     // Đánh dấu tài khoản đã kết nối
//                     IsAccountConnected = true;

//                     // Lưu địa chỉ ví vào PlayerPrefs
//                     PlayerPrefs.SetString("WalletAddress", activeAccount.Address);
//                     PlayerPrefs.Save();

//                     await GetERC20TokenBalance(ownerAddress);
//                 }
//                 catch (System.Exception ex)
//                 {
//                     Debug.LogError("Error during account connection: " + ex.Message);
//                 }
//             };
//         }
//         catch (System.Exception ex)
//         {
//             Debug.LogError("Error initializing Web3Modal: " + ex.Message);
//         }
//     }



//     // Hàm lấy số dư token BEP20
//     public async Task GetERC20TokenBalance(string ownerAddress)
//     {
//         try
//         {
//             var contract = web3.Eth.GetContract(abi, contractAddress);

//             // Gọi hàm balanceOf
//             var balanceOfFunction = contract.GetFunction("balanceOf");
//             BigInteger tokenBalance = await balanceOfFunction.CallAsync<BigInteger>(ownerAddress);

//             // Gọi hàm decimals
//             var decimalsFunction = contract.GetFunction("decimals");
//             BigInteger decimals = await decimalsFunction.CallAsync<BigInteger>();

//             // Tính toán số dư thực tế
//             decimal finalBalance = (decimal)tokenBalance / (decimal)BigInteger.Pow(10, (int)decimals);

//             // Hiển thị số dư trong log hoặc UI
//             Debug.Log($"Token balance of {ownerAddress}: {finalBalance}");
//             Debug.Log($"contractAddress of {contractAddress}");
//         }
//         catch (System.Exception ex)
//         {
//             Debug.LogError("Error getting token balance: " + ex.Message);
//         }
//     }

//     // Hàm này sẽ được gọi khi nhấn nút "Connect"
//     public void OnConnectButtonClicked()
//     {
//         // Kiểm tra Web3Modal đã khởi tạo hay chưa
//         if (Web3Modal.IsInitialized)
//         {
//             Web3Modal.OpenModal();
//         }
//         else
//         {
//             Debug.LogError("Web3Modal is not initialized yet");
//         }
//     }
// }
