using System;
using UnityEngine;
using UnityEngine.UI;

public class Body01Click:MonoBehaviour
{
    public GameObject Mark;
    public int BodyID;
    public int[] RemainID;
    public bool isClicked=false;
    private void Update()
    {
        if (isClicked == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isClicked = true;
                    GetComponent<SpriteRenderer>().material = GlobalData.Instance.M_Defalut;
                    GlobalData.Instance.AudioManager[2].GetComponent<AudioSource>().Play();
                    EventManager.Instance.TriggerEvent(EventType.ClickBody,new ClickBodyEventArgs(BodyID));
                    for (int i = 0; i < RemainID.Length; i++)
                    {
                        EventManager.Instance.TriggerEvent(EventType.GetRemain,new GetRemainEventArgs(RemainID[i]));
                    }
                    Mark.SetActive(true);
                    Mark.GetComponent<Image>().material=GlobalData.Instance.M_Outline;
                }
            }
        }
        

    }
}