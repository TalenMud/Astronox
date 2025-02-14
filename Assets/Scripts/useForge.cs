using UnityEngine;
using UnityEngine.UI;

public class useForge : MonoBehaviour
{
    private bool isInRange = false; 
    public SpriteRenderer forgeMessage; 
    public Inventory inventory;
    public InventoryItem launchpad;
    public string ForgeQuestID;
    void Start()
    {
        forgeMessage.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && inventory.FindQuantityOfItem("Copper Ore") >= 7)
        {
            Forge();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isInRange = true;
            forgeMessage.enabled = true; 
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            forgeMessage.enabled = false; 
        }
    }

    void Forge()
    {
        Debug.Log("forging");
        forgeMessage.enabled = false;
        QuestManager.instance.UpdateQuestProgress(ForgeQuestID, 1);
        
       // inventory.AddItem(launchpad, 2);
        
    }
}
