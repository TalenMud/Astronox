using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemUIPrefab; // Prefab for inventory UI item button
    public static InventoryManager instance;
    public Dictionary<string, int> inventory = new Dictionary<string, int>(); // Stores item names & quantities
    public GameObject inventoryDisplayCanvas; // Reference to the canvas where the inventory will be displayed
    private bool inventoryOpen = false;
    public Dictionary<string, Sprite> itemsIconsDictionary; // Maps item names to sprite icons
    private List<GameObject> itemButtons = new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        inventoryDisplayCanvas.SetActive(false); // Hide inventory at start
    }

    private void Start()
    {
        UpdateInventoryDisplay(); // Initialize UI
    }

    public void AddItem(InventoryItem newItem)
    {
        if (inventory.ContainsKey(itemSO.itemName))
    {
        inventory[itemSO.itemName]++; // Increase count
    }
    else
    {
        inventory[itemSO.itemName] = 1; // Add new item
        itemsIconsDictionary[itemSO.itemName] = itemSO.icon; // Store item icon
    }

    Debug.Log(itemSO.itemName + " added! Total: " + inventory[itemSO.itemName]);
    UpdateInventoryDisplay();
    }

    public bool HasItem(InventoryItem item)
    {
        return playerInventory.items.Contains(item);
    }

    private void UpdateInventoryDisplay()
    {
       // Clear existing buttons to avoid duplicates
        foreach (var button in itemButtons)
        {
            Destroy(button);
        }
        itemButtons.Clear();

        // Create a new button for each item
        foreach (var item in inventory)
        {
            GameObject newItemButton = Instantiate(itemUIPrefab, inventoryDisplayCanvas.transform);
            itemButtons.Add(newItemButton);

            // Set the item's name with quantity (e.g., "Iron Ore x2")
            Text itemText = newItemButton.GetComponentInChildren<Text>();
            if (itemText != null)
            {
                itemText.text = " x" + item.Value; // Example: "Iron Ore x2"
            }

            // Set the item's icon (if available)
            Sprite icon = itemsIconsDictionary[item.Key];
            Image itemImage = newItemButton.GetComponent<Image>();
            if (icon != null && itemImage != null)
            {
                itemImage.sprite = icon;
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
        inventoryDisplayCanvas.SetActive(inventoryOpen);
    }
}
