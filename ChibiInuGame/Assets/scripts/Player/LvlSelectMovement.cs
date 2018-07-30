using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSelectMovement : MonoBehaviour {

    public LevelSelectManager levelSelectManager;
    public int position;
    private const float speed = 10f;

	void Start () {
        position = 0; //current level
        transform.position = levelSelectManager.levels[position].transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

        MovePlayer(position);
        if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position == levelSelectManager.levels[position].transform.position)
        {
            if(position + 1 < levelSelectManager.levels.Capacity)
                position += 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position == levelSelectManager.levels[position].transform.position)
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
