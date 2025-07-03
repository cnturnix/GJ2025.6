using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("背包数据")]
    public PlayerInventory inventory;

    [Header("上方列表")]
    public Transform topPanel;          // TopPanel 容器
    public GameObject itemSlotPrefab;   // 小图标预制体

    [Header("详情显示")]
    public Image detailIcon;            // DetailPanel/DetailIcon
    public TMP_Text detailDesc;         // InfoPanel/DetailDesc

    void Start()
    {
        RefreshTopPanel();
    }

    // 刷新上方图标列表
    public void RefreshTopPanel()
    {
        // 清空旧的
        foreach (Transform c in topPanel) Destroy(c.gameObject);

        // 重新生成
        List<ItemData> list = inventory.GetUnassignedItems();
        foreach (var item in list)
        {
            var go = Instantiate(itemSlotPrefab, topPanel);
            var slotUI = go.GetComponent<ItemSlotUI>();
            slotUI.Setup(item, this);
        }
    }

    // 点击某个物品后调用，显示到下方大图和右侧文字
    public void ShowItemDetail(ItemData item)
    {
        detailIcon.sprite = item.icon;
        detailDesc.text = item.description;
    }
}

