using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handle the movement of the Bard Boss
public class BardBossMovementController : MonoBehaviour {
	private Vector2 direction;
	public float speed;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rigid;
	private bool isMoving = false;
	[Header("Boundary Detection")]
	public Transform leftLowerPoint;
	public Transform rightUpperPoint;
	private float centerX;
	private float centerY;
	public float halfWidth;
	public float halfHeight;

	private Vector3 scale;

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		centerX = (leftLowerPoint.position.x + rightUpperPoint.position.x)/2;
		centerY = (leftLowerPoint.position.y + rightUpperPoint.position.y)/2;
		scale = transform.localScale;
		//facing left
		transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
	}


	void Update()
	{
		if(isMoving)
			CheckBoundary();
	}

	//move towards a random direction
	public void StartMoving(float speed = -1)
	{
		if(speed > 0)
			this.speed = speed;
		float radian = Random.Range(0, 6.28f);
		SetVelocityRadian(radian);
		isMoving = true;
	}

	public void ContinueMoving()
	{
		rigid.velocity = speed * direction;
		isMoving = true;
	}

	public void StopMoving()
	{
		rigid.velocity = Vector2.zero;
		isMoving = false;
	}

	//change speed in the middle of movement
	public void ChangeSpeed(float speed)
	{
		this.speed = speed;
		rigid.velocity = this.speed * direction;
	}

	private void CheckBoundary()
	{
		Vector3 pos = transform.position;
		float radian = 0;
		//x position out of range
		//touch left
		if(pos.x - halfWidth < leftLowerPoint.position.x)
		{
			//set position in bound
			transform.position = new Vector3(leftLowerPoint.position.x + halfWidth ,pos.y, pos.z);
			//change direction depends on y
			if(pos.y > centerY)
			{
				radian = Random.Range(5f, 6.28f); // go to right down
			}else{
				radian = Random.Range(0f, 1.4f); // go to right up
			}
			SetVelocityRadian(radian);
		}//right
		else if (pos.x + halfWidth> rightUpperPoint.position.x)
		{
			//set position in bound
			transform.position = new Vector3(rightUpperPoint.position.x - halfWidth ,pos.y, pos.z);
			//change direction depends on y
			if(pos.y > centerY)
			{
				radian = Random.Range(3.14f, 4.5f); // go to left down
			}else{
				radian = Random.Range(1.57f, 3f); // go to left up
			}
			SetVelocityRadian(radian);
		}
		//y position out of range
		//touch bottom
		if(pos.y - halfHeight < leftLowerPoint.position.y)
		{
			//set position in bound
			transform.position = new Vector3(pos.x, leftLowerPoint.position.y + halfWidth, pos.z);
			//change direction depends on y
			if(pos.x > centerX)
			{
				radian = Random.Range(1.57f, 3.14f); // go to left up
			}else{
				radian = Random.Range(0f, 1.57f); // go to right up
			}
			SetVelocityRadian(radian);
		}//top
		else if (pos.y + halfHeight > rightUpperPoint.position.y)
		{
			//set position in bound
			transform.position = new Vector3(pos.x, rightUpperPoint.position.y - halfWidth, pos.z);
			//change direction depends on y
			if(pos.x > centerX)
			{
				radian = Random.Range(3.14f, 4.71f); // go to left down
			}else{
				radian = Random.Range(4.71f, 6.28f); // go to right down
			}
			SetVelocityRadian(radian);
		}
	}

	//change face direction depends on the move direction
	private void ChangeFaceDirection()
	{
		if(direction.x >= 0)
			transform.localScale = scale;
		else
			transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
	}

	private void SetVelocityRadian(float radian)
	{
		direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
		rigid.velocity = direction * speed;
		ChangeFaceDirection();
	}
	/* change velocity each time hitting the boundary
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
	}*/



}
