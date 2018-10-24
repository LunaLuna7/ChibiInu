using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleSlimeTriggerMove : MonoBehaviour {

    public bool wallDown;
    public List<GameObject> slimesType;
	void Start () {
		
        for(int i = 0; i != slimesType.Count; ++i)
        {
            StateController sc;
            sc = slimesType[i].GetComponent<StateController>();
            sc.enemyStats.moveSpeed = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (wallDown)
        {

            for (int i = 0; i != slimesType.Capacity; ++i)
            {
                StateController sc;
                sc = slimesType[i].GetComponent<StateController>();
                sc.enemyStats.moveSpeed = 6;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            wallDown = true;
        }
    }
}
