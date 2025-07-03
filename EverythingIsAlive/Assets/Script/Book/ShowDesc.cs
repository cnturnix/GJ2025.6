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
        RemainImage.sprite = remainData.RemainICON;
        RemainName.text=remainData.RemainName;
        RemainDesc.text = remainData.RemainDesc;

        
    }
    
}
