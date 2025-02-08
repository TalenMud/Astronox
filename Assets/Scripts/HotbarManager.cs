using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager instance; 
    public Image[] hotbarSlots; 
    public int selectedSlot = 0; 
    public Image[] hotbarArrows;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject); 
    }

    private void Start()
    {
        UpdateHotbarUI();
    }

    private void Update()
    {
        HandleInput(); 
    }

    
    private void HandleInput()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) 
            {
                SelectSlot(i);
            }
        }
    }

    // Assigns an image (tool icon) to a slot
    public void AssignItemToSlot( Sprite itemIcon, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= hotbarSlots.Length) return; 
        hotbarSlots[slotIndex].sprite = itemIcon; 
        hotbarSlots[slotIndex].color = Color.white; 
        UpdateHotbarUI();
    }

    
    private void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex; 
        UpdateHotbarUI(); 
    }

    
    private void UpdateHotbarUI()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i == selectedSlot)
            {   hotbarArrows[i].enabled = true;
                hotbarSlots[i].color = new Color(0.7f, 0.7f, 0.7f, 1f); 
                hotbarSlots[i].transform.localScale = new Vector2(1.2f, 1.2f); 
            }
            else
            {   hotbarArrows[i].enabled = false;
                hotbarSlots[i].color = Color.white; 
                hotbarSlots[i].transform.localScale = Vector2.one; 
            }
        }
    }
}
