using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class RocketCutscene : MonoBehaviour
{
    public Sprite chestIcon;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && QuestManager.instance.AllQuestsPlanet2Done() && SceneManager.GetActiveScene().name == "Planet_2")
        {
            SceneManager.LoadScene("Planet_3");
        }
        else if (other.gameObject.CompareTag("Player") && !QuestManager.instance.AllQuestsPlanet2Done() && SceneManager.GetActiveScene().name == "Planet_2")
        {
            UIManager.instance.ShowPopup(false, chestIcon);
        }
    }
}
