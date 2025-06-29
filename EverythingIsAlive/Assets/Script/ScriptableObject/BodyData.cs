using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBodyData", menuName = "SO/BodyData")]
public class BodyData : ScriptableObject
{
    [Header("基础信息")]
    public int BodyID;          

    [Header("目标遗物列表")]
    public List<RemainData> RemainsID;
}

