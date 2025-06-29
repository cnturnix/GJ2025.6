using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RelicSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public ItemData relicData;
    public Image iconImage;
    private InventoryUI inventoryUI;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector2 originalPosition;
    private bool dropped;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    // Now accept UI reference
    public void Setup(ItemData data, InventoryUI ui)
    {
        relicData = data;
        inventoryUI = ui;
        iconImage.sprite = data.icon;
        // show only if picked up and not yet assigned
        iconImage.enabled = data.isPickedUp && !data.isAssignedToOriginalOwner;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (iconImage.enabled)
        {
            inventoryUI.ShowItemDetail(relicData);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!iconImage.enabled) return;
        dropped = false;
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!iconImage.enabled) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!iconImage.enabled) return;
        canvasGroup.blocksRaycasts = true;
        if (!dropped)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    public void AcceptDrop(Transform dropParent)
    {
        dropped = true;
        transform.SetParent(dropParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}