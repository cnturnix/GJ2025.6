using UnityEngine;
using System;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public enum TutorialStep
    {
        None,
        WaitFirstPickup,
        HighlightNewRelic,
        WaitFirstClick,
        HighlightCharacter,
        WaitFirstDrop,
        WaitFirstConfirm,
        Complete
    }

    public static TutorialManager I { get; private set; }

    [Header("Overlay Elements")]
    public GameObject overlayPanel;
    public RectTransform highlightFrame;
    public TMP_Text tipText;

    private TutorialStep step = TutorialStep.None;

    void Awake()
    {
        if (I == null) I = this; else Destroy(gameObject);
        overlayPanel.SetActive(false);
    }

    /// <summary>
    /// 开始教程，从第一次拾取触发
    /// </summary>
    public void StartTutorial(RectTransform firstItemSlot = null)
    {
        step = TutorialStep.WaitFirstPickup;
        overlayPanel.SetActive(true);
        tipText.text = "恭喜获得第一个遗物！";
        if (firstItemSlot != null)
        {
            ShowHighlightAt(firstItemSlot.position);
        }
    }

    public void OnFirstPickup(RectTransform itemSlotUI)
    {
        if (step != TutorialStep.WaitFirstPickup) return;
        step = TutorialStep.HighlightNewRelic;
        tipText.text = "拖动此遗物到左侧人物上试试！";
        ShowHighlightAt(itemSlotUI.position);
    }

    public void OnRelicClicked(RectTransform itemSlotUI)
    {
        if (step != TutorialStep.HighlightNewRelic) return;
        step = TutorialStep.HighlightCharacter;
        tipText.text = "现在点击角色头像看效果";
        ShowHighlightAt(itemSlotUI.position);
    }

    public void OnFirstDrop(RectTransform characterButtonUI)
    {
        if (step != TutorialStep.HighlightCharacter) return;
        step = TutorialStep.WaitFirstConfirm;
        tipText.text = "拖拽成功！最后点击 CONFIRM 完成";
        ShowHighlightAt(characterButtonUI.position);
    }

    public void OnFirstConfirm(RectTransform confirmButtonUI)
    {
        if (step != TutorialStep.WaitFirstConfirm) return;
        step = TutorialStep.Complete;
        HideOverlay();
        EventManager.Instance.TriggerEvent(EventType.BodyConfirmed, new BodyConfirmedEventArgs(1));

    }

    private void ShowHighlightAt(Vector2 screenPos)
    {
        highlightFrame.gameObject.SetActive(true);
        highlightFrame.position = screenPos;
        overlayPanel.SetActive(true);
    }

    private void HideOverlay()
    {
        overlayPanel.SetActive(false);
        highlightFrame.gameObject.SetActive(false);
    }
}