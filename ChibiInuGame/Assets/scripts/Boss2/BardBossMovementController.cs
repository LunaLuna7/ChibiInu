using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handle the movement of the Bard Boss
public class BardBossMovementController : MonoBehaviour {
	private Vector2 direction;
	public float speed;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rigid;

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		Debug.Log(rigid.velocity);
	}

	//move towards a random direction
	public void StartMoving(float speed = -1)
	{
		if(speed > 0)
			this.speed = speed;
		float radian = Random.Range(0, 6.28f);
		SetDirection(radian);
		rigid.velocity = this.speed * direction;
		ChangeFaceDirection();
	}

	private void SetDirection(float radian)
	{
		direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
	}
	public void StopMoving()
	{
		ChangeSpeed(0);
	}

	//change speed in the middle of movement
	public void ChangeSpeed(float speed)
	{
		this.speed = speed;
		rigid.velocity = this.speed * direction;
	}

	//change velocity each time hitting the boundary
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag == "Wall")
		{
			ChangeDirectionAfterHitWall();
		}
	}

	//change the direction based on current direction
	private void ChangeDirectionAfterHitWall()
	{
		direction = -direction;
		//convert direction to angle
		float radian = Mathf.Atan2(direction.y, direction.x);
		//change angle
		radian += Random.Range(-1.57f, 1.57f);
		//convert angle back to vector
		SetDirection(radian);
		rigid.velocity = direction * speed;
		ChangeFaceDirection();
	}

	//change face direction depends on the move direction
	private void ChangeFaceDirection()
	{
		if(direction.x >= 0)
			spriteRenderer.flipX = false;
		else
			spriteRenderer.flipX = true;
	}

}
