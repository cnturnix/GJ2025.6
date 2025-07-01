using UnityEngine;
using UnityEngine.EventSystems;

public class DropScript : MonoBehaviour,IDropHandler
{
    public Vector2[] dropPoint;
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RemainController>().inBody=true;
        eventData.pointerDrag.transform.SetParent(transform);
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition=GetComponent<RectTransform>().anchoredPosition+dropPoint[transform.childCount-1];
        //被拖动ui物体的锚点位置==插槽锚点位置
        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}