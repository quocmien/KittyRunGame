using UnityEngine;
using UnityEngine.UI;

public class AvatarSelector : MonoBehaviour
{
    public GameObject avatarSelectionCanvas; // Canvas giao diện chọn avatar
    public Image mainAvatar; // Hình ảnh avatar chính bên ngoài
    [SerializeField] private Sprite[] avatarSprites; // Danh sách các avatar (gán trong Inspector)

    void Start()
    {
        // Gán sự kiện tạo Button cho mỗi avatar
        for (int i = 0; i < avatarSprites.Length; i++)
        {
            CreateAvatarButton(i);
        }
    }

    void CreateAvatarButton(int index)
    {
        // Tìm AvatarGrid
        Transform avatarGrid = GameObject.Find("AvatarGrid").transform;

        // Tạo Button mới
        GameObject avatarButton = new GameObject("AvatarButton" + index, typeof(Button), typeof(Image));
        avatarButton.transform.SetParent(avatarGrid);

        // Gán hình ảnh avatar
        Image buttonImage = avatarButton.GetComponent<Image>();
        buttonImage.sprite = avatarSprites[index];
        buttonImage.preserveAspect = true;

        // Gán sự kiện click
        Button button = avatarButton.GetComponent<Button>();
        button.onClick.AddListener(() => SelectAvatar(avatarSprites[index]));
    }

    void SelectAvatar(Sprite avatar)
    {
        // Thay đổi avatar chính
        mainAvatar.sprite = avatar;

        // Ẩn giao diện chọn avatar
        avatarSelectionCanvas.SetActive(false);

        // Lưu avatar đã chọn (tuỳ chọn)
        PlayerPrefs.SetString("SelectedAvatar", avatar.name);
        PlayerPrefs.Save();

        Debug.Log("Avatar selected: " + avatar.name);
    }

    public void OpenAvatarSelection()
    {
        // Hiển thị giao diện chọn avatar
        avatarSelectionCanvas.SetActive(true);
    }

    public void CloseAvatarSelection()
    {
        // Đóng giao diện chọn avatar
        avatarSelectionCanvas.SetActive(false);
    }
}
