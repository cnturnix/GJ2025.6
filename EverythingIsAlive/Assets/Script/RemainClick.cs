using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemainClick : MonoBehaviour
{

    public RemainData remainData;
    
    public void clickRemain()
    {
        EventManager.Instance.TriggerEvent(EventType.RemainClicked, remainData);
    }
    

}
