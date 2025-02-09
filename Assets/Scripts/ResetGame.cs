using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetGame : MonoBehaviour
{
    public Inventory inventory;

    public void BackToStart()
    {
        while (inventory.items.Count > 0)
    {
        inventory.RemoveItem(inventory.items[0]);
    }
        SceneManager.LoadScene("Planet_1");
    }
}
