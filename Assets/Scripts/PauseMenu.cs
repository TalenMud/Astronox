using UnityEngine;
using System;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
public GameObject pauseScreen;
public bool GetIsPaused() { return isPaused; }

bool isPaused = false;

void Update(){
    if (Input.GetKeyDown(KeyCode.Escape)){
        TogglePauseMenu();
    }
}

public void TogglePauseMenu()
{
    isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseScreen.SetActive(isPaused);
}

public void exitGame()
{
    //Quit();


}
}