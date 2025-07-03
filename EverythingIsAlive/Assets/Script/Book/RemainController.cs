using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemainController : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public bool inBody=false;
    public RemainData remainData;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public void clickRemain()
    {
        EventManager.Instance.TriggerEvent(EventType.RemainClicked, remainData);
    }
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        inBody = false;
        canvasGroup.blocksRaycasts = false;
        this.transform.SetParent(GlobalData.Instance.Book.transform);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 screenPoint = Camera.main.ScreenToWorldPoint(eventData.position) ;
        rectTransform.position = screenPoint;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (!inBody)
        {
            transform.parent = GlobalData.Instance.BookRemainParent.transform;
            rectTransform.position = GlobalData.Instance.BookRemainBG[remainData.RemainID - 1].transform.position;
        }
        
    }

}
