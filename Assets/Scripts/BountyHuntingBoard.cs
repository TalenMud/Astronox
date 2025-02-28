using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BountyHuntingBoard : MonoBehaviour
{
    public GameObject interactMessage;
    private bool isInRange = false; 
    public Inventory inventory;

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E)){
            SaveData.instance.SaveGame();
            SceneManager.LoadScene("BountyHuntMenu");
        }
    }

   void OnTriggerEnter2D(Collider2D other)
   {
    if (other.gameObject.CompareTag("Player"))
    {
        isInRange = true;
        interactMessage.SetActive(true);
    }

   }

   void OnTriggerExit2D(Collider2D other)
   {
    if (other.gameObject.CompareTag("Player"))
    {
        isInRange = false;
        interactMessage.SetActive(false);
    }
   }
}
