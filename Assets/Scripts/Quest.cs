using UnityEngine;
using System;
[System.Serializable]
public class Quest
{
    
    public string questID;            
    public bool isCompleted;            
    public float currentProgress;         
    public float requiredProgress;        
    public QuestType questType;    
    public float portionDone;    

    
    public Quest(string name, int required, QuestType type)
    {
        questID = name;
        requiredProgress = required;
        questType = type;
        currentProgress = 0;
        isCompleted = false;
        portionDone = 0;
    }

    
    public void UpdateProgress(int amount)
    {
        if (!isCompleted)
        {
            currentProgress += amount;  
            portionDone = (currentProgress/requiredProgress);
            if (currentProgress >= requiredProgress)
            {
                currentProgress = requiredProgress;  
                isCompleted = true;  
            }
        }
    }
}