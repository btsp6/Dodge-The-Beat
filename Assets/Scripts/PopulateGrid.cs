using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PopulateGrid : MonoBehaviour
{
	public GameObject prefabButton;

	void Start()
	{
		Populate(GetPlayableFiles());
	}

	void Update()
	{

	}

	string[] GetPlayableFiles(){
        string myPath = Application.streamingAssetsPath;
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles("*.*");

        string[] fileNames = new string[info.Length/2];
        int j = 0;
        
        Debug.Log(info.Length/2);
        Debug.Log(myPath);

        for (int i=0; i< info.Length; i++){
            string fileName = info[i].Name;
            string maybeIsMp3 = fileName.Substring(fileName.Length - 4, 4);
            bool isMp3 = (maybeIsMp3.Equals(".mp3"));
            if (isMp3){
                Debug.Log(fileName);
                fileNames[j] = fileName;
                j++;
            }
        }
        return fileNames;
    }

	void Populate(string[] fileNames)
	{
		GameObject newButton; // Create GameObject instance

		for (int i = 0; i < fileNames.Length; i++)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newButton = (GameObject)Instantiate(prefabButton, transform);
			
			// Randomize the color of our image
			newButton.GetComponentInChildren<Text>().text = fileNames[i];
		}
	}
}