using UnityEngine;

public class HidePanel : MonoBehaviour
{
    public GameObject panelToHide; // Reference to the Panel you want to hide

    // This function will be called when the button is clicked
    public void ClosePanel()
    {
        panelToHide.SetActive(false); // Disables the panel
    }
}
