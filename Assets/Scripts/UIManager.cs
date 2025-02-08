using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject popupPanel; 
    public Image plusIcon;
    public Image icon; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowPopup(Sprite displayIcon)
    {
        plusIcon.gameObject.SetActive(true);
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