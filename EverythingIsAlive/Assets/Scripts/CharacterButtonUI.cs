using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterButtonUI : MonoBehaviour
{
    [Header("UI 组件引用")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button button;      

    private CharacterData data;
    private CorpsesUIManager uiManager;

    public void Setup(CharacterData cd, CorpsesUIManager mgr)
    {
        data = cd;
        uiManager = mgr;

        // 第一次刷新，可能会用到上面那些引用
        Refresh();

        // 绑定点击事件
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    private void Refresh()
    {
        // 以下字段如果为 null，就会在这一行报 NRE
        switch (data.state)
        {
            case CharacterState.Unclicked:
                portraitImage.sprite = null;
                portraitImage.color = new Color(1, 1, 1, 0);
                nameText.text = "NOTFOUND";
                button.interactable = false;
                break;

            case CharacterState.Selected:
                portraitImage.sprite = data.corpseSprite;
                portraitImage.color = Color.white;
                nameText.text = "CONFIRM";
                button.interactable = true;
                break;

            case CharacterState.Paired:
                portraitImage.sprite = data.portraitSprite;
                portraitImage.color = Color.white;
                nameText.text = data.personName;
                button.interactable = false;
                break;
        }
    }

    private void OnClick()
    {
        uiManager.OnCharacterClicked(data);
        Refresh();
    }
}

