using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWaveState : IState {
	private KnightBossManager controller;
	//shield coming out from center of Boss
	private float shieldComingOutDistance = 5f;
	private float shieldComingOutSpeed = 7.5f;
	//wait before shoot
	private float waitTime = 2f;
	//shoot
	private float projectileSpeed = 30;

	public ShieldWaveState(KnightBossManager controller)
	{
		this.controller = controller;
	}

	public void EnterState()
	{
		float healthLost = (controller.bossHealth.maxHealth - controller.bossHealth.health)/controller.bossHealth.maxHealth;
		int num = 5 + (int)(healthLost/0.2f);
		controller.StartCoroutine(Skill(num, waitTime));
	}

	public void ExecuteState()
	{

	}

	public void ExitState()
	{

	}

	public IEnumerator Skill(int num, float waitTime)
	{
		//throw shields out
		float angleInterval = 360f/num;
		List<GameObject> objectList = new List<GameObject>();
		for(int x = 0; x < num; ++x)
		{
			GameObject obj = GameObject.Instantiate(controller.shieldProjectile, controller.transform.position + Vector3.back, Quaternion.identity);
			obj.transform.SetParent(controller.skillObjectsGroup);
			objectList.Add(obj);
			//move obj out
			float rotateSpeed = 0;
			if(Random.Range(0, 2) == 0)
				rotateSpeed = Random.Range(-20f, -15f);
			else
				rotateSpeed = Random.Range(15, 20f);
			obj.GetComponent<BossShieldProjectile>().SetRotateSpeed(rotateSpeed);
			obj.GetComponent<BossShieldProjectile>().MoveTowards(angleInterval * (x + 1), shieldComingOutDistance, shieldComingOutSpeed);
		}

		yield return new WaitForSeconds(waitTime);

		//shoot all shields towards random direction
		for(int x = 0; x < objectList.Count; ++x)
		{
			Vector3 direction = objectList[x].transform.position - controller.transform.position;
			direction.z = 0;
			objectList[x].GetComponent<BossShieldProjectile>().ShootTowards(objectList[x].transform.position + direction, projectileSpeed);
			//obj.GetComponent<BossShieldProjectile>().Shoot(projectileSpeed);
			GameObject.Destroy(objectList[x], 10f);
		}

		yield return new WaitForSeconds(waitTime);
		controller.SwitchState();
	}
}
