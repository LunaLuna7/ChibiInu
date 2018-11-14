using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;


public class SaveManager : MonoBehaviour {
	public void Save()
	{

		//construct the save date
		//status related
		SaveData saveData = new SaveData();

		//map related

		//event related

		//save it to the local path
		SaveToPath(saveData, GeneratePath("save1"));
	}
	
	public void LoadButton()
	{
		Load();
	}

	public bool Load()
	{
		//load the savedata from the path
		SaveData saveData = ReadFromPath(GeneratePath("save1"));
		//if savedata exists, use the info in the game
		if(saveData == null)
			return false;
		//status related

		//map related

		//event related

		return true;
	}

	//==============================================================
	//Save Strings to local position
	//==============================================================
	private string GeneratePath(string fileName)
	{
		//construct the path
		string path = "";
		path = Path.Combine(Application.dataPath,"Save");
		path = Path.Combine(path, fileName + ".data");
		return path;
	}

	private void SaveToPath(SaveData data, string path)
	{
		//if the path do not exist, create the folder
		if(!Directory.Exists(Path.GetDirectoryName(path)))
			Directory.CreateDirectory(Path.GetDirectoryName(path));
		//convert the save data to string
		MemoryStream ms = new MemoryStream();
		using (BsonWriter writer = new BsonWriter(ms))
		{
			JsonSerializer serializer = new JsonSerializer();
			serializer.Serialize(writer, data);
		}
		string bsonData = Convert.ToBase64String(ms.ToArray());
		//save the data to a local file
		File.WriteAllText(path,bsonData);
	}

	private SaveData ReadFromPath(string path)
	{
		//if the file does not exist, return null
		if(!File.Exists(path))
			return null;
		string bsonData = File.ReadAllText(path);
		byte[] data = Convert.FromBase64String(bsonData);
		MemoryStream ms = new MemoryStream(data);
		SaveData saveData;
		using (BsonReader reader = new BsonReader(ms))
		{
			JsonSerializer serializer = new JsonSerializer();
			saveData = serializer.Deserialize<SaveData>(reader);
		}
		return saveData;
	}

}

[System.Serializable]
public class SaveData
{

}
