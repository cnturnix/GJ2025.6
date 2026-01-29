using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// 书本/标记按钮逻辑。点击 Mark 时：
/// - 首次：打开书本并显示 NPC02（新手引导）；再次点击可关闭书本并移走 NPC02。
/// - 非首次：仅开关书本。
/// </summary>
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
                    if (mainCamera.transform.childCount >= 2)
                    {
                        mainCamera.transform.GetChild(0).localPosition = new Vector3(-6.5f, -3.5f, 10);
                        mainCamera.transform.GetChild(0).localScale = new Vector3(0.27f, 0.27f, 0.27f);
                        mainCamera.transform.GetChild(1).localPosition = new Vector3(6.5f, -3.5f, 10);
                        mainCamera.transform.GetChild(1).localScale = new Vector3(0.32f, 0.32f, 0.32f);
                    }
                }
                StartCoroutine(MoveNPC(NPC02.transform.position + new Vector3(-15, 0, 0), 6f));
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
                // 首次进入该界面时触发 NPC02 教程对话
                PlayNPC02TutorialDialog();
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

    /// <summary>首次进入书本界面时播放 NPC02 教程对话（Trigger1），仅播一次</summary>
    private void PlayNPC02TutorialDialog()
    {
        if (GlobalData.Instance != null && GlobalData.Instance.NPC02TutorialDialogPlayed) return;
        if (NPC02 == null || NPC02.transform.childCount == 0) return;
        Transform trigger = NPC02.transform.GetChild(0);
        trigger.gameObject.SetActive(true);
        var dialog = trigger.GetComponent<NPC02Dialog>();
        if (dialog != null)
        {
            GlobalData.Instance.NPC02TutorialDialogPlayed = true;
            dialog.ShowDialog();
        }
    }
}
