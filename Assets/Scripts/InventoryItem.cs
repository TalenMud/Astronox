using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string description;
    public bool isTool;
    public string type;
    public bool isStackable = true;  // Default to stackable
    public int stackCount = 1;       // To track the number of stackable items
}

