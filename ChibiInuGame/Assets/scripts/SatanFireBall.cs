using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanFireBall : MonoBehaviour {
    private float rotateSpeed = 0;
    public GameObject fireBallStorm;
    private Rigidbody2D rb;
    SatanBossManager satanBossManager;

    // Use this for initialization
    void Awake()
    {
        satanBossManager = FindObjectOfType<SatanBossManager>();
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
        GameObject createdEnemy = Instantiate(fireBallStorm, transform.position, Quaternion.identity);
        createdEnemy.transform.SetParent(satanBossManager.skillObjectsGroup);
    }

    public void RotateTowards(GameObject target)
    {
        Vector3 diff = (target.transform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(diff.y, diff.x) / 3.14f * 180;
        transform.eulerAngles = new Vector3(0,0,targetAngle);
    }

    //shoot towards current direction
    public void Shoot(float speed)
    {
        float angle = transform.eulerAngles.z;
        //angle -= 90;
        //calculate velocity
        float radian = angle / 180f * 3.14f;
        rb.velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * speed;
    }
    
}
