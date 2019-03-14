using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanFireBall : MonoBehaviour {
    private float rotateSpeed = 0;
    public GameObject fireBallStorm;
    private Rigidbody2D rb;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(fireBallStorm, transform.position, Quaternion.identity);
    }

    //shoot towards current direction
    public void Shoot(float speed)
    {
        //get direction, adjust it to match the art
        float angle = transform.eulerAngles.z;
        //angle -= 90;
        //calculate velocity
        float radian = angle / 180f * 3.14f;
        rb.velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * speed;
    }

    //rotate towards the determination in preareTime, then shoot
    public void ShootTowards(Vector3 position, float speed)
    {
        StartCoroutine(ShootTowardsRoutine(position, speed));
    }

    public IEnumerator ShootTowardsRoutine(Vector3 targetPos, float speed)
    {

        //rotate speed cannot be 0
        if (rotateSpeed == 0)
            rotateSpeed = 5f;

        //calculate amount need to rotate
        Vector3 diff = (targetPos - transform.position).normalized;
        float targetAngle = Mathf.Atan2(diff.y, diff.x) / 3.14f * 180;
        //cancel the effect of shoot
        targetAngle += 90;
        float angleDiff = targetAngle - transform.eulerAngles.z;
        angleDiff %= 360;
        //match target angle to angular speed
        if (rotateSpeed > 0 && angleDiff < 0)
        {
            angleDiff += 360;
        }
        else if (rotateSpeed < 0 && angleDiff > 0)
        {
            angleDiff -= 360;
        }

        int unit = (int)(angleDiff / rotateSpeed);
        float angleInterval = rotateSpeed;
        //stop rotating, fake the rotation from now on
        rotateSpeed = 0;
        for (int x = 0; x < unit; ++x)
        {
            transform.Rotate(0, 0, angleInterval);
            yield return new WaitForEndOfFrame();
        }
        transform.eulerAngles = new Vector3(0, 0, targetAngle);

        //shoot
        Shoot(speed);
    }

    
}
