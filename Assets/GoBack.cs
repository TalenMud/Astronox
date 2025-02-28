using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{


void Update(){

if(SceneManager.GetActiveScene().name == "Planet_2")
{
    Debug.Log("LOADING...");
    SaveData.instance.LoadGame();
}

}

    public void returntoP2(){
        SceneManager.LoadScene("Planet_2");

    }
}
