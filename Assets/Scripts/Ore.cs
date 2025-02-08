using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Ore : MonoBehaviour
{
    
    public InventoryItem oreItem; // Ore item reference
    public string oreName = "Copper Ore";
    public float timeToBreak = 1.5f; // Mining duration
    public float miningRange = 10f; // Mining distance
    private bool isBeingMined = false;
    private Coroutine miningCoroutine;
    private Transform player; // Player reference
    public Inventory inventory;
    
    private Tilemap tilemap;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        tilemap = FindFirstObjectByType<Tilemap>();
    }

    public void StartMining()
    {
        if (CanMineOre() && !isBeingMined)
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
            if (!Input.GetMouseButton(0) || !CanMineOre()) 
            {
                isBeingMined = false;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        BreakOre();
    }

    private void BreakOre()
    {
            inventory.AddItem(oreItem, 1);
            QuestManager.instance.UpdateQuestProgress("Q2P1", 1);
            Destroy(gameObject);
            Vector3Int tilePosition = tilemap.WorldToCell(transform.position); 
            tilemap.SetTile(tilePosition, null); 

    }

    private bool CanMineOre()
    {
        if (HotbarManager.instance == null || player == null) return false;

        bool isCloseEnough = (transform.position - player.position).sqrMagnitude <= miningRange * miningRange;
        bool hasDrillEquipped = HotbarManager.instance.selectedSlot == 0; // Assuming drill is in slot 0

        return isCloseEnough && hasDrillEquipped;
    }
}
