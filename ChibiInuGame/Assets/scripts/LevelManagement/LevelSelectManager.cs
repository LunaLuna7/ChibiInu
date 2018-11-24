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
        for(int levelIndex = 0; levelIndex < levels.Count; ++levelIndex)
        {
            Level level = levels[levelIndex];
            level.unlocked = SaveManager.dataInUse.levels[levelIndex].unlocked;
            if(level.unlocked)
                levels[levelIndex].transform.GetComponent<LevelImage>().UpdateCollectableSprites(SaveManager.dataInUse.levels[levelIndex].collectable);
        }
        levels[0].unlocked = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
