using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject popupPanel; // Reference to the UI Panel
    public TextMeshProUGUI popupText; // Reference to the text component

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowPopup(string message)
    {
        popupText.text = message; // Set the text
        popupPanel.SetActive(true); // Show popup
        StartCoroutine(HidePopupAfterDelay(2f)); // Hide after 2 seconds
    }

    private IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        popupPanel.SetActive(false); // Hide the popup
    }
}