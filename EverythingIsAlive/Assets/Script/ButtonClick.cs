using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

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
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void ClickMark()
    {
        if (FirstTime)
        {
            if (isOpened)
            {
                if(!canClose)return;
                isOpened = false;
                FirstTime = false;
                GlobalData.Instance.wall.SetActive(false);
                GlobalData.Instance.Book.SetActive(false);
                playerControl.CanMove = true;
                for (int i = 0; i < NPC02.transform.childCount; i++)
                {
                    NPC02.transform.GetChild(i).gameObject.SetActive(false);
                }
                NPC02.GetComponent<Animation>().enabled = true;
                NPC02.GetComponent<Animator>().enabled = true;
                NPC02.GetComponent<Animator>().SetBool("canplay", true);
                
                Camera mainCamera = FindObjectOfType<Camera>();
                if (mainCamera != null)
                {
                    mainCamera.orthographicSize = 5;
                }

                
                mainCamera.transform.GetChild(0).transform.localPosition = new Vector3(-6.5f, -3.5f, 10);
                mainCamera.transform.GetChild(0).transform.localScale = new Vector3(0.27f,0.27f,0.27f);
                mainCamera.transform.GetChild(1).transform.localPosition = new Vector3(6.5f, -3.5f, 10);
                mainCamera.transform.GetChild(1).transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
                StartCoroutine(MoveNPC(NPC02.transform.position+new Vector3(-15,0,0), 6f));
            }
            else
            {
                canClose = false;
                NPC02.SetActive(true);
                NPC02.transform.position=FindObjectOfType<Camera>().transform.position+new Vector3(2.5f,-0.5f,10);
                NPC01.SetActive(false);
                GlobalData.Instance.Book.SetActive(true);
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
                playerControl.CanMove = true;
                GlobalData.Instance.Book.SetActive(false);
                //EventManager.Instance.TriggerEvent(EventType.OpenBook,new OpenBookEventArgs(false,false));
            }
            else
            {
                isOpened = true;
                GlobalData.Instance.Book.SetActive(true);
                //EventManager.Instance.TriggerEvent(EventType.OpenBook,new OpenBookEventArgs(false,true));
            }
        }
        
        if (gameObject.GetComponent<Image>().material == GlobalData.Instance.M_Outline)
        {
            gameObject.GetComponent<Image>().material = GlobalData.Instance.M_Defalut;
        }
    }
    IEnumerator MoveNPC(Vector3 target, float duration)
    {
        Vector3 start = NPC02.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            NPC02.transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
        NPC02.SetActive(false);
    }


}
