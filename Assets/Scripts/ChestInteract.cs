using UnityEngine;
using UnityEngine.UI;

public class ChestInteract : MonoBehaviour
{
    public int chosenSlot;
    public InventoryItem loot;
    public SpriteRenderer chestMessage; 
    private bool isInRange = false; 
    private bool isOpened = false; 
    public Inventory inventory;

    public string ChestQuestID;

    void Start()
    {
        chestMessage.enabled = false; 
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && isOpened == false)
        {
            OpenChest();
        }
        if (!isInRange && isOpened == true){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // If the player enters the chest's trigger zone
        {
            isInRange = true;
            chestMessage.enabled = true; // Show "Press E to open" message
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            chestMessage.enabled = false; // Hide message when player moves away
        }
    }

    void OpenChest()
    {
        isOpened = true; 
        chestMessage.enabled = false;
        QuestManager.instance.UpdateQuestProgress(ChestQuestID, 1);
        
        inventory.AddItem(loot, chosenSlot);
        
    }

}
