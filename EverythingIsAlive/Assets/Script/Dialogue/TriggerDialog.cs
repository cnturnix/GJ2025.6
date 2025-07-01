using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//挂载在被点击物体下
public class TriggerDialog : MonoBehaviour
{
    //对话的部分
    public GameObject[] TextSpace;//对话框
    public string[] Dialog;//对话文本
    public TMP_Text[] DialogText; // 对话文本UI对象(按照DialogueType顺序)
    private int currentLine = 0; // 当前对话行索引
    public float letterDelay = 0.05f; // 字母显示延迟
    private string currentDialog; // 当前正在显示的对
    public PlayerMovement playerControl;//玩家控制
    public float seconds;

    void Update()
    {
            //点击触发检测
            if (Input.GetMouseButtonDown(0))
            { 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    Debug.Log("点击了");
                    //不可移动
                    playerControl.CanMove = false;
                    playerControl.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    playerControl.gameObject.GetComponent<Animator>().SetBool("isMove", false);
                    GlobalData.Instance.AudioManager[1].GetComponent<AudioSource>().Stop();
                    StartTyping();
                }
            }
    }
    
    private void StartTyping()
    {
        StartCoroutine(TypeText());
  
    }
    IEnumerator TypeText()
    {
        for (int i = 0; i < TextSpace.Length; i++)
        {
            currentDialog=Dialog[i];
            TextSpace[i].SetActive(true);
            foreach (char c in currentDialog)
            {
                DialogText[i].text += c;
                yield return new WaitForSeconds(letterDelay);
            }
            yield return new WaitForSeconds(seconds);
        }
        playerControl.CanMove = true;
        for (int i = 0; i < TextSpace.Length; i++)
        {
            TextSpace[i].SetActive(false);
        }
    }

}
