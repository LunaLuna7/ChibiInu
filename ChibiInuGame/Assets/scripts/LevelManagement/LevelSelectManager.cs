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
        public int sceneIndex;
    }

    public List<Level> levels;
    public SpriteRenderer[] macguffins;
    public LevelChanger levelChanger;
    

	void Start () {

		//read and set the levels depends on the info get from saveData
        for(int levelIndex = 0; levelIndex < levels.Count; ++levelIndex)
        {
            Level level = levels[levelIndex];
            level.unlocked = SaveManager.dataInUse.levels[levelIndex].unlocked;
            //update coin UI unless it is boss2/boss3
            if(level.unlocked)
            {
                levels[levelIndex].transform.GetComponent<LevelImage>().UpdateCollectableSprites(SaveManager.dataInUse.levels[levelIndex].collectable);
            }
            //update macguffins sprites if player pass certain levels (unlock the level behind)
            if(levelIndex == 3)
            {
                macguffins[0].color = Color.white;
            }else if(levelIndex == 6)
            {
                macguffins[1].color = Color.white;
            }else if(levelIndex == 9){
                macguffins[2].color = Color.white;
            }
        }
        levels[0].unlocked = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Back"))
            levelChanger.FadeToLevel(0);
	}
}
