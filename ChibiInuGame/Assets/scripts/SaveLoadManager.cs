using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class SaveLoadManager{

	public static void SavePlayer(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.data", FileMode.Create);

        PlayerData data = new PlayerData(player);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/player.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.data", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(stream);
            stream.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("File does not exist");
            return new int[3];
        }
        
    }
}

[System.Serializable]
public class PlayerData
{
    public int[] stats;

    public PlayerData(Player player)
    {
        stats = new int[3];
        stats[0] = player.level;
        stats[1] = player.maxHealth;
        stats[2] = player.money;
    }
}
