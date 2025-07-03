using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerInventory : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        if (item != null && !items.Contains(item))
        {
            item.isPickedUp = true;
            items.Add(item);
        }
    }

    // 获取未分类的物品
    public List<ItemData> GetUnassignedItems()
    {
        return items.Where(item => !item.isAssignedToOriginalOwner).ToList();
    }

    // 获取指定人物的已分类物品
    public List<ItemData> GetItemsAssignedTo(string ownerID)
    {
        return items.Where(item => item.originalOwnerID == ownerID && item.isAssignedToOriginalOwner).ToList();
    }

    // 执行分类操作
    public void AssignItemToOriginalOwner(ItemData item)
    {
        item.isAssignedToOriginalOwner = true;
    }
}

