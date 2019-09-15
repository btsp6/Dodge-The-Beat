using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class UIManager : MonoBehaviour
{
    void Start(){
        // Debug.Log("help"); 
    }

    public void StartMenuScene(){
        SceneManager.LoadScene("Menu");
    }

    public void StartLevelSelectScene(){
        // Debug.Log("help1");
        SceneManager.LoadScene("LevelSelect");
    }

    public void StartGameScene(){
        // Debug.Log("help2");
        SceneManager.LoadScene("Game"); 
    }

    public void doExitGame(){
        Application.Quit();
    }
}
