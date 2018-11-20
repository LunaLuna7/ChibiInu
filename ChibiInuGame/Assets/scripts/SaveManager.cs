using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;


public class SaveManager : MonoBehaviour {
	public static string filename;
	public static SaveData dataInUse;

	public static void Save(string fileName)
	{
		//save it to the local path
		SaveToPath(SaveManager.dataInUse, GeneratePath(fileName));
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

	public static void DebugJsonData(SaveData data)
	{
		string js = UnityEngine.JsonUtility.ToJson(data);
		Debug.Log(js);
	}
}

[System.Serializable]
public class SaveData
{
	public string playerName;
	public LevelInfo[] levels = new LevelInfo[12];
	public List<PartnerInfo> partners;


	public SaveData(string name)
	{
		playerName = name;
		//default levels, only the first is on
		levels[0] = new LevelInfo(true, false, false, false);
		for(int index = 1; index < levels.Length; ++index)
		{
			levels[index] = new LevelInfo(false, false, false, false);
		}
		//default parter
		partners = new List<PartnerInfo>();
	}

	public void GetPartnerInfo(PartnerManager pm)
	{
		//clean the list first
		partners.Clear();
		foreach(Partner partner in pm.allPartners)
		{
			//record the partners that are in use
			if(partner.selected)
			{
				//check which skill slot its in is on
				if(partner.J)
					partners.Add(new PartnerInfo(partner.partnerID, "J"));
				else
					partners.Add(new PartnerInfo(partner.partnerID, "K"));
			}
		}
	}

}

[System.Serializable]
public class LevelInfo
{
	public bool unlocked;
	public bool[] collectable;

	public LevelInfo(bool unlocked, bool collectable1, bool collectable2, bool collectable3)
	{
		this.unlocked = unlocked;
		this.collectable = new bool[3]{collectable1, collectable2, collectable3};
	}
}


[System.Serializable]
public class PartnerInfo
{
	public int index;
	public string skillSlot;

	public PartnerInfo(int index, string skillSlot)
	{
		this.index = index;
		this.skillSlot = skillSlot;
	}
}
