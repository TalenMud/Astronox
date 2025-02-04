using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    

    public static QuestManager instance;
    
    mineIcon = Resources.Load<Sprite>("Icons/mineIcon");
    discoverIcon = Resources.Load<Sprite>("Icons/discoverIcon");
    defeatIcon = Resources.Load<Sprite>("Icons/defeatIcon"); 
    collectIcon = Resources.Load<Sprite>("Icons/collectIcon");
    lootIcon = Resources.Load<Sprite>("Icons/lootIcon");

    public List<Quest> activeQuests = new List<Quest>();

    [Header("UI Elements")]
    public GameObject questUIPrefab; 
    public Transform questPanel; 

    private void Awake()
    {
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
        // If no saved quests, set initial quests
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
    // Clear existing UI elements
    foreach (Transform child in questPanel)
    {
        Destroy(child.gameObject);
    }

    // Create UI elements for each active quest
    foreach (Quest quest in activeQuests)
    {
        // Instantiate the quest prefab
        GameObject questUI = Instantiate(questUIPrefab, questPanel);

        // Get references to the UI elements
        Image icon = questUI.transform.Find("QuestIcon").GetComponent<Image>(); // Quest icon
        Image progressBar = questUI.transform.Find("CompletionLevel").GetComponent<Image>(); // Progress bar
        Image checkmark = questUI.transform.Find("Done_image").GetComponent<Image>(); // Checkmark

        // Set the icon based on the quest type
        icon.sprite = GetQuestIcon(quest.questType);

        // Set progress bar values
        progressBar.fillAmount = quest.requiredProgress / quest.currentProgress;

        // Show checkmark if the quest is completed
        checkmark.enabled = quest.isCompleted;
    }
}


private Sprite GetQuestIcon(QuestType questType)
{
    switch (questType)
    {
        case QuestType.Collect: return coinIcon;  // You can assign the coin icon here
        case QuestType.Defeat: return enemyIcon;  // Assign the enemy icon here
        case QuestType.Discover: return chestIcon;
        case QuestType.Mine: return mineIcon;  // Assign the chest icon here
        case questType.Loot: return lootIcon;
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

// Wrapper class for serializing lists in JSON
[System.Serializable]
public class QuestListWrapper
{
    public List<Quest> quests;

    public QuestListWrapper(List<Quest> quests)
    {
        this.quests = quests;
    }
}
