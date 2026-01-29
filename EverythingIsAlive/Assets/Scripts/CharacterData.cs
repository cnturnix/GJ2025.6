using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Characters/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("基础信息")]
    public int characterID;          
    public string personName;           
    public string description;          

    [Header("图片资源")]
    public Sprite corpseSprite;         // 点击前（尸体）显示的图
    public Sprite portraitSprite;       // 配对后（生前）显示的图

    [Header("遗物列表")]
    public List<ItemData> relics;

    [Header("运行时状态")]
    public CharacterState state = CharacterState.Unclicked;
}

