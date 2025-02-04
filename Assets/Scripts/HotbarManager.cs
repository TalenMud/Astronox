using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager instance;
    public Image[] hotbarSlots; // UI Images for each slot
    public Sprite[] itemIcons; // Item images/icons
    public int selectedSlot = 0;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject); // Prevent duplicate instances
    }


    void Start()
    {
        AssignItemToSlot(0, itemIcons[0]);
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
        UpdateHotbarUI();
    }

    void UpdateHotbarUI()
    {
        // Highlight the selected slot
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
         if (i == selectedSlot)
        {
            hotbarSlots[i].color = new Color(0.5f, 0.5f, 0.5f, 1f); 
            hotbarSlots[i].transform.localScale = new Vector2(1.2f, 1.2f); // Increase size
        }
        else
        {
            hotbarSlots[i].color = Color.white;
            hotbarSlots[i].transform.localScale = Vector2.one; // Reset size
        }
        }
    }

    public void AssignItemToSlot(int slotIndex, Sprite itemIcon)
{
    // Check if the slot is already occupied
    if (hotbarSlots[slotIndex].sprite != null) 
    {
        UIManager.instance.ShowPopup("Slot " + (slotIndex + 1) + " already has an item!");
        return; // Don't assign the item if the slot is already filled
    }

    // If slot is empty, assign the item to the slot
    hotbarSlots[slotIndex].sprite = itemIcon;
    UIManager.instance.ShowPopup("Item added to slot " + (slotIndex + 1));
}

public void UseItem(int slotIndex)
    {
        UIManager.instance.ShowPopup("Using item in slot " + (slotIndex + 1));
        // Implement item use logic (e.g., consume potion, equip weapon)
    }

}

