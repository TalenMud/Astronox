using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{

    public RectTransform questPanel; 
    public Image progressBar;
    
    public void ShowQuest(float fillAmount)
    {
        GameObject questPanelObject = GameObject.FindGameObjectWithTag("QuestPanel");
        RectTransform questPanel = questPanelObject.GetComponent<RectTransform>();
        Image progressBar = questPanel.GetComponentInChildren<Image>();
        // Slide in from the left
        questPanel.anchoredPosition = new Vector2(-questPanel.rect.width, questPanel.anchoredPosition.y); // Start off-screen
        questPanel.DOAnchorPos(new Vector2(0, questPanel.anchoredPosition.y), 0.5f) // Slide in over 0.5 seconds
            .SetEase(Ease.OutBack) // Use a nice easing effect
            .OnComplete(() =>
            {
                // Fill progress bar
                progressBar.fillAmount = 0; // Start at 0
                progressBar.DOFillAmount(fillAmount, 1.5f) // Fill over 1.5 seconds
                    .OnComplete(() =>
                    {
                        // Stay on screen for 2 seconds
                        DOVirtual.DelayedCall(2f, () => {
                            // You can add code here to hide the quest or move to the next quest
                        });
                    });
            });
    }
}
