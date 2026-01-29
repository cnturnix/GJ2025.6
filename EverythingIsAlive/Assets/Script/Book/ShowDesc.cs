using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 书本右侧遗物说明面板。显示遗物图标/名称/描述；
/// 新手教程：第一次点击任意遗物时，触发 NPC02 的教程对话。
/// </summary>
public class ShowDesc : MonoBehaviour
{
    [Header("UI 引用")]
    public Image RemainImage;
    public TMP_Text RemainName;
    public TMP_Text RemainDesc;
    public Sprite DefaultICON;

    /// <summary>是否尚未触发过“第一次点击遗物”的教程对话</summary>
    public bool FirstClick = true;

    void Start()
    {
        EventManager.Instance.AddListener<RemainData>(EventType.RemainClicked, OnRemainClicked);
    }

    void OnDestroy()
    {
        EventManager.Instance.RemoveListener<RemainData>(EventType.RemainClicked, OnRemainClicked);
    }

    /// <summary>
    /// 遗物被点击时：若为第一次点击则播放 NPC02 教程对话；并刷新说明内容。
    /// </summary>
    public void OnRemainClicked(RemainData remainData)
    {
        // 新手教程：第一次点击遗物时播放 NPC02 对话
        if (FirstClick)
        {
            FirstClick = false;
            TryPlayFirstClickTutorial();
        }

        if (remainData != null)
        {
            if (RemainImage != null) RemainImage.sprite = remainData.RemainICON;
            if (RemainName != null) RemainName.text = remainData.RemainName;
            if (RemainDesc != null) RemainDesc.text = remainData.RemainDesc;
        }
    }

    /// <summary>尝试播放“第一次点击遗物”的教程对话（NPC02 子物体 Trigger1），若已播过则跳过</summary>
    private void TryPlayFirstClickTutorial()
    {
        if (GlobalData.Instance == null || GlobalData.Instance.NPC02 == null) return;
        if (GlobalData.Instance.NPC02TutorialDialogPlayed) return;
        Transform npc02 = GlobalData.Instance.NPC02.transform;
        if (npc02.childCount == 0) return;

        Transform trigger = npc02.GetChild(0);
        trigger.gameObject.SetActive(true); // 协程只能在激活的 GameObject 上启动
        var dialog = trigger.GetComponent<NPC02Dialog>();
        if (dialog != null)
        {
            GlobalData.Instance.NPC02TutorialDialogPlayed = true;
            dialog.ShowDialog();
        }
    }

    public void Clear()
    {
        if (RemainImage != null && DefaultICON != null) RemainImage.sprite = DefaultICON;
        if (RemainName != null) RemainName.text = "";
        if (RemainDesc != null) RemainDesc.text = "";
    }
}
