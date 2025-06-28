using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void StartButtonClicked()
    {
        FindObjectOfType<PlayerMovement>().CanMove = true;
        transform.parent.gameObject.SetActive(false);
    }
}
