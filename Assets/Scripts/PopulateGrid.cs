using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PopulateGrid : MonoBehaviour
{
	public GameObject prefabButton;

	void Start(){
		Populate(GetPlayableFiles());
	}

	List<string> GetPlayableFiles(){
        string myPath = Application.streamingAssetsPath;
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles("*.*");

        List<string> fileNames = new List<string>();

        for (int i=0; i< info.Length; i++){
            string fileName = info[i].Name;
            string maybeIsWav = fileName.Substring(fileName.Length - 4, 4);
            bool isWav = (maybeIsWav.Equals(".wav"));
            if (isWav){
                Debug.Log(fileName);
                fileNames.Add(fileName);
            }
        }
        return fileNames;
    }

	void Populate(List<string> fileNames){
		GameObject newButton; // Create GameObject instance
		bool firstTime = true;

		foreach (string currName in fileNames){
			// Create new instances of our prefab until we've created as many as we specified
			newButton = (GameObject)Instantiate(prefabButton, transform);
			
			// Setting display of newButton to be the name of the mp3 file
			newButton.GetComponentInChildren<Text>().text = currName;

			// todo: make the buttons work 
			// newButton.GetComponent<Button>().onClick.AddListener( delegate {FilePathFetcher(currName);} );

			if (firstTime){
				newButton.GetComponent<Button>().Select();
				firstTime = false;
			}
		}
	}

	// string FilePathFetcher(string fileName){
	// 	string myPath = Application.streamingAssetsPath;
	// 	string filePath = myPath + "/" + fileName;
	// 	Debug.Log(filePath);
	// 	return filePath;
	// }
}