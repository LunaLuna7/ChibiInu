using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public float power;
    private Rigidbody2D rb;
    public CharacterController2D characterController2d;
    public GameObject destroyParticle;


    // Use this for initialization
    void Start () {
        characterController2d = FindObjectOfType<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        if (characterController2d.m_FacingRight)
            rb.velocity = new Vector3(power * 10, 0, 0);
        else
            rb.velocity = new Vector3(power * -10, 0, 0);

        Destroy(this.gameObject, .7f);
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.gameObject.tag == "projectile")
        {
            //Instantiate(collision.gameObject.GetComponent<Projectile>().destroyParticle, collision.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "hurtBox" || collision.gameObject.tag == "hitBox"  || collision.gameObject.tag == "DestructibleWall"
            || collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Limit"))
        {
            Destroy(this.gameObject);
            
        }

    }

    private void OnDestroy()
    {
        if (!LevelEnd.levelEnding)
        {
            GameObject temp = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(temp, 1f);

        }
    }
}
