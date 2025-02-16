using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{

    public RectTransform questPanel; 
    public Image progressBar;
    
    public void ShowQuest(Quest quest)
    {
        GameObject questPanelObject = GameObject.FindGameObjectWithTag("QuestPanel");
        RectTransform questPanel = questPanelObject.GetComponent<RectTransform>();
        questPanel.pivot = new Vector2(0, 0.5f);
        questPanel.anchorMin = new Vector2(0, 0.5f);
        questPanel.anchorMax = new Vector2(0, 0.5f);
        Image icon = transform.Find("QuestIcon").GetComponent<Image>();
        Image progressBar = transform.Find("CompletionLevel").GetComponent<Image>();
        Image checkmark = transform.Find("Done_image").GetComponent<Image>();
        checkmark.enabled = false;
        icon.sprite = QuestManager.instance.GetQuestIcon(quest.questType);
        questPanel.anchoredPosition = new Vector2(-questPanel.rect.width, questPanel.anchoredPosition.y); // Start off-screen

        // Store reference to this component
        QuestUI thisQuestUI = this;

        questPanel.DOAnchorPos(new Vector2(0, questPanel.anchoredPosition.y), 0.5f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // Check if object still exists
                if (thisQuestUI == null) return;

                Debug.Log(quest.portionDone);
                progressBar.DOFillAmount(quest.portionDone, 1f)
                    .OnComplete(() =>
                    {
                        // Check if object still exists
                        if (thisQuestUI == null) return;

                        if (quest.isCompleted)
                        {
                            checkmark.enabled = true;
                        }

                        DOVirtual.DelayedCall(2.5f, () =>
                        {
                            // Check if object still exists before destroying
                            if (thisQuestUI != null)
                            {
                                Destroy(gameObject);
                            }
                        });
                    });
            });
    }
}
