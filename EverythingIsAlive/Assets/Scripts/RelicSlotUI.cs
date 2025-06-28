using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RelicSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData relicData;
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

    public void Setup(ItemData data)
    {
        relicData = data;
        GetComponent<Image>().sprite = data.icon;
        // 初始状态：只有已拾取且未分配的物品可见可交互
        gameObject.SetActive(data.isPickedUp && !data.isAssignedToOriginalOwner);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dropped = false;
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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

