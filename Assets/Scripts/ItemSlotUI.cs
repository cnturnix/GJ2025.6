using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ItemSlotUI : MonoBehaviour
{
    public Image iconImage;         // 小图标
    private ItemData itemData;
    private InventoryUI ui;

    // 由 InventoryUI 传入自身引用和数据
    public void Setup(ItemData item, InventoryUI parentUI)
    {
        Debug.Log($"[Setup] 初始化物品槽：{item.itemID}");
        itemData = item;
        ui = parentUI;
        iconImage.sprite = item.icon;

        // 给 Button 加点击事件
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnClick);

    }

    private void OnClick()
    {
        ui.ShowItemDetail(itemData);
        Debug.Log($"[ItemSlotUI] 点击了物品：{itemData.itemID} —— 描述：{itemData.description}");

    }
}

