using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpikes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(collision.GetComponent<PlayerHealth>().DamageState());
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2((collision.transform.position.x - this.transform.position.x) * 50,
                (collision.transform.position.y - this.transform.position.y)* 20);
        }
    }

    private void OnEnable()
    {
        //StartCoroutine(TurnOff());
        
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
