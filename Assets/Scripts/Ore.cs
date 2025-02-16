using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Ore : MonoBehaviour
{
    private static bool isAnyOreMining = false; 
    private float lastMiningTime = 0f;
    private float miningCooldown = 1f;
    public InventoryItem oreItem; 
    public string oreName = "Copper Ore";
    public float timeToBreak = 1.5f; 
    public float miningRange = 10f; 
    private bool isBeingMined = false;
    private Coroutine miningCoroutine;
    private Transform player; 
    public Inventory inventory;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }


    public void StartMining()
    {
        if (Time.time - lastMiningTime < miningCooldown || isAnyOreMining)
        {
            return;
        }

        if (CanMineOre() && !isBeingMined)
        {
            isAnyOreMining = true;
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
                isAnyOreMining = false;
                UIManager.instance.HideMiningBar();
                miningCoroutine = null;
                yield break;
            }

            timer += Time.deltaTime;
            float progress = timer / timeToBreak;
            UIManager.instance.UpdateMiningProgress(progress);
            yield return null;
        }

        BreakOre();
    }

    private void BreakOre()
    {
        lastMiningTime = Time.time;
        inventory.AddItem(oreItem, 1);
        QuestManager.instance.UpdateQuestProgress("Q2P1", 1);
        isAnyOreMining = false;
        Destroy(gameObject);
        UIManager.instance.HideMiningBar();
    }

    private bool CanMineOre()
    {
        if (HotbarManager.instance == null || player == null) return false;

        bool isCloseEnough = (transform.position - player.position).sqrMagnitude <= miningRange * miningRange;
        bool hasDrillEquipped = HotbarManager.instance.selectedSlot == 0 && inventory.CheckIfHasItem("CopperDrill"); 

        return isCloseEnough && hasDrillEquipped;
    }

    private void OnDestroy()
    {
        if (miningCoroutine != null)
        {
            StopCoroutine(miningCoroutine);
            UIManager.instance.HideMiningBar();
        }
    }
}