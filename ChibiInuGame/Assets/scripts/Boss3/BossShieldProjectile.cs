using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldProjectile : MonoBehaviour {
	private float rotateSpeed = 0;
	private Rigidbody2D rigid;

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rotateSpeed != 0)
		{
			transform.Rotate(0, 0, rotateSpeed * Time.deltaTime * 60);
		}
	}

	public void SetRotateSpeed(float speed)
	{
		rotateSpeed = speed;
	}

	//shoot towards current direction
	public void Shoot(float speed)
	{
		//get direction, adjust it to match the art
		float angle = transform.eulerAngles.z;
		angle -= 90;
		//calculate velocity
		float radian = angle/180f * 3.14f;
		rigid.velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * speed;
	}

	//rotate towards the determination in preareTime, then shoot
	public void ShootTowards(Vector3 position, float speed)
	{
		StartCoroutine(ShootTowardsRoutine(position, speed));
	}

	public IEnumerator ShootTowardsRoutine(Vector3 targetPos, float speed)
	{
		//rotate speed cannot be 0
		if(rotateSpeed == 0)
			rotateSpeed = 5f;

		//calculate amount need to rotate
		Vector3 diff = (targetPos - transform.position).normalized;
		float targetAngle = Mathf.Atan2(diff.y, diff.x) /3.14f * 180;
		//cancel the effect of shoot
		targetAngle += 90;
		float angleDiff = targetAngle - transform.eulerAngles.z;
		angleDiff %= 360;
		//match target angle to angular speed
		if(rotateSpeed > 0 && angleDiff < 0)
		{
			angleDiff += 360;
		}else if (rotateSpeed < 0 && angleDiff > 0)
		{
			angleDiff -= 360;
		}

		int unit = (int)(angleDiff / rotateSpeed);
		float angleInterval = rotateSpeed;
		//stop rotating, fake the rotation from now on
		rotateSpeed = 0;
		for(int x = 0; x < unit; ++x)
		{
			transform.Rotate(0, 0, angleInterval);
			yield return new WaitForEndOfFrame();
		}
		transform.eulerAngles = new Vector3(0, 0, targetAngle);

		//shoot
		Shoot(speed);
	}

	public void MoveTowards(float angle, float distance, float speed)
	{
		StartCoroutine(MoveTowardsRoutine(angle, distance, speed));
	}

	private IEnumerator MoveTowardsRoutine(float angle, float distance, float speed)
	{
		//calculate needed data
		float totalTime = distance/speed;
		Vector3 currentPos = transform.position;
		float radian = angle / 180 * 3.14f;
		Vector3 diff = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * distance;

		//move
		float timeInterval = 0.02f;
		for(float x = 0; x < totalTime; x += timeInterval)
		{
			rigid.MovePosition(currentPos + diff * x / totalTime);
			yield return new WaitForSeconds(timeInterval);
		}
		rigid.MovePosition(currentPos + diff);
	}

}
