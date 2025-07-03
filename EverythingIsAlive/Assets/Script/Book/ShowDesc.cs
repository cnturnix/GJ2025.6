using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowDesc : MonoBehaviour
{
    public Image RemainImage;
    public TMP_Text RemainName;
    public TMP_Text RemainDesc;
    public bool FirstClick=true;
    public Sprite DefaultICON;
    public void Start()
    {
        EventManager.Instance.AddListener<RemainData>(EventType.RemainClicked, OnRemainClicked);
    }

    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener<RemainData>(EventType.RemainClicked, OnRemainClicked);
    }

    public void OnRemainClicked(RemainData remainData)
    {
        if (FirstClick)
        {
            FirstClick = false;
            GlobalData.Instance.NPC02.transform.GetChild(0).GetComponent<NPC02Dialog>().ShowDialog();
        }
        RemainImage.sprite = remainData.RemainICON;
        RemainName.text=remainData.RemainName;
        RemainDesc.text = remainData.RemainDesc;
    }

    public void Clear()
    {
        RemainImage.sprite = DefaultICON;
        RemainName.text = "";
        RemainDesc.text = "";
    }
}
