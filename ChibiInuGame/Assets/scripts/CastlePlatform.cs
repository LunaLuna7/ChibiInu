using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlePlatform : MonoBehaviour {

    public float speed;
    public bool active;
    public bool powerOn;
    public Transform destination;
    private Rigidbody2D rb;
    public PlayerHealth playerHealth;
    public List<GameObject> enemiesToReturn;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(active)
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        if (playerHealth.HPLeft == 0)
        {
            ResetPlatform();
            active = false;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (powerOn && collision.gameObject.CompareTag("Player"))
        {
            active = true;
        }
    }

    private void ResetPlatform()
    {
        transform.localPosition = Vector3.zero;
        foreach(GameObject each in enemiesToReturn)
        {
            each.transform.localPosition = Vector3.zero;
        }
    }
}
