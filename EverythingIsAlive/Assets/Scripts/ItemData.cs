using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemID;
    public bool isPickedUp;
    public string characterID;
    public bool isAssignedToOriginalOwner;
    public Sprite icon;
    [TextArea] public string description;
}

