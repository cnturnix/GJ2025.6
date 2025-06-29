// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
//
// public class RelicSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
// {
//     public ItemData relicData;
//     public Image iconImage;
//     private InventoryUI inventoryUI;
//     private RectTransform rectTransform;
//     private Canvas canvas;
//     private CanvasGroup canvasGroup;
//     private Vector2 screenOffset;
//     private bool dropped;
//
//     private const float DropSpacing = 30f;
//
//     void Awake()
//     {
//         rectTransform = GetComponent<RectTransform>();
//         canvas = GetComponentInParent<Canvas>();
//         canvasGroup = gameObject.AddComponent<CanvasGroup>();
//     }
//
//     // public void Setup(ItemData data, InventoryUI ui)
//     // {
//     //     relicData = data;
//     //     inventoryUI = ui;
//     //     iconImage.sprite = data.icon;
//     //     iconImage.enabled = data.isPickedUp && !data.isAssignedToOriginalOwner;
//     // }
//
//     public void OnPointerClick(PointerEventData eventData)
//     {
//         if (iconImage.enabled)
//             inventoryUI.ShowItemDetail(relicData);
//     }
//
//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         if (!iconImage.enabled) return;
//
//         dropped = false;
//         canvasGroup.blocksRaycasts = false;
//
//         // Calculate offset between pointer and icon center
//         Vector2 localMousePos;
//         RectTransformUtility.ScreenPointToLocalPointInRectangle(
//             canvas.transform as RectTransform,
//             eventData.position,
//             eventData.pressEventCamera,
//             out localMousePos);
//         Vector3 worldPoint = (canvas.transform as RectTransform).TransformPoint(localMousePos);
//         screenOffset = rectTransform.position - worldPoint;
//
//         // Move to root of canvas to avoid layout
//         rectTransform.SetParent(canvas.transform, worldPositionStays: true);
//     }
//
//     public void OnDrag(PointerEventData eventData)
//     {
//         if (!iconImage.enabled) return;
//         // Follow pointer exactly with offset
//         Vector2 screenPoint = eventData.position;
//         rectTransform.position = screenPoint + screenOffset;
//     }
//
//     public void OnEndDrag(PointerEventData eventData)
//     {
//         if (!iconImage.enabled) return;
//         canvasGroup.blocksRaycasts = true;
//         if (!dropped)
//         {
//             // Return to original placeholder parent to maintain slot position
//             rectTransform.SetParent(inventoryUI.rightItemPanel, worldPositionStays: false);
//             // Keep local anchored position zero to snap into slot
//             rectTransform.anchoredPosition = Vector2.zero;
//         }
//     }
//
//     public void AcceptDrop(Transform dropParent)
//     {
//         dropped = true;
//         rectTransform.SetParent(dropParent, false);
//         // calculate number of relics already under this parent
//         var existingSlots = dropParent.GetComponentsInChildren<RelicSlotUI>(includeInactive: false);
//         int count = existingSlots.Length - 1; // this new slot just added
//         if (count >= 3)
//         {
//             // too many, revert to original parent
//             dropped = false;
//             rectTransform.SetParent(inventoryUI.rightItemPanel, false);
//             rectTransform.anchoredPosition = Vector2.zero;
//             return;
//         }
//         // Position slots horizontally, max 3 per row
//         float xOffset = count * DropSpacing;
//         rectTransform.anchoredPosition = new Vector2(xOffset, 0f);
//     }
// }
