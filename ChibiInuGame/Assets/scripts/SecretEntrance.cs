using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretEntrance : MonoBehaviour {

    public List<GameObject> secretPath;
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
            foreach(GameObject pathPiece in secretPath)
            {
                SpriteRenderer sprite = pathPiece.GetComponent<SpriteRenderer>();
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, .5f);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (GameObject pathPiece in secretPath)
            {
                SpriteRenderer sprite = pathPiece.GetComponent<SpriteRenderer>();
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            }
        }
    }
}
