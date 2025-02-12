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
        bool stacked = false;

        foreach (InventoryItem existingItem in items)
        {
            if (existingItem.itemName == item.itemName)
            {
                existingItem.stackCount += item.stackCount;
                UIManager.instance.ShowPopup(item.itemIcon);
                stacked = true; 
                break; 
            }
        }

        if (!stacked) 
        {
            items.Add(item);
        }
    }
    else 
    {
        items.Add(item);
    }

    if (item.isTool)
    {
        HotbarManager.instance.AssignItemToSlot(item.itemIcon, slot);
        UIManager.instance.ShowPopup(item.itemIcon);
    }
    }

    public void RemoveItem(InventoryItem item)
    {
        if (items.Contains(item))
        {
            if (item.isStackable && item.stackCount > 1)
            {
                item.stackCount -= 1; 
            }
            else
            {
                items.Remove(item); 
            }
        }
    }
    public bool CheckIfHasItem(string itemName)
    {
        foreach (InventoryItem item in items)
        {
            if (item.itemName == itemName){
                return true;
            }
        
        }
        return false;
    }

    public int FindQuantityOfItem(string itemName)
    {
        int quantity = 0;
        foreach (InventoryItem item in items)
        {
            if (item.itemName == itemName)
            {
                quantity += item.stackCount; 
                if(!item.isStackable){
                    quantity++; 
                }

            }
        }
        return quantity;
    }

}