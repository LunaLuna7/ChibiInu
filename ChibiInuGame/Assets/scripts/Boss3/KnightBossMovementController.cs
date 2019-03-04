using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBossMovementController : MonoBehaviour {
	public Transform[] possibleLocations;
	private Rigidbody2D rigid;
	private SpriteRenderer sprite;
	// Use this for initialization
	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}
	void Start () {
		transform.position = new Vector3(possibleLocations[0].position.x, possibleLocations[0].position.y, transform.position.z);
	}
	

	public IEnumerator MoveTo(Vector3 position, float speed)
	{
		float distance = Vector3.Distance(transform.position, position);
		float totalTime = distance/speed;
		Vector3 diff = position - transform.position;
		Vector3 originPos = transform.position;
		//change sprite
		if(diff.x > 0)
		{
			sprite.flipX = false;
		}else if(diff.x < 0){
			sprite.flipX = true;
		}
		//move
		for(float time = 0; time< totalTime; time += Time.deltaTime)
		{
			Vector3 tempPos = originPos + diff * time/totalTime;
			tempPos.z = originPos.z;
			rigid.MovePosition(tempPos);
			yield return new WaitForEndOfFrame();
		}
		rigid.MovePosition(new Vector3(position.x, position.y, originPos.z));
	}
}
