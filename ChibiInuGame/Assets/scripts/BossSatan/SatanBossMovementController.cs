using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossMovementController: KnightBossMovementController{
	public Transform[] possibleLocations1;

	public Transform[] possibleLocations2;
	public Transform[] possibleLocations3;
	public Transform[][] possibleLocationList = new Transform[4][];
	public GameObject player;

	public override void Awake()
	{
		base.Awake();
		possibleLocationList[0] = possibleLocations;
		possibleLocationList[1] = possibleLocations1;
		possibleLocationList[2] = possibleLocations2;
		possibleLocationList[3] = possibleLocations3;
	}

	public void Update()
	{
		//always facing the player
		Vector3 diff = player.transform.position - transform.position;
		//change sprite
		if(diff.x > 0)
		{
			transform.localScale = scale;
		}else if(diff.x < 0){
			transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
		}
	}

	public override IEnumerator MoveTo(Vector3 position, float speed)
	{
		float distance = Vector3.Distance(transform.position, position);
		float totalTime = distance/speed;
		Vector3 diff = position - transform.position;
		Vector3 originPos = transform.position;
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
