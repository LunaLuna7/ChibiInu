using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStrikeState : IState {
	private KnightBossManager controller;
	private float timer;
	private float throwInterval;
	

	public ShieldStrikeState(KnightBossManager controller)
	{
		this.controller = controller;
		timer = 0;
		throwInterval = 1f;
	}

	public void EnterState()
	{
		controller.StartCoroutine(Move(12));
	}

	public void ExecuteState()
	{
		timer += Time.deltaTime;
		if(timer > throwInterval)
		{
			timer = 0;
			controller.StartCoroutine(ThrowShield(1f, 20));
		}
	}

	public void ExitState()
	{
		
	}

	private IEnumerator Move(float speed)
	{
		//get a position to go to
		Transform[] possibleLocations = controller.movementController.possibleLocations;
		Vector3 pos = possibleLocations[Random.Range(0, possibleLocations.Length)].position;
		//loop until get a position that is not the current position
		while(pos == controller.transform.position)
			pos = possibleLocations[Random.Range(0, possibleLocations.Length)].position;
		//switch state after finish moving
		yield return controller.movementController.MoveTo(pos, speed);
		controller.SwitchState();
	}

	private IEnumerator ThrowShield(float waitTime, float speed)
	{
		int type = Random.Range(0,2);
		GameObject obj;
		//throw shields out
		if(type == 0)
			obj = GameObject.Instantiate(controller.spikeShieldProjectile, controller.transform.position + Vector3.back, Quaternion.identity);
		else
			obj = GameObject.Instantiate(controller.shieldProjectile, controller.transform.position + Vector3.back, Quaternion.identity);
		obj.transform.SetParent(controller.skillObjectsGroup);

		//move obj out
		float rotateSpeed = 0;
		if(Random.Range(0, 2) == 0)
			rotateSpeed = Random.Range(-20f, -15f);
		else
			rotateSpeed = Random.Range(15, 20f);
		obj.GetComponent<BossShieldProjectile>().SetRotateSpeed(rotateSpeed);
		obj.GetComponent<BossShieldProjectile>().MoveTowards(90, 5, 15);

		yield return new WaitForSeconds(waitTime);

		if(obj != null)
		{
			if(type == 0)
			{
				//shoot shield towards player
				obj.GetComponent<BossShieldProjectile>().ShootTowards(controller.player.transform.position, speed);
				GameObject.Destroy(obj, 10f);
			}else{
				//shoot towards random direction
				obj.GetComponent<BossShieldProjectile>().SetRotateSpeed(0);
				obj.GetComponent<BossShieldProjectile>().Shoot(speed);
				GameObject.Destroy(obj, 10f);
			}
		}
	}
}
