using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleSlimeTriggerMove : MonoBehaviour {

    public bool wallDown;
    public float moveSpeed;
    public GameObject wall;
    public List<GameObject> slimesType;
    public List<GameObject> slimes;
    public List<GameObject> slimesToRevive;
	void Start () {
            StateController sc;
            sc = slimesType[0].GetComponent<StateController>();
            sc.enemyStats.moveSpeed = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (wall.activeSelf)
            wallDown = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&& !wallDown)
        {
            wallDown = true;
            InitializeSlimes();
            
        }
    }

    void InitializeSlimes()
    {
        
        for(int x = 0; x < slimes.Count; x++)
        {
            slimesToRevive[x].SetActive(true);
            slimes[x].transform.localPosition = Vector3.zero;
           
        }
        StateController sc;
        sc = slimesType[0].GetComponent<StateController>();
        sc.enemyStats.moveSpeed = moveSpeed;
    }

}
