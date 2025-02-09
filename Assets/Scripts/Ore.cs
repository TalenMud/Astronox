using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Ore : MonoBehaviour
{
    
    public InventoryItem oreItem; 
    public string oreName = "Copper Ore";
    public float timeToBreak = 1.5f; 
    public float miningRange = 10f; 
    private bool isBeingMined = false;
    private Coroutine miningCoroutine;
    private Transform player; 
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
            UIManager.instance.ShowMiningBar();
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
                UIManager.instance.HideMiningBar();
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
            float progress = timer / timeToBreak; 
            UIManager.instance.UpdateMiningProgress(progress);
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
            UIManager.instance.HideMiningBar();

    }

    private bool CanMineOre()
    {
        if (HotbarManager.instance == null || player == null) return false;

        bool isCloseEnough = (transform.position - player.position).sqrMagnitude <= miningRange * miningRange;
        bool hasDrillEquipped = HotbarManager.instance.selectedSlot == 0 && inventory.CheckIfHasItem("CopperDrill"); 

        return isCloseEnough && hasDrillEquipped;
    }
}
