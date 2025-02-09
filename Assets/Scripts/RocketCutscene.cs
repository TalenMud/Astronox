using UnityEngine;
using UnityEngine.Playables;

public class RocketCutscene : MonoBehaviour
{
        public PlayableDirector cutscene; // Drag your cutscene Timeline here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && QuestManager.instance.AllQuestsPlanet1Done())
        {
            cutscene.Play();
        }
    }

}
