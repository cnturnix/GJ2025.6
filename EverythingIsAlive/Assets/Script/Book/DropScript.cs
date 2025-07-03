using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropScript : MonoBehaviour,IDropHandler
{
    public Vector2[] dropPoint;
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RemainController>().inBody=true;
        eventData.pointerDrag.transform.SetParent(transform);
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition=GetComponent<RectTransform>().anchoredPosition+dropPoint[transform.childCount-1];
        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (GlobalData.Instance.Mark.GetComponent<ButtonClick>().FirstTime)
        {
            if (transform.childCount == transform.parent.GetChild(1)
                    .GetComponent<ConfirmClicked>().bodyData.RemainsID.Count)
            {
                transform.parent.GetChild(1).GetComponent<Image>().material = GlobalData.Instance.M_Outline;
            }
        }
    }
}