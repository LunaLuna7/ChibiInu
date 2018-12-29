using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class DialogueJsonEditor : EditorWindow {
	public DialogueData dialogueData;

	[MenuItem("Tool/DialogueDataEditor")]
	public static void OpenWindow()
	{
		//show editor window
		EditorWindow.GetWindow(typeof(DialogueJsonEditor)).Show();
	}

	private void OnGUI()
	{
		//draw UI
		if(dialogueData != null)
		{
			//draw this property at the window
			SerializedObject so = new SerializedObject(this);
			SerializedProperty sp = so.FindProperty("dialogueData");
			EditorGUILayout.PropertyField(sp, true);
			so.ApplyModifiedProperties();
			//draw save button
			if(GUILayout.Button("Save"))
			{
				SaveDialogueData();
			}
		}

		//new button
		if(GUILayout.Button("New"))
			CreateNewData();
		if(GUILayout.Button("Load"))
			LoadDialogueData();
	}

	public void CreateNewData()
	{
		//set data to a new one
		dialogueData = new DialogueData();
	}

	public void SaveDialogueData()
	{
		string folderPath = Path.Combine(Application.dataPath, "Resources");
		folderPath = Path.Combine(folderPath, "Dialogue");
		//open a save file panel
		string path = EditorUtility.SaveFilePanel("Save File to", folderPath, "", "json");
		//save the data
		if( path!= null && path.Length > 0)
		{
			string jsonData = JsonUtility.ToJson(dialogueData);
			//save json to local
			using(StreamWriter sw = new StreamWriter(path))
			{
				sw.Write(jsonData);
			}
		}
	}

	public void LoadDialogueData()
	{
		string folderPath = Path.Combine(Application.dataPath, "Resources");
		folderPath = Path.Combine(folderPath, "Dialogue");
		//open a open file panel
		string path = EditorUtility.OpenFilePanel("Choose your dialogue data", folderPath,"json");
		//load the data
		if( path!= null && path.Length > 0)
		{
			string jsonData;
			//read json data
			using(StreamReader sr = new StreamReader(path))
			{
				jsonData = sr.ReadToEnd();
			}
			dialogueData = JsonUtility.FromJson<DialogueData>(jsonData);
		}
	}

}
