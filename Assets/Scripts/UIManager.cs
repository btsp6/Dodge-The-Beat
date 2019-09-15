using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class UIManager : MonoBehaviour
{
    void Start(){
        Debug.Log("help"); 
    }

    public void StartLevelSelectScene(){
        Debug.Log("help1");
        SceneManager.LoadScene("LevelSelect");
    }

    public void StartGameScene(){
        Debug.Log("help2");
        SceneManager.LoadScene("Game"); 
    }

    public void LogStreamingAssets(){
        string myPath = Application.streamingAssetsPath;

        Debug.Log(myPath);
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles("*.*");
        
        Debug.Log(info.Length/2);
        string[] fileNames = new string[info.Length/2];
        int j = 0;

        for (int i=0; i< info.Length; i++){
            string fileName = info[i].Name;
            string maybeIsMeta = fileName.Substring(fileName.Length - 5, 5);
            bool isMeta = (maybeIsMeta.Equals(".meta"));
            if (!isMeta){
                // do stuff
                Debug.Log(fileName);
                fileNames[j] = fileName;
                j++;
            }            
        }
        Debug.Log(fileNames.ToString());
        // fileNames.ToList().ForEach(name => Debug.Log(name.ToString()));
    }
    
}
