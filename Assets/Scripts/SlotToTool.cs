using UnityEngine;

public class SlotToTool : MonoBehaviour
{
    public Inventory inventory;
    public GameObject spaceGun;
    public GameObject Drill;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (HotbarManager.instance.selectedSlot == 1 && inventory.CheckIfHasItem("SpaceGun"))
    {
    spaceGun.SetActive(true);
    }
        else
    {
    spaceGun.SetActive(false);
    }

    if (HotbarManager.instance.selectedSlot == 0 && inventory.CheckIfHasItem("CopperDrill"))
    {
    Drill.SetActive(true);
    }
        else
    {
    Drill.SetActive(false);
    }
    }
}
