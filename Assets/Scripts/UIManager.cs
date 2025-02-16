using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator drillAnimator;
    public Image miningBar;
    public Image miningBarBG;
    public static UIManager instance;
    public GameObject popupPanel; 
    public Image plusIcon;
    public Image icon; 
    public Image crossIcon;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

   public void ShowMiningBar()
{
    drillAnimator.SetBool("IsMining", true);
    miningBar.gameObject.SetActive(true);
    miningBarBG.gameObject.SetActive(true);
}

public void HideMiningBar()
{
    drillAnimator.SetBool("IsMining", false);
    miningBar.gameObject.SetActive(false);
    miningBarBG.gameObject.SetActive(false);
}
public void UpdateMiningProgress(float progress)
{
    miningBar.fillAmount = progress;
}
    public void ShowPopup( bool isPlus, Sprite displayIcon)
    {
        if (isPlus)
        {
            plusIcon.gameObject.SetActive(true);
            crossIcon.gameObject.SetActive(false);
        }
        else
        {
            plusIcon.gameObject.SetActive(false);
            crossIcon.gameObject.SetActive(true);
        }
        icon.sprite = displayIcon; 
        icon.gameObject.SetActive(true);
        popupPanel.SetActive(true);
        StartCoroutine(HidePopupAfterDelay(2f)); 
    }

    private IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        plusIcon.gameObject.SetActive(false);
        popupPanel.SetActive(false);
        icon.sprite = null;
        icon.gameObject.SetActive(true);
    }
}