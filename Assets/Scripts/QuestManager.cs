using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    

    public static QuestManager instance;
    
    
   public Sprite mineIcon;
   public Sprite discoverIcon;
   public Sprite defeatIcon;
   public Sprite collectIcon;
   public Sprite lootIcon;
   public Sprite chestIcon;

   public List<Quest> activeQuests = new List<Quest>();

    public GameObject questUIPrefab; 
    public Transform questPanel; 

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
    

       
        if (activeQuests.Count == 0 && SceneManager.GetActiveScene().name == "Planet_1")
        {
            InitializeQuests();
        }

        RefreshQuestUI();
        Canvas.ForceUpdateCanvases();
    }

    public void InitializeQuests()
    {
        // Add starting quests
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

    public void UpdateQuestProgress(string questID, int progress)
    {
        Quest quest = activeQuests.Find(q => q.questID == questID);
        if (quest != null)
        {
            quest.UpdateProgress(progress);
            SaveQuests();
            RefreshQuestUI();
  
        }
    }


public bool AllQuestsPlanet1Done(){
    List<Quest> p1Quests = activeQuests.FindAll(q => q.questID.Contains("P1"));
    return p1Quests.TrueForAll(q => q.isCompleted);

}
    private void RefreshQuestUI()
{
   
    
    foreach (Transform child in questPanel)
    {
        Destroy(child.gameObject);
    }

    List<Quest> incompleteQuests = new List<Quest>();
    List<Quest> completedQuests = new List<Quest>();

    
    foreach (Quest quest in activeQuests)
    {
        if (quest.isCompleted)
        {
            completedQuests.Add(quest);
        }
        else
        {
            incompleteQuests.Add(quest);
        }
    }

    int maxQuests = Mathf.Min(1, incompleteQuests.Count + completedQuests.Count);

    
    for (int i = 0; i < Mathf.Min(1, incompleteQuests.Count); i++)
    {
        DisplayQuest(incompleteQuests[i]);
    }

    
    for (int i = 0; i < maxQuests - incompleteQuests.Count; i++)
    {
        DisplayQuest(completedQuests[i]);
    }
}

private void DisplayQuest(Quest quest)
{
    GameObject questUI = Instantiate(questUIPrefab, questPanel);

    Image icon = questUI.transform.Find("QuestIcon").GetComponent<Image>();
    Image progressBar = questUI.transform.Find("CompletionLevel").GetComponent<Image>();
    Image checkmark = questUI.transform.Find("Done_image").GetComponent<Image>();
    icon.sprite = GetQuestIcon(quest.questType);
    checkmark.enabled = quest.isCompleted;
    progressBar.fillAmount = quest.portionDone;

    questUI.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f); 
    LeanTween.scale(questUI, new Vector3(1.7f, 1.7f, 1.7f), 3f).setEase(LeanTweenType.easeOutBack); 

    if (quest.isCompleted)
    {
        LeanTween.scale(questUI, Vector3.zero, 1.5f);
            Destroy(questUI);
            RefreshQuestUI(); 

    }
    LeanTween.scale(questUI, Vector3.zero, 1.5f);

}
    

private Sprite GetQuestIcon(QuestType questType)
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
