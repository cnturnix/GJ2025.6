using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC03Dialog : MonoBehaviour
{
    //对话的部分
    public GameObject[] OldTextSpace;//消失对话框
    public GameObject[] NewTextSpace;//出现对话框
    public TMP_Text[] DialogText;//对话文本
    public string[] Dialog;//对话文本
    public float letterDelay = 0.05f; // 字母显示延迟
    private string currentDialog; // 当前正在显示的对话
    public float seconds;//间隔时间


    public void Body01Confirmed()
    {
        GlobalData.Instance.Mark.GetComponent<Image>().material=GlobalData.Instance.M_Outline;
        GlobalData.Instance.Mark.GetComponent<Button>().canClose=true;
     
        
        for (int i = 0; i < OldTextSpace.Length; i++)
        {
            OldTextSpace[i].SetActive(false);
        }
        StartCoroutine(TypeText());
    }

    
    
    IEnumerator TypeText()
    {
        for (int i = 0; i < NewTextSpace.Length; i++)
        {
            currentDialog=Dialog[i];
            NewTextSpace[i].SetActive(true);
            foreach (char c in currentDialog)
            {
                DialogText[i].text += c;
                yield return new WaitForSeconds(letterDelay);
            }
            yield return new WaitForSeconds(seconds);
        }
    }

}
