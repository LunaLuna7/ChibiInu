using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public float power;
    private Rigidbody2D rb;
    public CharacterController2D characterController2d;
    
    

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
        if(collision.gameObject.tag == "hurtBox" || collision.gameObject.tag == "DestructibleWall" || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(this.gameObject);
            
        }
    }
}
