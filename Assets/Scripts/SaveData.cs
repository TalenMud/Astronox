using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    public static SaveData instance; 
    public Transform playerTransform; // Assign your player's transform in the Inspector
    public Camera mainCamera; // Assign your main camera in the Inspector
    public Dictionary<string, bool> enemyStatus = new Dictionary<string, bool>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject); 
    }

    //Example of how to add an enemy, in your enemy script you can do this
    public void AddEnemy(string enemyID){
        if(!enemyStatus.ContainsKey(enemyID)){
            enemyStatus.Add(enemyID, true); 
        }
    }

    public void SaveGame()
    {
        // Player Data
        PlayerData playerData = new PlayerData();
        playerData.positionX = playerTransform.position.x;
        playerData.positionY = playerTransform.position.y;
        playerData.positionZ = playerTransform.position.z;

        // Camera Data
        CameraData cameraData = new CameraData();
        cameraData.positionX = mainCamera.transform.position.x;
        cameraData.positionY = mainCamera.transform.position.y;
        cameraData.positionZ = mainCamera.transform.position.z;
        cameraData.rotationX = mainCamera.transform.rotation.x;
        cameraData.rotationY = mainCamera.transform.rotation.y;
        cameraData.rotationZ = mainCamera.transform.rotation.z;
        cameraData.rotationW = mainCamera.transform.rotation.w;

        // Enemy Data
        EnemyData enemyData = new EnemyData();
        enemyData.enemies = enemyStatus;

        // Convert to JSON
        string playerJson = JsonUtility.ToJson(playerData);
        string cameraJson = JsonUtility.ToJson(cameraData);
        string enemyJson = JsonUtility.ToJson(enemyData);

        // Save to file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", playerJson + "\n" + cameraJson + "\n" + enemyJson);
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            string[] allLines = File.ReadAllLines(Application.persistentDataPath + "/savefile.json");

            // Load Player Data
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(allLines[0]);
            playerTransform.position = new Vector3(playerData.positionX, playerData.positionY, playerData.positionZ);

            // Load Camera Data
            CameraData cameraData = JsonUtility.FromJson<CameraData>(allLines[1]);
            mainCamera.transform.position = new Vector3(cameraData.positionX, cameraData.positionY, cameraData.positionZ);
            mainCamera.transform.rotation = new Quaternion(cameraData.rotationX, cameraData.rotationY, cameraData.rotationZ, cameraData.rotationW);

            // Load Enemy Data
            EnemyData enemyData = JsonUtility.FromJson<EnemyData>(allLines[2]);
            enemyStatus = enemyData.enemies;

            //Update Enemies based on save data.
            UpdateEnemies();

            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No save file found!");
        }
    }

    //This function will update the enemies based on the saved data.
    public void UpdateEnemies(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        foreach(GameObject enemy in enemies){
            string enemyID = enemy.name; //name each enemy it's ID
            if(enemyStatus.ContainsKey(enemyID)){
                if(enemyStatus[enemyID] == false){ //If the enemy is dead.
                    Destroy(enemy); 
                }
            }
        }
    }

    // Data Classes
    [System.Serializable]
    public class PlayerData
    {
        public float positionX;
        public float positionY;
        public float positionZ;
    }

    [System.Serializable]
    public class CameraData
    {
        public float positionX;
        public float positionY;
        public float positionZ;
        public float rotationX;
        public float rotationY;
        public float rotationZ;
        public float rotationW;
    }

    [System.Serializable]
    public class EnemyData
    {
        public Dictionary<string, bool> enemies;
    }
}
