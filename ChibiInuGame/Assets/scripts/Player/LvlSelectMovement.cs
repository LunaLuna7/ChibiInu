using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LvlSelectMovement : MonoBehaviour {

    public LevelSelectManager levelSelectManager;
    public LevelChanger levelChanger;
    public int position;
    public Text levelName;
    

    private const float speed = 10f;
    float moveX;

	void Start () {
        levelName.text = "[Level Name]";
        position = 0; //current level
        transform.position = levelSelectManager.levels[position].transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

        moveX = Input.GetAxisRaw("Horizontal");

        MovePlayer(position);

        if(transform.position == levelSelectManager.levels[position].transform.position)
        {
            levelName.text = levelSelectManager.levels[position].name;
            if (Input.GetKeyDown(KeyCode.X))
            {
                levelChanger.FadeToLevel(position + 1);
            }
        }

        if (moveX > 0 && transform.position == levelSelectManager.levels[position].transform.position)
        {
            if(position + 1 < levelSelectManager.levels.Capacity)
                position += 1;
        }

        if (moveX < 0 && transform.position == levelSelectManager.levels[position].transform.position)
        {
            if (position - 1 >= 0)
                position -= 1;
        }

        

    }

    void MovePlayer(int i)
    {
        transform.position = Vector3.MoveTowards(transform.position, levelSelectManager.levels[i].transform.position, speed * Time.deltaTime);
    }

    
}
