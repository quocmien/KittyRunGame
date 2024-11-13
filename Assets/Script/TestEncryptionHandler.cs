// using UnityEngine;

// public class TestEncryptionHandler : MonoBehaviour
// {
//     void Start()
//     {
//         // Địa chỉ ví cần mã hóa
//         string walletAddress = "0xYourWalletAddress";  // Thay bằng địa chỉ ví thực tế của bạn
//         Debug.Log("Original Wallet Address: " + walletAddress);

//         // Mã hóa địa chỉ ví thành Base64
//         string encryptedWalletBase64 = EncryptionHandler.EncryptWalletAddressBase64(walletAddress);
//         Debug.Log("Encrypted Wallet Address (Base64): " + encryptedWalletBase64);

//         // Giải mã Base64 để lấy địa chỉ ví làm key cho AES
//         string decryptedWallet = EncryptionHandler.DecryptWalletAddressBase64(encryptedWalletBase64);
//         Debug.Log("Decrypted Wallet Address from Base64: " + decryptedWallet);

//         // Dữ liệu cần mã hóa bằng AES
//         string dataToEncrypt = "This is my sensitive data.";
//         Debug.Log("Original Data: " + dataToEncrypt);

//         // Mã hóa dữ liệu bằng AES với key là địa chỉ ví
//         string aesEncryptedData = EncryptionHandler.EncryptDataAES(dataToEncrypt, decryptedWallet);
//         Debug.Log("AES Encrypted Data: " + aesEncryptedData);

//         // Giải mã dữ liệu AES để kiểm tra quá trình giải mã
//         string decryptedData = EncryptionHandler.DecryptDataAES(aesEncryptedData, decryptedWallet);
//         Debug.Log("Decrypted Data: " + decryptedData);
//     }
// }
