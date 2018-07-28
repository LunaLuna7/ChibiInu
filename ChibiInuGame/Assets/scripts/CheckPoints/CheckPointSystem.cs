using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour {

    public List<Transform> checkPoints;
    public GameObject player;

	// Use this for initialization
	void Start () {
        player.transform.position = checkPoints[UpdateCheckPoint.currentCheckPoint -1].position;
	}
	
	
}
