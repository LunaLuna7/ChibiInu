using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour {

    [System.SerializableAttribute]
    public class Level
    {
        public string name;
        public bool unlocked;
        public Transform transform;
    }

    public List<Level> levels;

	void Start () {
		//read and set the levels depends on the info get from saveData
        for(int index = 0; index < levels.Count; ++index)
        {
            levels[index].unlocked = SaveManager.dataInUse.levels[index].unlocked;
        }
        levels[0].unlocked = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
