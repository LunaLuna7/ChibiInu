using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasAOE : MonoBehaviour {


    public Transform Gas;
    public Transform start;
    public Transform end;
    public float speed;

    void Start () {
        Gas.position = start.position;
        DestroyObject(gameObject, 35);
	}
	
	
	void FixedUpdate () {
        Gas.position = Vector3.Lerp(Gas.position, end.position, speed * Time.deltaTime);
    }
}
