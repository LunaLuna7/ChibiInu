using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* read from json files and store all information needed for dialogues
*/
public class DialogueLibrary : MonoBehaviour {
	public static DialogueLibrary instance = null;
	public FaceSprite[] faceSpriteList;
	private Dictionary<string, Sprite> faceSpriteLibrary = new Dictionary<string, Sprite>();
	private Dictionary<string, Dialogue[]> dialogueLibrary = new Dictionary<string, Dialogue[]>(); 
	// Use this for initialization
	void Start () {
		//make sure there is only one instance
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
			InitializeFaceSprites();
		}else{
			Destroy(gameObject);
		}
	}
	
	//Load a dialogue data from JSON file, and store it in the dictionary
	public void LoadDialogueJson(string filePath)
	{
		if(!dialogueLibrary.ContainsKey(filePath))
		{
			string path = "Dialogue/" + filePath;
			//read the json file to text
			try{
				string jsonData = Resources.Load<TextAsset>(path).text;
				DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(jsonData);
				dialogueLibrary.Add(filePath, dialogueData.dialogueSequence);
			}catch{
				Debug.LogError("Fail to load file: " + path);
			}
		}
	}

	//use face sprites list to create the face sprite library 
	public void InitializeFaceSprites()
	{
		faceSpriteLibrary.Clear();
		foreach(FaceSprite fs in faceSpriteList)
		{
			faceSpriteLibrary.Add(fs.name, fs.faceSprite);
		}
	}

	//====================================================================================
	//Get info from the library
	//====================================================================================
	public Sprite GetFaceSprite(string imageName)
	{
		return faceSpriteLibrary[imageName];
	}

	public Dialogue[] GetDialogueSequence(string filePath)
	{
		return dialogueLibrary[filePath];
	}

}

[System.Serializable]
public class FaceSprite
{
    public string name;
	public Sprite faceSprite;
}

[System.Serializable]
public class DialogueData
{
    public Dialogue[] dialogueSequence;
}
