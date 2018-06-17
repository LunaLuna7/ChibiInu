using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    [Header("wall attributes")]
    public int health;
    
    [Tooltip("seconds before wall regenerates")]
    public int regenerateTime;
    public bool immune;

    private int originalHP;
    private SpriteRenderer sprite;
    private PolygonCollider2D wallCollider;

    private void Start()
    {
        wallCollider = gameObject.GetComponent<PolygonCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        originalHP = health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "attack")
        {
            DestroyObject(collision.gameObject);
            if(immune == false)
                health--;
        }
        if(health <= 0 && immune == false)
        {
            StartCoroutine(regenerateWall());
            sprite.enabled = false;
            wallCollider.enabled = false;
        }
    }

    IEnumerator regenerateWall()
    {
        yield return new WaitForSeconds(regenerateTime);
        health = originalHP;
        sprite.enabled = true;
        wallCollider.enabled = true;
    }

}
