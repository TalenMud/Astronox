using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    

    public static QuestManager instance;
    

    public LTRect QuestRect;
   public Sprite mineIcon;
   public Sprite discoverIcon;
   public Sprite defeatIcon;
   public Sprite collectIcon;
   public Sprite lootIcon;
   public Sprite chestIcon;

   public List<Quest> activeQuests = new List<Quest>();

    public GameObject questUIPrefab; 
    public Transform questTab; 
    public GameObject questTabObj;

    private void Awake()
    {
        PlayerPrefs.DeleteKey("QuestData"); // DELETE BEFORE REAL GAME !!!!!!
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadQuests();
    }

    private void Start()
    {
    

       
        if (activeQuests.Count == 0)
        {
            InitializeQuests();
        }

        RefreshQuestUI();
        Canvas.ForceUpdateCanvases();
    }

    public void InitializeQuests()
    {
        AddQuest(new Quest("Q1P1", 1, QuestType.Loot));
        AddQuest(new Quest("Q2P1", 7, QuestType.Mine));
        AddQuest(new Quest("Q3P1", 1, QuestType.Loot));
        AddQuest(new Quest("Q4P1", 3, QuestType.Defeat));
        
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        SaveQuests();
    }

     private HashSet<string> completedAndShownQuests = new HashSet<string>(); 


    public void UpdateQuestProgress(string questID, int progress)
    {
        Debug.Log("UpdateQuestProgress called for questID: " + questID);
       Quest quest = activeQuests.Find(q => q.questID == questID);
            bool wasCompleted = quest.isCompleted; 
            quest.UpdateProgress(progress);

            if (quest.isCompleted && !wasCompleted)
            {
                completedAndShownQuests.Add(questID);
                RefreshQuestUI();
                DisplayQuest(quest); 
            }
    else if (!quest.isCompleted)
    {
        SaveQuests();
        RefreshQuestUI();
        DisplayQuest(quest);
    }
    }


public bool AllQuestsPlanet1Done(){
    List<Quest> p1Quests = activeQuests.FindAll(q => q.questID.Contains("P1"));
    return p1Quests.TrueForAll(q => q.isCompleted);

}
    private void RefreshQuestUI()
{
    Debug.Log("RefreshQuestUI called.");

    
    foreach (Transform child in questTab)
    {
        Destroy(child.gameObject);
    }

    questTabObj.SetActive(false); 
}

private IEnumerator DelayedRefresh()
{
    yield return new WaitForSeconds(7f);
    RefreshQuestUI();
    SaveQuests();
}

private void DisplayQuest(Quest quest)
{
    questTabObj.SetActive(true);

    GameObject questUI = Instantiate(questUIPrefab, questTab);
    QuestUI questUIComponent = questUI.GetComponent<QuestUI>();
    questUIComponent.ShowQuest(quest);
    completedAndShownQuests.Add(quest.questID);
    StartCoroutine(Wait(questTabObj));


    
    
}

IEnumerator Wait(GameObject ThingToHide)
    {
        
        yield return new WaitForSeconds(2f);

        ThingToHide.SetActive(false);
    }

public Sprite GetQuestIcon(QuestType questType)
{
    switch (questType)
    {
        case QuestType.Collect: return collectIcon;  
        case QuestType.Defeat: return defeatIcon;  
        case QuestType.Discover: return chestIcon;
        case QuestType.Mine: return mineIcon;  
        case QuestType.Loot: return lootIcon;
        default: return null;
    }
}
    public void SaveQuests()
    {
        string json = JsonUtility.ToJson(new QuestListWrapper(activeQuests));
        PlayerPrefs.SetString("QuestData", json);
        PlayerPrefs.Save();
        Debug.Log("Quests Saved!");
    }

    public void LoadQuests()
    {
        if (PlayerPrefs.HasKey("QuestData"))
        {
            string json = PlayerPrefs.GetString("QuestData");
            QuestListWrapper wrapper = JsonUtility.FromJson<QuestListWrapper>(json);
            activeQuests = wrapper.quests;
            Debug.Log("Quests Loaded!");
        }
    }
}


[System.Serializable]
public class QuestListWrapper
{
    public List<Quest> quests;

    public QuestListWrapper(List<Quest> quests)
    {
        this.quests = quests;
    }
}
