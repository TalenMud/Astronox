using UnityEngine;
using UnityEngine.UI;

public class ChestInteract : MonoBehaviour
{
    public GameObject chestContentsPanel; // UI Panel showing chest contents
    public SpriteRenderer chestMessage; // UI Text that shows "Press E to open" when nearby
    private bool isInRange = false; // Player proximity flag
    private bool isOpened = false; // Check if the chest is already opened
    public GameObject[] chestItems;

    void Start()
    {
        chestContentsPanel.SetActive(false); // Hide chest contents UI initially
        chestMessage.enabled = false; // Hide message initially
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleChest();
        }
        if (!isInRange){
            chestMessage.enabled = false;
            chestContentsPanel.SetActive(false);
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
        chestContentsPanel.SetActive(true); // Show chest contents panel
        chestMessage.enabled = false;
        
        // Display items inside the chest
        foreach (var item in chestItems)
        {
            item.SetActive(true); // Or instantiate the item prefab, for example
            Debug.Log("Item added: " + item.name);
        }
    }

    void CloseChest(){

        isOpened = false;
        chestContentsPanel.SetActive(false);

    }

        public void ToggleChest()
    {
        if (isOpened)
        {
            CloseChest();
        }
        else
        {
            OpenChest();
        }
    }
}
