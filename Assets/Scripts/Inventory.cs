using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(InventoryItem item, int slot)
    {
        if (item.isStackable)
        {
            // Check if the item already exists in the inventory
            foreach (InventoryItem existingItem in items)
            {
                if (existingItem.itemName == item.itemName)
                {
                    existingItem.stackCount += item.stackCount; // Stack items together
                    Debug.Log(item.itemName + " stacked in inventory!");
                    return;
                }
            }
        }
        // If it's not stackable or not found, add as a new item
        items.Add(item);
        HotbarManager.instance.AssignItemToSlot(item.itemIcon, slot);
    }

    public void RemoveItem(InventoryItem item)
    {
        if (items.Contains(item))
        {
            if (item.isStackable && item.stackCount > 1)
            {
                item.stackCount -= 1; // Decrease stack count instead of removing the item
                Debug.Log(item.itemName + " stack decremented!");
            }
            else
            {
                items.Remove(item); // If it's not stackable or stack count is 1, remove the item
                Debug.Log(item.itemName + " removed from inventory.");
            }
        }
    }
    public bool CheckIfHasItem(string itemName)
    {
        foreach (InventoryItem item in items)
        {
            if (item.itemName == itemName){ // Compare itemName instead of name
                return true;
            }
        
        }
        return false;
    }

}