using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ConfirmClicked : MonoBehaviour
{
    public BodyData bodyData;
    public bool passConfirm;
    

    public void Clicked1Confirm()
    {
        passConfirm = false;
        GameObject ConfirmParent = transform.parent.GetChild(0).gameObject;
        if (ConfirmParent.transform.childCount == bodyData.RemainsID.Count)
        {
            passConfirm = true;
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                bool FindSame=false;
                for (int j = 0; j < bodyData.RemainsID.Count; j++)
                {
                    if (ConfirmParent.transform.GetChild(i).GetComponent<RemainController>().remainData ==
                        bodyData.RemainsID[j])
                    {
                        FindSame = true;
                    }
                }
                if (FindSame == false)
                {
                    passConfirm = false;
                }
            }
        }
        if (passConfirm == true)
        {
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                ConfirmParent.transform.GetChild(i).gameObject.SetActive(false);
            }
            GlobalData.Instance.BookBody[bodyData.BodyID-1].SetActive(false);
            GlobalData.Instance.BookAliveBody[bodyData.BodyID-1].SetActive(true);
            GlobalData.Instance.Bodies[bodyData.BodyID-1].SetActive(false);
            GlobalData.Instance.AliveBodies[bodyData.BodyID-1].SetActive(true);
            GetComponent<Image>().material=GlobalData.Instance.M_Defalut;
            GlobalData.Instance.Mark.GetComponent<Image>().material = GlobalData.Instance.M_Outline;
            GlobalData.Instance.Mark.GetComponent<ButtonClick>().canClose = true;
        }

       
    }
    public void Clicked2Confirm()
    {
        passConfirm = false;
        GameObject ConfirmParent = transform.parent.GetChild(0).gameObject;
        
        if (ConfirmParent.transform.childCount == bodyData.RemainsID.Count)
        {
            
            passConfirm = true;
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                bool FindSame=false;
                for (int j = 0; j < bodyData.RemainsID.Count; j++)
                {
                    if (ConfirmParent.transform.GetChild(i).GetComponent<RemainController>().remainData ==
                        bodyData.RemainsID[j])
                    {
                        FindSame = true;
                    }
                }
                if (FindSame == false)
                {
                    
                    passConfirm = false;
                }
            }
        }
        if (passConfirm == true)
        {
            
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
               
                ConfirmParent.transform.GetChild(i).gameObject.SetActive(false);
            }
            GlobalData.Instance.BookBody[bodyData.BodyID-1].SetActive(false);
            GlobalData.Instance.BookAliveBody[bodyData.BodyID-1].SetActive(true);
        }
    }
    public void Clicked3Confirm()
    {
        passConfirm = false;
        GameObject ConfirmParent = transform.parent.GetChild(0).gameObject;
        
        if (ConfirmParent.transform.childCount == bodyData.RemainsID.Count)
        {
            
            passConfirm = true;
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                bool FindSame=false;
                for (int j = 0; j < bodyData.RemainsID.Count; j++)
                {
                    if (ConfirmParent.transform.GetChild(i).GetComponent<RemainController>().remainData ==
                        bodyData.RemainsID[j])
                    {
                        FindSame = true;
                    }
                }
                if (FindSame == false)
                {
                    passConfirm = false;
                }
            }
        }
        if (passConfirm == true)
        {
            
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                
                ConfirmParent.transform.GetChild(i).gameObject.SetActive(false);
            }
            GlobalData.Instance.BookBody[bodyData.BodyID-1].SetActive(false);
            GlobalData.Instance.BookAliveBody[bodyData.BodyID-1].SetActive(true);
        }
    }
    public void Clicked4Confirm()
    {
        passConfirm = false;
        GameObject ConfirmParent = transform.parent.GetChild(0).gameObject;
        
        if (ConfirmParent.transform.childCount == bodyData.RemainsID.Count)
        {
            
            passConfirm = true;
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                bool FindSame=false;
                for (int j = 0; j < bodyData.RemainsID.Count; j++)
                {
                    if (ConfirmParent.transform.GetChild(i).GetComponent<RemainController>().remainData ==
                        bodyData.RemainsID[j])
                    {
                        FindSame = true;
                    }
                }
                if (FindSame == false)
                {
                    passConfirm = false;
                }
            }
        }
        if (passConfirm == true)
        {
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                ConfirmParent.transform.GetChild(i).gameObject.SetActive(false);
            }
            GlobalData.Instance.BookBody[bodyData.BodyID-1].SetActive(false);
            GlobalData.Instance.BookAliveBody[bodyData.BodyID-1].SetActive(true);
        }
    }
    public void Clicked5Confirm()
    {
        passConfirm = false;
        GameObject ConfirmParent = transform.parent.GetChild(0).gameObject;
        if (ConfirmParent.transform.childCount == bodyData.RemainsID.Count)
        {
            
            passConfirm = true;
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                bool FindSame=false;
                for (int j = 0; j < bodyData.RemainsID.Count; j++)
                {
                    if (ConfirmParent.transform.GetChild(i).GetComponent<RemainController>().remainData ==
                        bodyData.RemainsID[j])
                    {
                        FindSame = true;
                    }
                }
                if (FindSame == false)
                {
                    passConfirm = false;
                }
            }
        }
        if (passConfirm == true)
        {
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                ConfirmParent.transform.GetChild(i).gameObject.SetActive(false);
            }
            GlobalData.Instance.BookBody[bodyData.BodyID-1].SetActive(false);
            GlobalData.Instance.BookAliveBody[bodyData.BodyID-1].SetActive(true);
        }
    }
    public void Clicked6Confirm()
    {
        passConfirm = false;
        GameObject ConfirmParent = transform.parent.GetChild(0).gameObject;
        if (ConfirmParent.transform.childCount == bodyData.RemainsID.Count)
        {
            passConfirm = true;
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                bool FindSame=false;
                for (int j = 0; j < bodyData.RemainsID.Count; j++)
                {
                    if (ConfirmParent.transform.GetChild(i).GetComponent<RemainController>().remainData ==
                        bodyData.RemainsID[j])
                    {
                        FindSame = true;
                    }
                }
                if (FindSame == false)
                {
                    passConfirm = false;
                }
            }
        }
        if (passConfirm == true)
        {
            for (int i = 0; i < ConfirmParent.transform.childCount; i++)
            {
                ConfirmParent.transform.GetChild(i).gameObject.SetActive(false);
            }
            GlobalData.Instance.BookBody[bodyData.BodyID-1].SetActive(false);
            GlobalData.Instance.BookAliveBody[bodyData.BodyID-1].SetActive(true);
        }
    }
}
