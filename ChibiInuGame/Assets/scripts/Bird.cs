using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public bool triggerFly;
    public float speed;
    public float timeBeforeFlyAway;
    public Transform playerTransform;
    public float xDistanceMax;
    public float xDistanceMin;
    public float yDistance;
    Animator anim;
    SpriteRenderer sprite;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    void Start () {
        triggerFly = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (triggerFly)
            FlyAway();
	}

    private void FlyAway()
    {
        anim.Play("BirdFly");
        sprite.flipX = true;
        float xDistance = Random.Range(xDistanceMin, xDistanceMax);
        if (playerTransform.position.x - transform.position.x < 0)//coming from Right
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x + xDistance, transform.position.y + yDistance, 0), speed * Time.deltaTime);
        }
        else
        {
            sprite.flipX = false;
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x - xDistance, transform.position.y + yDistance, 0), speed * Time.deltaTime);
        }

    }
}
