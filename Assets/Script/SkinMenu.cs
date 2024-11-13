using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryManager : MonoBehaviour
{
    public GameObject[] accessories;  // Mảng chứa tất cả các phụ kiện (mũ, kính, râu, v.v.)

    void Start()
    {
        // Tắt tất cả các phụ kiện khi bắt đầu
        foreach (GameObject accessory in accessories)
        {
            accessory.SetActive(false);
        }
    }

    // Hàm để bật phụ kiện
    public void EquipAccessory(int index)
    {
        if (index >= 0 && index < accessories.Length)
        {
            // Bật phụ kiện được chọn
            accessories[index].SetActive(true);
        }
    }

    // Hàm để tắt tất cả các phụ kiện khác trước khi bật cái mới
    public void UnequipAllAccessories()
    {
        foreach (GameObject accessory in accessories)
        {
            accessory.SetActive(false);
        }
    }
}

