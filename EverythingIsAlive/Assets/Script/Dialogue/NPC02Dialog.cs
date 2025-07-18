﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC02Dialog : MonoBehaviour
{
    //对话的部分
    public GameObject[] TextSpace;//对话框
    public TMP_Text[] DialogText;//对话文本
    public string[] Dialog;//对话文本
    public float letterDelay = 0.05f; // 字母显示延迟
    private string currentDialog; // 当前正在显示的对话
    public float seconds;//间隔时间

    public void ShowDialog()
    {
        GlobalData.Instance.AudioManager[3].GetComponent<AudioSource>().Play();
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
    }

}
