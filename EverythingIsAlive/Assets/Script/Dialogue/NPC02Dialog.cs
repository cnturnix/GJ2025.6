using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 新手教程：NPC02 的对话（第一次点击遗物时由 ShowDesc 触发）。
/// 逐字显示多句对话，可配置延迟与句间间隔。
/// </summary>
public class NPC02Dialog : MonoBehaviour
{
    [Header("对话 UI")]
    public GameObject[] TextSpace;
    public TMP_Text[] DialogText;
    public string[] Dialog;

    [Header("节奏")]
    public float letterDelay = 0.05f;
    public float seconds = 1f;

    public void ShowDialog()
    {
        PlayTutorialSound();
        StartCoroutine(TypeText());
    }

    private void PlayTutorialSound()
    {
        if (GlobalData.Instance == null || GlobalData.Instance.AudioManager == null) return;
        if (GlobalData.Instance.AudioManager.Length <= 3) return;
        GameObject audioObj = GlobalData.Instance.AudioManager[3];
        if (audioObj != null)
        {
            var audioSource = audioObj.GetComponent<AudioSource>();
            if (audioSource != null) audioSource.Play();
        }
    }

    IEnumerator TypeText()
    {
        int count = Mathf.Min(
            TextSpace != null ? TextSpace.Length : 0,
            DialogText != null ? DialogText.Length : 0,
            Dialog != null ? Dialog.Length : 0);

        for (int i = 0; i < count; i++)
        {
            if (DialogText[i] != null) DialogText[i].text = "";
            string line = Dialog[i];
            if (TextSpace[i] != null) TextSpace[i].SetActive(true);

            foreach (char c in line)
            {
                DialogText[i].text += c;
                yield return new WaitForSeconds(letterDelay);
            }
            yield return new WaitForSeconds(seconds);
        }
    }
}
