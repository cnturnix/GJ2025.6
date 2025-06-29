using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class CharacterButtonUI : MonoBehaviour, IDropHandler
{
    public Image portraitImage;
    public TMP_Text nameText;

    private CharacterData data;
    private CorpsesUIManager uiManager;
    private Button button;

    private List<RelicSlotUI> assignedRelicSlots = new List<RelicSlotUI>();

    public void Setup(CharacterData cd, CorpsesUIManager manager)
    {
        data = cd;
        uiManager = manager;
        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
        Refresh();
    }

    public void Refresh()
    {
        switch (data.state)
        {
            case CharacterState.Unclicked:
                portraitImage.gameObject.SetActive(false);
                nameText.text = "NOTFOUND";
                button.interactable = false;
                break;
            case CharacterState.Selected:
                portraitImage.gameObject.SetActive(true);
                portraitImage.sprite = data.corpseSprite;
                nameText.text = "CONFIRM";
                button.interactable = true;
                break;
            case CharacterState.Paired:
                portraitImage.gameObject.SetActive(true);
                portraitImage.sprite = data.portraitSprite;
                nameText.text = data.personName;
                button.interactable = false;
                break;
        }
    }

    private void OnClick()
    {
        if (data.state == CharacterState.Unclicked)
        {
            uiManager.OnCharacterSelected(data);
            Refresh();
        }
        else if (data.state == CharacterState.Selected)
        {
            ConfirmRelics();
        }
    }

    private void ConfirmRelics()
    {
        int expectedCount = uiManager.GetExpectedRelicCount(data);
        bool success = (assignedRelicSlots.Count == expectedCount);

        foreach (var slot in assignedRelicSlots)
        {
            slot.relicData.isAssignedToOriginalOwner = true;
            Destroy(slot.gameObject);
        }
        assignedRelicSlots.Clear();
        if (uiManager.inventoryUI != null)
        {
            uiManager.inventoryUI.RefreshUI();

            if (success)
            {
                uiManager.ConfirmCharacter(data);
                uiManager.RefreshCharacterPanel();
            }
        }
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        var slot = eventData.pointerDrag?.GetComponent<RelicSlotUI>();
        if (slot == null || data.state != CharacterState.Selected)
            return;

        if (slot.relicData.characterID == data.characterID)
        {
            assignedRelicSlots.Add(slot);
            slot.AcceptDrop(portraitImage.transform);
        }
    }
}


