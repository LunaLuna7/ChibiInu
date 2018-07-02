using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private GameObject player;
    private Rigidbody2D rb;
    private bool isGrounded;

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public float smoothUp;
    public float smoothDown;
    public float currentHeight;
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHeight = player.transform.position.y;
        rb = player.GetComponent<Rigidbody2D>();
        
    }
	
	
	void LateUpdate () {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);

       
        if (player.transform.position.y > currentHeight + 5) //above
        {
            //try to do y + some formula that the higher the current height the more you can see below you
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y + 1 , gameObject.transform.position.z), smoothUp); //y + 1f
        }
        else if(player.transform.position.y < currentHeight)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y + 1f, gameObject.transform.position.z), smoothDown);
        }
        else if(player.transform.position.y < currentHeight + 5)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, currentHeight, gameObject.transform.position.z), smoothDown);   
        }
        if (rb.velocity.y == 0)
        {
            currentHeight = player.transform.position.y;
        }
       
        Debug.Log(rb.velocity.y);
    }
    
}
