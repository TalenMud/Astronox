using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingRange : MonoBehaviour
{

 public Inventory inventory;
 public Sprite sniperIcon;
 private bool inrange = false;

public GameObject interactIcon;
    void Update()
    {
        if (inrange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("ShootingRange");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && inventory.CheckIfHasItem("Sniper"))
        {
            interactIcon.SetActive(true);
            inrange = true;
        }
        else if (other.gameObject.CompareTag("Player") && !inventory.CheckIfHasItem("Sniper"))
        {
            interactIcon.SetActive(false);
            UIManager.instance.ShowPopup(false, sniperIcon);
        }
    }
}
