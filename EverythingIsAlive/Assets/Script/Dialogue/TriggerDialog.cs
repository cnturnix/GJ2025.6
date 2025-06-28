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
    public GameObject TextSpace;//对话框
    public TMP_Text DialogText; // 对话文本UI对象(按照DialogueType顺序)
    public string[] dialogLines; // 对话行数组
    private int currentLine = 0; // 当前对话行索引
    public float letterDelay = 0.05f; // 字母显示延迟
    private string currentDialog; // 当前正在显示的对话
    public bool isTyping = false; // 是否正在逐字显示对话
    public bool isWaitingForSpace = false;//是否在等待空格切换下一句
    
    public PlayerMovement playerControl;//玩家控制
    
    public bool TriggerDialogue;//是否进入可触发对话区域

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
            //点击触发检测
            if (Input.GetMouseButtonDown(0))
            { 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                // 射线检测被点击到的物体是父对象
                // if (hit.collider != null)
                // {
                //     Debug.Log(hit.collider.name);
                // }
                if (hit.collider != null && hit.collider.gameObject == transform.parent.gameObject)
                {
                    //触发对话第一句
                    if (currentLine == 0)
                    {
                        TextSpace.SetActive(true);
                        //开始对话第一句
                        StartTyping();
                        //不可移动
                        playerControl.CanMove = false;
                        playerControl.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    }
                }
            }
            //正在输入且没有输入完毕时按空格直接显示整句
            if (isTyping && !isWaitingForSpace)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentDialog = dialogLines[currentLine];//找到当前句文本
                    DialogText.text = currentDialog;//设定新文本为当前句
                    isTyping = false;//不在打字
                    isWaitingForSpace = true; // 设置为等待空格键
                    currentLine++; // 准备显示下一行对话
                }
            }
            //对话结束时重置
            else if (currentLine >= dialogLines.Length && isWaitingForSpace)
            {
                // 启动协程控制隐藏
                StartCoroutine(HideDialogAfterSeconds(2f));
            }
            else//对话中时按空格切换下一句
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartTyping();
                }
            }
        }
    }

    private void StartTyping()
    {
        if (currentLine < dialogLines.Length)
        {
            //设定将要显示的文本
            currentDialog = dialogLines[currentLine];

            // 准备逐字显示对话
            isTyping = true;
            isWaitingForSpace = false;

            // 开始逐字显示对话
            StartCoroutine(TypeText());
            
            
        }
    }
    
    IEnumerator TypeText()
    {
        foreach (char c in currentDialog)
        {
            DialogText.text += c;
            yield return new WaitForSeconds(letterDelay);
        }

        isTyping = false;
        isWaitingForSpace = true;
        currentLine++;
    }
    
    IEnumerator HideDialogAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds + currentDialog.Length * letterDelay);

        if (!isTyping && isWaitingForSpace)
        {
            HideDialog();
        }
    }
    private void HideDialog()
    {
        playerControl.CanMove = true;
        isTyping = false;
        isWaitingForSpace = false;
        currentLine = 0;
        TextSpace.SetActive(false);
    }


}
