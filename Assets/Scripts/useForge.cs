using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;


public class useForge : MonoBehaviour
{
    private bool isInRange = false; 
    public SpriteRenderer forgeMessage; 
    public Inventory inventory;
    public InventoryItem launchpad;
    public string ForgeQuestID;
    public Animator animator;
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
        animator.SetBool("isForging", true);
        forgeMessage.enabled = false;
        QuestManager.instance.UpdateQuestProgress(ForgeQuestID, 1);
        
        inventory.AddItem(launchpad, 2);
        StartCoroutine(ResetForgeAnimation());
        
    }
    private IEnumerator ResetForgeAnimation()
    {
        yield return new WaitForSeconds(1.2f); 
        animator.SetBool("isForging", false);
    }
}
