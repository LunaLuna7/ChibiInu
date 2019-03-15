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
    private bool canMove;

	void Start () {
        levelName.text = "[Level Name]";
        position = SaveManager.dataInUse.lastLevelEntered; //current level
        transform.position = levelSelectManager.levels[position].transform.position;
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {

        moveX = Input.GetAxisRaw("Horizontal");

        MovePlayer(position);
        
        //when not moving
        if(transform.position == levelSelectManager.levels[position].transform.position && canMove)
        {
            levelName.text = levelSelectManager.levels[position].name;
            if (Input.GetButtonDown("Submit"))
            {
                SoundEffectManager.instance.Stop("MainMenu");
                SoundEffectManager.instance.Play("MenuSelect");
                levelChanger.FadeToLevel(levelSelectManager.levels[position].sceneIndex);
                canMove = false;
            }
            else if (moveX > 0)
            {
                if(position + 1 < levelSelectManager.levels.Capacity)
                {
                    //only able to go to that level when it is unlocked
                    if(levelSelectManager.levels[position + 1].unlocked)
                    {
                        SoundEffectManager.instance.Play("MenuScroll");
                        position += 1;
                    }
                }
            }
            else if (moveX < 0)
            {
                if (position - 1 >= 0)
                {
                    SoundEffectManager.instance.Play("MenuScroll");
                    position -= 1;
                }
            }
        }
        
    }

    void MovePlayer(int i)
    {
        transform.position = Vector3.MoveTowards(transform.position, levelSelectManager.levels[i].transform.position, speed * Time.deltaTime);
    }

    
}
