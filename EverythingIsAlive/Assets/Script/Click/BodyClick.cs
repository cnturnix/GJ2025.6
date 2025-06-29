using System;
using UnityEngine;
using UnityEngine.UI;

public class BodyClick:MonoBehaviour
{
    public int BodyID;
    public int RemainID;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                GlobalData.Instance.AudioManager[2].GetComponent<AudioSource>().Play();
                EventManager.Instance.TriggerEvent(EventType.ClickBody,new ClickBodyEventArgs(BodyID));
                EventManager.Instance.TriggerEvent(EventType.GetRemain,new GetRemainEventArgs(RemainID));
                
            }
        }

    }
}