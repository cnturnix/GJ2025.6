using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum DialogType
{
    Player,
    NPC,
    
}

public class NPCDialog : MonoBehaviour
{
    //对话的部分
    public Text[] DialogText; // 对话文本UI对象(按照DialogueType顺序)
    public string[] dialogLines; // 对话行数组
    public DialogType[] avatars; // 对话角色顺序
    private int currentLine = 0; // 当前对话行索引
    public float letterDelay = 0.05f; // 字母显示延迟
    private string currentDialog; // 当前正在显示的对话
    public bool isTyping = false; // 是否正在逐字显示对话
    public bool isWaitingForSpace = false;//是否在等待空格切换下一句
    
    public bool TriggerOnce = true;// 是否只触发一次
    
    public bool TriggerControlHint = false;//是否在第一句触发控制提示空格切换到下一句
    public GameObject ControlHint;//控制提示对象
    
    public bool NeedClickTrigger;//是否需要左击触发
    public GameObject ClickObject;//右击触发对象
    
    public PlayerMovement playerControl;//玩家控制

    //trigger的部分
    bool TriggerDialogue;

    void OnTriggerEnter2D(Collider2D other)
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
            if (NeedClickTrigger&&Input.GetMouseButtonDown(0))
            { 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                // 射线检测被点击到的物体是目标对象
                if (hit.collider != null && hit.collider.gameObject == ClickObject)
                {
                    if (currentLine == 0)
                    {
                        StartTyping();
                        playerControl.CanMove = false;
                    }
                }
            }

            if (isTyping && !isWaitingForSpace)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentDialog = dialogLines[currentLine];//找到当前句文本
                    DialogText[(int)avatars[currentLine]].text = currentDialog;//设定新文本为当前句
                    isTyping = false;//不在打字
                    isWaitingForSpace = true; // 设置为等待空格键
                    currentLine++; // 准备显示下一行对话
                }
            }
            else if (currentLine >= dialogLines.Length && isWaitingForSpace)//对话结束
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerControl.CanMove = true;
                    isTyping = false;
                    isWaitingForSpace = true; // 设置为等待空格键
                    currentLine = 0;
                }
            }
            else
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
            DialogText[(int)avatars[currentLine]].text = currentDialog;

            // 准备逐字显示对话
            isTyping = true;
            isWaitingForSpace = false;

            // 开始逐字显示对话
            StartCoroutine(TypeText());
            
            // 启动协程控制隐藏
            StartCoroutine(HideDialogAfterSeconds(2f));
        }
    }

    
    IEnumerator TypeText()
    {
        foreach (char c in currentDialog)
        {
            //dialogText.text += c;
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
    }


}
