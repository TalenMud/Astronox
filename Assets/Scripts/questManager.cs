using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("UI Elements")]
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
    

       
        if (activeQuests.Count == 0)
        {
            InitializeQuests();
        }

        RefreshQuestUI();
    }

    public void InitializeQuests()
    {
        // Add starting quests
        AddQuest(new Quest("Q1P1", 1, QuestType.Loot));
        AddQuest(new Quest("Q2P1", 5, QuestType.Mine));
        AddQuest(new Quest("Q3P1", 3, QuestType.Defeat));
        
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        SaveQuests();
        RefreshQuestUI();
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

    private void RefreshQuestUI()
{
    
    foreach (Transform child in questPanel)
    {
        Destroy(child.gameObject);
    }

   
    foreach (Quest quest in activeQuests)
    {
        
        GameObject questUI = Instantiate(questUIPrefab, questPanel);

        
        Image icon = questUI.transform.Find("QuestIcon").GetComponent<Image>(); 
        Image progressBar = questUI.transform.Find("CompletionLevel").GetComponent<Image>(); 
        Image checkmark = questUI.transform.Find("Done_image").GetComponent<Image>(); 

        
        icon.sprite = GetQuestIcon(quest.questType);

        
        progressBar.fillAmount = quest.currentProgress / quest.requiredProgress;

        
        checkmark.enabled = quest.isCompleted;
    }
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
