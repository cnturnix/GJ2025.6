using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RemainDrag: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RemainData relicData;
    //public Image iconImage;
    //private InventoryUI inventoryUI;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 screenOffset;
    private bool dropped;
    public int dropradius;
    public GameObject parent;

    private const float DropSpacing = 30f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        dropped = false;
        canvasGroup.blocksRaycasts = false;
        
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localMousePos);
        Vector3 worldPoint = (canvas.transform as RectTransform).TransformPoint(localMousePos);
        screenOffset = rectTransform.position - worldPoint;
        
        //rectTransform.SetParent(canvas.transform, worldPositionStays: true);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!dropped)
        {
            Vector2 screenPoint = Camera.main.ScreenToWorldPoint(eventData.position) ;
            rectTransform.position = screenPoint + screenOffset;
            float minDis=-1;
            int index = -1;
            for (int i = 0; i < GlobalData.Instance.Body.Length; i++)
            { 
                //Debug.Log(GlobalData.Instance.Body[i].transform.position);
                //Debug.Log( rectTransform.position);
                //Debug.Log(Vector3.Distance(GlobalData.Instance.Body[i].GetComponent<RectTransform>().position, rectTransform.position));
                if (Vector3.Distance(GlobalData.Instance.Body[i].transform.position, rectTransform.position) < 1f)
                {
                    if (minDis== -1)
                    {
                        minDis = Vector3.Distance( GlobalData.Instance.Body[i].GetComponent<RectTransform>().position ,rectTransform.position);
                        index = i;
                    }
                    else if (Vector3.Distance(GlobalData.Instance.Body[i].transform.position, rectTransform.position) <
                             minDis)
                    {
                        minDis = Vector3.Distance(GlobalData.Instance.Body[i].transform.position, rectTransform.position);
                        index = i;
                    }
                }
            }

            if (index == -1)
            {
                dropped = false;
            }
            else
            {
                dropped = true;
                parent = GlobalData.Instance.Body[index];

            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        rectTransform.position = GlobalData.Instance.RemainBG[relicData.RemainID - 1].transform.position;
        float minDis=-1;
        int index = -1;
        for (int i = 0; i < GlobalData.Instance.Body.Length; i++)
        {
           
            Debug.Log(Vector3.Distance(GlobalData.Instance.Body[i].transform.position, rectTransform.position));
            if (Vector3.Distance(GlobalData.Instance.Body[i].transform.position, rectTransform.position) < 1f)
            {
                if (minDis== -1)
                {
                    minDis = Vector3.Distance( GlobalData.Instance.Body[i].GetComponent<RectTransform>().position ,rectTransform.position);
                    index = i;
                }
                else if (Vector3.Distance(GlobalData.Instance.Body[i].transform.position, rectTransform.position) <
                         minDis)
                {
                    minDis = Vector3.Distance(GlobalData.Instance.Body[i].transform.position, rectTransform.position);
                    index = i;
                }
            }
        }

        if (index == -1)
        {
            dropped = false;
        }
        else
        {
            dropped = true;
            parent = GlobalData.Instance.Body[index];
            rectTransform.SetParent(parent.transform, true);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}