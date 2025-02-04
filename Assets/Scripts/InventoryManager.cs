using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Inventory inventory; // Reference to Inventory ScriptableObject

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        // Ensure inventory is assigned
        if (inventory == null)
        {
            Debug.LogWarning("Inventory reference is not assigned in the InventoryManager.");
        }
    }

    // Method to get an item by its name from the inventory
    public InventoryItem GetItemByName(string itemName)
    {
        // Make sure the inventory is not null
        if (inventory == null)
        {
            Debug.LogError("Inventory is null in InventoryManager.");
            return null;
        }

        foreach (InventoryItem item in inventory.items)
        {
            if (item != null && item.itemName == itemName) // Compare itemName instead of name
                return item;
        }
        return null; // If item is not found
    }

    // Method to add an item to the inventory (if not already present)
    public void AddItem(InventoryItem item)
    {
        if (item == null || inventory == null) return;

        // Check if the item is stackable and already exists in inventory (for stackable items)
        if (item.isStackable)
        {
            InventoryItem existingItem = GetItemByName(item.itemName);
            if (existingItem != null)
            {
                // Add the stack count (if stackable item already exists)
                // You might want to handle stack logic here, for now, let's just assume we're adding one more item
                // You can modify this if you want to handle real stack size
                Debug.Log("Item already in inventory, increasing stack.");
            }
            else
            {
                inventory.AddItem(item);  // Add new item if it doesn't exist
                Debug.Log(item.itemName + " added to inventory.");
            }
        }
        else
        {
            // For non-stackable items, just add the item
            inventory.AddItem(item);
            Debug.Log(item.itemName + " added to inventory.");
        }
    }
}
