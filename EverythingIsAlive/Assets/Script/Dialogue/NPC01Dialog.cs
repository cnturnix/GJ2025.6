using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC01Dialog : MonoBehaviour
{
    //对话的部分
    public GameObject[] TextSpace;//对话框
    public TMP_Text[] DialogText;//对话文本
    public string[] Dialog;//对话文本
    public float letterDelay = 0.05f; // 字母显示延迟
    private string currentDialog; // 当前正在显示的对话

    public PlayerMovement playerControl;//玩家控制
    public bool TriggerDialogue;//是否进入可触发对话区域
    public bool TriggerOnce=true;
    public GameObject Body01;
    public float seconds;
    #region OnTrigger

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue = false;
        }
    }


    #endregion
    void Start()
    {
        TriggerDialogue = false;
    }

    void Update()
    {
        //进入区域，触发对话检测
        if (TriggerDialogue)
        {
            if (TriggerOnce)
            {
                TriggerOnce = false;
                //不可移动
                playerControl.CanMove = false;
                playerControl.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                playerControl.gameObject.GetComponent<Animator>().SetBool("isMove", false);
                GlobalData.Instance.AudioManager[3].GetComponent<AudioSource>().Stop();
                //开始对话第一句
                StartTyping();
            }
                    
        }
    }

    private void StartTyping()
    {
        GlobalData.Instance.AudioManager[3].GetComponent<AudioSource>().Play();
        Debug.Log("oldgoataudio");
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

        Body01.GetComponent<SpriteRenderer>().material = GlobalData.Instance.M_Outline;
        playerControl.CanMove = true;
    }

}
