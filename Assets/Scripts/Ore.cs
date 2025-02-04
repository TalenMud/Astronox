using UnityEngine;
using System.Collections;

public class Ore : MonoBehaviour
{

    public InventoryItem oreItem;
    public InventoryManager inventoryManager;
    public string oreName = "Copper Ore";
    public float timeToBreak = 2f; // Time required to break the ore
    public float miningRange = 10f; // How close the player needs to be
    private bool isBeingMined = false;
    private Coroutine miningCoroutine;
    private Transform player; // Reference to the player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player
    }

    public void StartMining()
{       
    if (CanMineOre() && !isBeingMined) // Check if player has drill & is close enough
    {

        isBeingMined = true;
        miningCoroutine = StartCoroutine(MineOre());
    }
}

    private IEnumerator MineOre()
{
    float timer = 0f;

    while (timer < timeToBreak)
    {
        if (!Input.GetMouseButton(0) || !CanMineOre()) // Stop if player moves or releases mouse
        {
            isBeingMined = false;
            yield break;
            // broken ore
        }

        timer += Time.deltaTime;
        yield return null;
    }

    BreakOre();
}

    private void BreakOre()
    {

        // Add to inventory
        inventoryManager.AddItem(oreItem);
        // Show UI popup
        UIManager.instance.ShowPopup("You mined " + oreName + "!");

        Destroy(gameObject);
    }
    

    private bool CanMineOre()
{   
    // Check if HotbarManager exists first (to prevent errors)
    if (HotbarManager.instance == null) return false;

        bool isCloseEnough = (transform.position - player.position).sqrMagnitude <= miningRange * miningRange;
        if (HotbarManager.instance == null) return false; // Prevent errors if missing
        bool hasDrillEquipped = HotbarManager.instance.selectedSlot == 0;

    return isCloseEnough && hasDrillEquipped;
}
}