using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    
    public Image[] hotbarSlots; // UI Images for each slot
    public Sprite[] itemIcons; // Item images/icons
    private int selectedSlot = 0;

    void Start()
    {
        UpdateHotbarUI();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Check for number key presses (1-6)
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }
    }

    void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex;
        Debug.Log("Selected Slot: " + (selectedSlot + 1));
        UpdateHotbarUI();
    }

    void UpdateHotbarUI()
    {
        // Highlight the selected slot
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i].color = (i == selectedSlot) ? Color.yellow : Color.white;
        }
    }

    public void AssignItemToSlot(int slotIndex, Sprite itemIcon)
{
    // Check if the slot is already occupied
    if (hotbarSlots[slotIndex].sprite != null) 
    {
        Debug.Log("Slot " + (slotIndex + 1) + " already has an item!");
        return; // Don't assign the item if the slot is already filled
    }

    // If slot is empty, assign the item to the slot
    hotbarSlots[slotIndex].sprite = itemIcon;
    Debug.Log("Item added to slot " + (slotIndex + 1));
}

public void UseItem(int slotIndex)
    {
        Debug.Log("Using item in slot " + (slotIndex + 1));
        // Implement item use logic (e.g., consume potion, equip weapon)
    }

}
