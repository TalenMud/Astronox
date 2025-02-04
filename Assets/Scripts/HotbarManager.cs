using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager instance; // Singleton instance of HotbarManager
    public Image[] hotbarSlots; // UI Images for each hotbar slot (set in inspector)
    public int selectedSlot = 0; // Default selected slot (0, 1, or 2 for 3 slots)

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject); // Ensures there is only one instance of HotbarManager
    }

    private void Start()
    {
        UpdateHotbarUI(); // Update UI when game starts
    }

    private void Update()
    {
        HandleInput(); // Check for hotbar slot selection input
    }

    // Handles input for selecting slots (1, 2, 3 keys or key 1, 2, 3)
    private void HandleInput()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Select slot 1, 2, or 3
            {
                SelectSlot(i);
            }
        }
    }

    // Assigns an image (tool icon) to a slot
    public void AssignImageToSlot(int slotIndex, Sprite itemIcon)
    {
        if (slotIndex < 0 || slotIndex >= hotbarSlots.Length) return; // Ensure valid slot index

        hotbarSlots[slotIndex].sprite = itemIcon; // Assign image to the slot
        hotbarSlots[slotIndex].color = Color.white; // Ensure it's visible (not transparent)
    }

    // Select a hotbar slot (changes UI appearance)
    private void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex; // Update the selected slot
        UpdateHotbarUI(); // Refresh UI to reflect the selected slot
    }

    // Updates the hotbar UI, highlights the selected slot
    private void UpdateHotbarUI()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i == selectedSlot)
            {
                hotbarSlots[i].color = new Color(0.5f, 0.5f, 0.5f, 1f); // Highlight selected slot
                hotbarSlots[i].transform.localScale = new Vector2(1.2f, 1.2f); // Scale up selected slot
            }
            else
            {
                hotbarSlots[i].color = Color.white; // Normal slot color
                hotbarSlots[i].transform.localScale = Vector2.one; // Reset scale for non-selected slots
            }
        }
    }
}
