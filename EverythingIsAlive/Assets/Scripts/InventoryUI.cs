using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory inventory;
    public Transform rightItemPanel;
    public GameObject relicSlotPrefab;

    [Header("详情显示")]
    public Image detailIcon;            // DetailPanel/DetailIcon
    public TMP_Text detailDesc;         // InfoPanel/DetailDesc
    public TMP_Text itemName;

   
    void Start()
    {
        RefreshUI();
        ClearDetail();
    }

    public void RefreshUI()
    {
        foreach (Transform t in rightItemPanel)
            Destroy(t.gameObject);

        foreach (var item in inventory.items)
        {
            var slotGO = Instantiate(relicSlotPrefab, rightItemPanel);
            // pass this UI so clicks work
            slotGO.GetComponent<RelicSlotUI>().Setup(item, this);
        }
    }

  
    public void ClearDetail()
    {
        detailIcon.sprite = null;
        detailIcon.color = new Color(1, 1, 1, 0);
        detailDesc.text = string.Empty;
    }
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

