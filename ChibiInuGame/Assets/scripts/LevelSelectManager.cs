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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
