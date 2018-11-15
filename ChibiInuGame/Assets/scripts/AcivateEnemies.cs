using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcivateEnemies : MonoBehaviour {

    public List<GameObject> enemiesToEnable;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            for(int i = 0; i < enemiesToEnable.Capacity; i++)
            {
                    enemiesToEnable[i].SetActive(true);
            }
        }
    }
}
