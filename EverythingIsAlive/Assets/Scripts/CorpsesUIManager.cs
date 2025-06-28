using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CorpsesUIManager : MonoBehaviour
{
    public List<CharacterData> allCharacters;
    public GameObject characterButtonPrefab;
    public Transform characterPanel;

    public InventoryUI inventoryUI;
    public PlayerInventory inventory;
    public CharacterData currentCharacter; // 之前的 currentCorpse 改为 currentCharacter

    private Dictionary<CharacterData, CharacterButtonUI> characterUIMap = new Dictionary<CharacterData, CharacterButtonUI>();

    void Start()
    {
        RefreshCharacterPanel();
    }

    public void RefreshCharacterPanel()
    {
        foreach (Transform t in characterPanel)
            Destroy(t.gameObject);
        characterUIMap.Clear();

        foreach (var cd in allCharacters)
        {
            var go = Instantiate(characterButtonPrefab, characterPanel);
            var ui = go.GetComponent<CharacterButtonUI>();
            ui.Setup(cd, this);
            characterUIMap[cd] = ui;
        }
    }

    public void OnCharacterSelected(CharacterData cd)
    {
        if (cd.state == CharacterState.Unclicked)
        {
            cd.state = CharacterState.Selected;
            characterUIMap[cd].Refresh();
        }
    }

    public void ConfirmCharacter(CharacterData cd)
    {
        if (cd.state == CharacterState.Selected)
        {
            cd.state = CharacterState.Paired;
            characterUIMap[cd].Refresh();
        }
    }

    public int GetExpectedRelicCount(CharacterData cd)
    {
        if (currentCharacter == null || currentCharacter.relics == null)
            return 0;
        return currentCharacter.relics.Count(r => r.characterID == cd.characterID);
    }
}
