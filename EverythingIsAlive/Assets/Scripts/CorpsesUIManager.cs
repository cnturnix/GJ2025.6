using UnityEngine;
using System.Collections.Generic;

public class CorpsesUIManager : MonoBehaviour
{
    [Header("人物数据列表 (SO)")]
    public List<CharacterData> allCharacters;

    [Header("UI 预制体 & 容器")]
    public GameObject characterButtonPrefab;
    public Transform characterPanel;   // 左侧放按钮的容器

    void Start()
    {
        RefreshCharacterPanel();
    }

    public void RefreshCharacterPanel()
    {
        Debug.Log($"[Refresh] allCharacters.Count = {allCharacters?.Count}");
        foreach (var cd in allCharacters)
            Debug.Log($"-- CharacterID: {cd.characterID}");
        // 1. 清空容器
        foreach (Transform child in characterPanel)
        {
            Destroy(child.gameObject);
        }

        // 2. 逐个 Instantiate 并 Setup
        foreach (var cd in allCharacters)
        {
            var go = Instantiate(characterButtonPrefab, characterPanel);
            var btnUI = go.GetComponent<CharacterButtonUI>();
            btnUI.Setup(cd, this);
        }
    }

    // 从 Unclicked -> Selected
    public void OnCharacterClicked(CharacterData cd)
    {
        if (cd.state == CharacterState.Unclicked)
        {
            cd.state = CharacterState.Selected;
        }
    }

    // 在 Confirm 操作后，被调用来完成配对
    public void ConfirmCharacter(CharacterData cd)
    {
        if (cd.state == CharacterState.Selected)
        {
            cd.state = CharacterState.Paired;
            // TODO: 更新下方详情 & 拖拽逻辑
        }
    }
}
