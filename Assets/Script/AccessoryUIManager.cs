// using UnityEngine;
// using UnityEngine.UI;

// public class AccessoryUIManager : MonoBehaviour
// {
//     public GameObject accessoryButtonPrefab; // Prefab của nút phụ kiện
//     public Transform contentParent;          // Vị trí cha chứa các nút phụ kiện
//     public AccessoryManager accessoryManager; // Tham chiếu đến script AccessoryManager

//     void Start()
//     {
//         GenerateAccessoryButtons("hat", accessoryManager.hats);
//         GenerateAccessoryButtons("scarf", accessoryManager.scarves);
//         GenerateAccessoryButtons("mask", accessoryManager.masks);
//         GenerateAccessoryButtons("eyebrow", accessoryManager.eyebrows);
//         GenerateAccessoryButtons("eyelash", accessoryManager.eyelashes);
//         GenerateAccessoryButtons("glasses", accessoryManager.glasses);
//         GenerateAccessoryButtons("mouth", accessoryManager.mouths);
//         GenerateAccessoryButtons("beard", accessoryManager.beards);
//     }

//     // Hàm để sinh các nút phụ kiện cho từng loại
//     void GenerateAccessoryButtons(string type, GameObject[] accessories)
//     {
//         for (int i = 0; i < accessories.Length; i++)
//         {
//             // Tạo một instance của Prefab
//             GameObject newButton = Instantiate(accessoryButtonPrefab, contentParent);

//             // Thiết lập text cho nút
//             Text buttonText = newButton.GetComponentInChildren<Text>();
//             buttonText.text = $"{type} {i + 1}";

//             // Thêm sự kiện khi bấm nút
//             int index = i;  // Lưu index để sử dụng trong sự kiện
//             Button buttonComponent = newButton.GetComponent<Button>();
//             buttonComponent.onClick.AddListener(() => accessoryManager.EnableAccessory(type, index));
//         }
//     }
// }
