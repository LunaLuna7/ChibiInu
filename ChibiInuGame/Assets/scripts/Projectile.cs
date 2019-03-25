using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    public float speed;
    public float lifeSpam = 3f;
    public GameObject destroyParticle;
    private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeSpam);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

   
    private void OnDestroy()
    {
        if (!LevelEnd.levelEnding)
        {
            GameObject temp = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(temp, 1f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, .05f);
        }
    }
}
