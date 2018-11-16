using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;


public class SaveManager : MonoBehaviour {
	public static void Save(string fileName)
	{

		//construct the save date
		SaveData saveData = new SaveData();

		//save it to the local path
		SaveToPath(saveData, GeneratePath(fileName));
	}
	

	public static SaveData Load(string fileName)
	{
		//load the savedata from the path
		return ReadFromPath(GeneratePath(fileName));
	}

	//==============================================================
	//Save Strings to local position
	//==============================================================
	private static string GeneratePath(string fileName)
	{
		string path = "";
		//when at PC, give the path inside the game folder
		if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			//construct the path
			path = Path.Combine(Application.dataPath,"Save");
			path = Path.Combine(path, fileName + ".data");
		}
		else
		{
			path = Application.persistentDataPath;
			path = Path.Combine(path, fileName + ".data");
		}
		return path;
	}

	private static void SaveToPath(SaveData data, string path)
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

	private static SaveData ReadFromPath(string path)
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

	private static void DebugJsonData(SaveData data)
	{
		string js = UnityEngine.JsonUtility.ToJson(data);
		Debug.Log(js);
	}
}

[System.Serializable]
public class SaveData
{
    public int[] stats = new int[3];
	public string playerName;
	public int highestLevelAchieved;

    public void GetPlayerData(Player player)
    {
        stats[0] = player.level;
        stats[1] = player.maxHealth;
        stats[2] = player.money;
    }



}
