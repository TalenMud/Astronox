using UnityEngine;
[System.Serializable]
public class Quest
{
    // Basic quest information
    public string questID;            // The name of the quest (this could be icon-based instead of text)
    public bool isCompleted;            // Whether the quest is completed
    public int currentProgress;         // Current progress towards completing the quest
    public int requiredProgress;        // How much progress is required to complete the quest
    public QuestType questType;         // The type of the quest (e.g., Collect, Defeat, Discover)

    // Constructor to initialize a new quest
    public Quest(string name, int required, QuestType type)
    {
        questID = name;
        requiredProgress = required;
        questType = type;
        currentProgress = 0;
        isCompleted = false;
    }

    // Method to update progress (called when player completes an action related to the quest)
    public void UpdateProgress(int amount)
    {
        if (!isCompleted)
        {
            currentProgress += amount;  // Increment progress
            if (currentProgress >= requiredProgress)
            {
                currentProgress = requiredProgress;  // Cap progress at the required amount
                isCompleted = true;  // Mark the quest as completed
            }
        }
    }
}