using System;
using UnityEngine;
using UnityEngine.UI;

public class Body01Click:MonoBehaviour
{
    public GameObject Mark;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                GetComponent<SpriteRenderer>().material = GlobalData.Instance.M_Defalut;
                EventManager.Instance.TriggerEvent(EventType.ClickBody,new ClickBodyEventArgs(1));
                EventManager.Instance.TriggerEvent(EventType.GetRemain,new GetRemainEventArgs(1));
                Mark.SetActive(true);
                Mark.GetComponent<Image>().material=GlobalData.Instance.M_Outline;
            }
        }

    }
}