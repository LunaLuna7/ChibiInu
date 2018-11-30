using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShibaRunningMainMenu : MonoBehaviour {

    private Transform playerPosition;
    public Transform originPosition;
    public Transform target;
    public float speed;
    public float loopTime;
    

	// Use this for initialization
	void Start () {
        
        playerPosition = GetComponent<Transform>();
        InvokeRepeating("LoopBack",12f, loopTime);

	}
	
	// Update is called once per frame
	void Update () {
        
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}

    private void LoopBack()
    {
        //yield return new WaitForSeconds(loopTime);
        playerPosition.position = originPosition.position;
    }
}
