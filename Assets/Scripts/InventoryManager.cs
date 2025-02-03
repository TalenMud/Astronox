using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemUIPrefab; // Prefab for inventory UI item button
    public static InventoryManager instance;
    public List<string> inventory = new List<string>(); // Stores item names
    public GameObject inventoryDisplayCanvas; // Reference to the canvas where the inventory will be displayed
    private bool inventoryOpen = false;
    public Dictionary<string, Sprite> itemsIconsDictionary; // Dictionary to map item names to sprite icons
    private List<GameObject> itemButtons = new List<GameObject>(); // Keep track of created UI buttons to manage them

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }

        itemsIconsDictionary = new Dictionary<string, Sprite>(); // Initialize dictionary
    }

    private void Start()
    {
        inventoryDisplayCanvas.SetActive(false);
        AddItem("Copper Drill", Resources.Load<Sprite>("Items/CopperDrillIcon")); // Load an icon from resources
        UpdateInventoryDisplay(); // Initialize the inventory display
    }

    public void AddItem(string itemName, Sprite icon)
    {
        if (!inventory.Contains(itemName))
        {
            inventory.Add(itemName);
            itemsIconsDictionary[itemName] = icon; // Store item name to sprite mapping
            Debug.Log(itemName + " added to inventory!");
            UpdateInventoryDisplay(); // Refresh the display
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    private void UpdateInventoryDisplay()
    {
        // Clear existing buttons to avoid duplicates
        foreach (var button in itemButtons)
        {
            Destroy(button); // Destroy old buttons before updating
        }
        itemButtons.Clear(); // Clear the list of buttons

        // Create a new button for each item
        foreach (var item in inventory)
        {
            GameObject newItemButton = Instantiate(itemUIPrefab, inventoryDisplayCanvas.transform); // Assuming itemUIPrefab is the UI element for displaying items
            itemButtons.Add(newItemButton); // Add the button to the list

            // Set the item's name
            Text itemText = newItemButton.GetComponentInChildren<Text>(); // Assuming there's a Text component in the prefab
            if (itemText != null)
            {
                itemText.text = item;
            }

            // Set the item's icon (if it has one)
            Sprite icon = itemsIconsDictionary[item];
            Image itemImage = newItemButton.GetComponent<Image>(); // Assuming there's an Image component in the prefab
            if (icon != null && itemImage != null)
            {
                itemImage.sprite = icon; // Assign item icon to button
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;

        // Show or hide the inventory display based on whether it's open or not
        inventoryDisplayCanvas.SetActive(inventoryOpen);
    }
}
