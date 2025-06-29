using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRemainData", menuName = "SO/RemainData")]
public class RemainData : ScriptableObject
{
    public int RemainID;//遗物ID
    public string RemainName;    //遗物名称
    public int BodyID;//目标角色ID
    public string RemainDesc;//遗物描述
    public Sprite RemainImage;//遗物图像
    public Sprite RemainICON;//遗物图标
}

