using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class RandomizeCharacter : MonoBehaviour
{
    [System.Serializable]
    public class AccessoryGroup
    {
        public string groupName;
        public GameObject[] items;
    }

    [System.Serializable]
    public class BodyColorSet
    {
        public string setName;
        public Texture[] textures;
    }

    [System.Serializable]
    public class CharacterConfig
    {
        public List<string> accessories = new List<string>();
        public string[] bodyTextures;
    }

    public AccessoryGroup[] accessoryGroups;
    public BodyColorSet[] bodyColorSets;
    public SkinnedMeshRenderer bodyRenderer;
    public int[] specificMaterialIndices;

    private string saveFolderPath;
    private Dictionary<string, Texture> loadedTextures = new Dictionary<string, Texture>();

    private const int characterCount = 10;

    void Start()
    {
        // Load tất cả các texture từ Resources
        LoadAllTextures();

        saveFolderPath = Path.Combine(Application.persistentDataPath, "CharacterConfigs");
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        // Tạo và lưu ngẫu nhiên 10 nhân vật
        for (int i = 0; i < characterCount; i++)
        {
            RandomizeAndSaveCharacter(i);
        }

        // Load 10 nhân vật từ file JSON
        for (int i = 0; i < characterCount; i++)
        {
            LoadCharacterConfig(i);
        }
    }

    private void RandomizeAndSaveCharacter(int characterIndex)
    {
        // Random hóa nhân vật
        RandomizeItems();

        // Lưu cấu hình nhân vật vào file JSON
        string filePath = Path.Combine(saveFolderPath, $"characterConfig_{characterIndex}.json");
        SaveCharacterConfig(filePath);
    }

    public void RandomizeItems()
    {
        DisableAllAccessories();
        RandomizeAccessories();
    }

    private void DisableAllAccessories()
    {
        foreach (var group in accessoryGroups)
        {
            foreach (var item in group.items)
            {
                item.SetActive(false);
            }
        }
    }

    private void RandomizeAccessories()
    {
        foreach (var group in accessoryGroups)
        {
            if (group.items.Length > 0)
            {
                int randomIndex = Random.Range(0, group.items.Length);
                group.items[randomIndex].SetActive(true);
            }
        }

        if (bodyColorSets.Length > 0)
        {
            int randomSetIndex = Random.Range(0, bodyColorSets.Length);
            BodyColorSet selectedSet = bodyColorSets[randomSetIndex];
            Material[] materials = bodyRenderer.materials;
            for (int i = 0; i < specificMaterialIndices.Length; i++)
            {
                int materialIndex = specificMaterialIndices[i];
                if (materialIndex < materials.Length && i < selectedSet.textures.Length)
                {
                    materials[materialIndex].SetTexture("_BASE_COLOR_MAP", selectedSet.textures[i]);
                }
            }
            bodyRenderer.materials = materials;
        }
    }

    private void SaveCharacterConfig(string filePath)
    {
        CharacterConfig config = new CharacterConfig();

        // Lưu danh sách phụ kiện đang bật
        foreach (var group in accessoryGroups)
        {
            foreach (GameObject accessory in group.items)
            {
                if (accessory.activeSelf)
                {
                    config.accessories.Add(accessory.name);
                }
            }
        }

        // Lưu danh sách màu sắc/body texture
        config.bodyTextures = new string[specificMaterialIndices.Length];
        for (int i = 0; i < specificMaterialIndices.Length; i++)
        {
            int materialIndex = specificMaterialIndices[i];
            if (materialIndex < bodyRenderer.materials.Length)
            {
                Material material = bodyRenderer.materials[materialIndex];

                // Sử dụng thuộc tính "_BASE_COLOR_MAP" cho texture
                if (material.HasProperty("_BASE_COLOR_MAP"))
                {
                    Texture mainTexture = material.GetTexture("_BASE_COLOR_MAP");
                    config.bodyTextures[i] = mainTexture != null ? mainTexture.name : "";
                }
                else
                {
                    Debug.LogWarning($"Material '{material.name}' does not have the '_BASE_COLOR_MAP' property.");
                    config.bodyTextures[i] = "";
                }
            }
        }

        // Lưu vào file JSON
        string json = JsonUtility.ToJson(config, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Character configuration saved to: " + filePath);
    }


    private void LoadCharacterConfig(int characterIndex)
    {
        string filePath = Path.Combine(saveFolderPath, $"characterConfig_{characterIndex}.json");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"Character config file not found: {filePath}");
            return;
        }

        string json = File.ReadAllText(filePath);
        CharacterConfig config = JsonUtility.FromJson<CharacterConfig>(json);

        // Tắt tất cả các phụ kiện trước
        DisableAllAccessories();

        // Bật các phụ kiện đã lưu
        foreach (var group in accessoryGroups)
        {
            foreach (GameObject accessory in group.items)
            {
                if (config.accessories.Contains(accessory.name))
                {
                    accessory.SetActive(true);
                }
            }
        }

        // Gán màu sắc/body texture từ cấu hình
        Material[] materials = bodyRenderer.materials;
        for (int i = 0; i < specificMaterialIndices.Length; i++)
        {
            int materialIndex = specificMaterialIndices[i];
            if (materialIndex < materials.Length && i < config.bodyTextures.Length)
            {
                string textureName = config.bodyTextures[i];
                if (!string.IsNullOrEmpty(textureName) && loadedTextures.TryGetValue(textureName, out Texture texture))
                {
                    materials[materialIndex].SetTexture("_BASE_COLOR_MAP", texture);
                    Debug.Log($"Applied texture {textureName} to material index {materialIndex}");
                }
                else
                {
                    Debug.LogWarning($"Texture '{textureName}' not found in loaded textures.");
                }   
            }
        }
        bodyRenderer.materials = materials;

        Debug.Log("Character configuration loaded from: " + filePath);
    }

    private void LoadAllTextures()
    {
        Object[] textures = Resources.LoadAll("TextureThanNguoiKhuonMat/", typeof(Texture));
        foreach (Object tex in textures)
        {
            if (tex is Texture texture)
            {
                loadedTextures[texture.name] = texture;
                Debug.Log($"Loaded texture: {texture.name}");
            }
        }
    }
}
