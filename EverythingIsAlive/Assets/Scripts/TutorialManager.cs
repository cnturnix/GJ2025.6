// using UnityEngine;
// using System;
// using TMPro;
//
// public class TutorialManager : MonoBehaviour
// {
//     public enum TutorialStep
//     {
//         None,
//         WaitFirstPickup,
//         HighlightNewRelic,
//         WaitFirstClick,
//         HighlightCharacter,
//         WaitFirstDrop,
//         WaitFirstConfirm,
//         Complete
//     }
//
//     public static TutorialManager I { get; private set; }
//
//     [Header("Overlay Elements")]
//     public GameObject overlayPanel;
//     public RectTransform highlightFrame;
//     public TMP_Text tipText;
//
//     private TutorialStep step = TutorialStep.None;
//
//     void Awake()
//     {
//         if (I == null) I = this;
//         else Destroy(gameObject);
//         overlayPanel.SetActive(false);
//     }
//
//     void Start()
//     {
//         // 订阅打开书本事件，触发教程开始
//         EventManager.Instance.AddListener<OpenBookEventArgs>(EventType.OpenBook, OnOpenBook);
//         Debug.Log("start");
//     }
//
//     void OnDestroy()
//     {
//         // 取消订阅
//
//         EventManager.Instance.RemoveListener<OpenBookEventArgs>(EventType.OpenBook, OnOpenBook);
//     }
//
//     // 收到 OpenBook 广播后执行
//     private void OnOpenBook(OpenBookEventArgs args)
//     {
//         Debug.Log("2");    
//         StartTutorial();
//     }
//
//     /// <summary>
//     /// 开始教程，从第一次拾取触发
//     /// </summary>
//     public void StartTutorial(RectTransform firstItemSlot = null)
//     {
//         Debug.Log("Tutorial Started");
//         step = TutorialStep.WaitFirstPickup;
//         overlayPanel.SetActive(true);
//         tipText.text = "恭喜获得第一个遗物！";
//         Debug.Log("1");
//         highlightFrame.gameObject.SetActive(firstItemSlot != null);
//         //if (firstItemSlot != null)
//         //{
//             ShowHighlightAt(firstItemSlot.position);
//           //  Debug.Log("1");
//         //}
//     }
//
//     public void OnFirstPickup(RectTransform itemSlotUI)
//     {
//         if (step != TutorialStep.WaitFirstPickup) return;
//         step = TutorialStep.HighlightNewRelic;
//         Debug.Log("2");
//         tipText.text = "拖动此遗物到左侧人物上试试！";
//         ShowHighlightAt(itemSlotUI.position);
//     }
//
//     public void OnRelicClicked(RectTransform itemSlotUI)
//     {
//         if (step != TutorialStep.HighlightNewRelic) return;
//         step = TutorialStep.HighlightCharacter;
//         Debug.Log("3");
//         tipText.text = "现在点击角色头像看效果";
//         ShowHighlightAt(itemSlotUI.position);
//     }
//
//     public void OnFirstDrop(RectTransform characterButtonUI)
//     {
//         if (step != TutorialStep.HighlightCharacter) return;
//         step = TutorialStep.WaitFirstConfirm;
//         Debug.Log("4");
//         tipText.text = "拖拽成功！最后点击 CONFIRM 完成";
//         ShowHighlightAt(characterButtonUI.position);
//     }
//
//     public void OnFirstConfirm(RectTransform confirmButtonUI)
//     {
//         if (step != TutorialStep.WaitFirstConfirm) return;
//         step = TutorialStep.Complete;
//         HideOverlay();
//         Debug.Log("Tutorial Complete");
//         // 发出教程结束广播，参数可改
//         EventManager.Instance.TriggerEvent(EventType.BodyConfirmed, new BodyConfirmedEventArgs(1));
//     }
//
//     private void ShowHighlightAt(Vector2 screenPos)
//     {
//         highlightFrame.gameObject.SetActive(true);
//         highlightFrame.position = screenPos;
//         overlayPanel.SetActive(true);
//     }
//
//     private void HideOverlay()
//     {
//         overlayPanel.SetActive(false);
//         highlightFrame.gameObject.SetActive(false);
//     }
// }