using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject placementArrow;
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

    public void Update()
    {
        if (inventory.CheckIfHasItem("CopperLaunchpad"))
        {
            placementArrow.SetActive(true);
        }
        else
        {
            placementArrow.SetActive(false);
        }
    }
    
}
