using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public GameObject NPC01;
    public GameObject NPC02;
    public PlayerMovement playerControl;//玩家控制
    public bool FirstTime = true;
    public bool isOpened;
    public bool canClose;
    public void StartButtonClicked()
    {
        FindObjectOfType<PlayerMovement>().CanMove = true;
        transform.parent.gameObject.SetActive(false);
    }

    public void ClickMark()
    {
        if (FirstTime)
        {
            if (isOpened)
            {
                if(!canClose)return;
                isOpened = false;
                EventManager.Instance.TriggerEvent(EventType.OpenBook,new OpenBookEventArgs(false,false));
                FirstTime = false;
                GlobalData.Instance.wall.SetActive(false);
                for(int i=0;i<transform.parent.childCount;i++)
                {
                    transform.parent.GetChild(i).gameObject.SetActive(false);
                }
                GlobalData.Instance.BagCanvas.SetActive(false);
                //TODO：老羊离开动画
                Camera mainCamera = FindObjectOfType<Camera>();
                if (mainCamera != null)
                {
                    mainCamera.orthographicSize = 2;
                }
            }
            else
            {
                
                canClose = false;
                EventManager.Instance.TriggerEvent(EventType.OpenBook,new OpenBookEventArgs(true,true));
                NPC02.SetActive(true);
                NPC02.transform.position=FindObjectOfType<Camera>().transform.position+new Vector3(2.5f,-0.5f,10);
                NPC01.SetActive(false);
                GlobalData.Instance.BagCanvas.SetActive(true);
                //不可移动
                playerControl.CanMove = false;
                playerControl.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                isOpened = true;
            }
            
        }
        else
        {
            
            if(isOpened)
            {
                isOpened = false;
                GlobalData.Instance.BagCanvas.SetActive(false);
                EventManager.Instance.TriggerEvent(EventType.OpenBook,new OpenBookEventArgs(false,false));
            }
            else
            {
                isOpened = true;
                GlobalData.Instance.BagCanvas.SetActive(true);
                EventManager.Instance.TriggerEvent(EventType.OpenBook,new OpenBookEventArgs(false,true));
            }
        }
        
        if (gameObject.GetComponent<Image>().material == GlobalData.Instance.M_Outline)
        {
            gameObject.GetComponent<Image>().material = GlobalData.Instance.M_Defalut;
        }
    }
}
