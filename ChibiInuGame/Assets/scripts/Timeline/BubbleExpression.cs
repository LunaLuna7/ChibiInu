using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleExpression : MonoBehaviour {
	public GameObject target;
	public Vector2 posDiffer;
	// Use this for initialization
	void OnEnable () {
		transform.position = target.transform.position + new Vector3(posDiffer.x, posDiffer.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.transform.position + new Vector3(posDiffer.x, posDiffer.y, 0);
	}
}
