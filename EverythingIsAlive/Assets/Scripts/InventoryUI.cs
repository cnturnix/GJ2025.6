using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("背包数据")]
    public PlayerInventory inventory;

    [Header("上方列表")]
    public Transform topPanel;          
    public GameObject itemSlotPrefab;  

    [Header("详情显示")]
    public Image detailIcon;            // DetailPanel/DetailIcon
    public TMP_Text detailDesc;         // InfoPanel/DetailDesc
    public TMP_Text itemName;

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
        if (detailIcon == null) Debug.LogError("⚠️ detailIcon 没有在 Inspector 里赋值！");
        if (detailDesc == null) Debug.LogError("⚠️ detailDesc 没有在 Inspector 里赋值！");
        if (itemName == null) Debug.LogError("⚠️ itemName 没有在 Inspector 里赋值！");

        detailIcon.sprite = item.icon;
        detailDesc.text = item.description;
        itemName.text = item.itemName;
    }
}

