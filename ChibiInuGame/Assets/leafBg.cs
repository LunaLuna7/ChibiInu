using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leafBg : MonoBehaviour
{

    public float xOffset;
    public float speed;
    Vector3 originTransform;
    private Vector3 pos1;
    private Vector3 pos2;


    private bool dirRight = true;
    //public float speed = 2.0f;
    // Use this for initialization
    void Start()
    {
        originTransform = transform.position;

    }
    /*
	// Update is called once per frame
	void Update () {
        pos1 = new Vector3(originTransform.x - 4, transform.position.y -1f , 0);
        pos2 = new Vector3(originTransform.x + 4, transform.position.y -1f, 0);
        MovingLeaf();
	}

    void MovingLeaf()
    {
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }*/


    void Update()
    {
        if (dirRight)
            transform.Translate(new Vector2(1, transform.position.y) * speed * Time.deltaTime);
        else
            transform.Translate(new Vector2(-1, transform.position.y) * speed * Time.deltaTime);

        if (transform.position.x >= originTransform.x + xOffset)
        {
            dirRight = false;
        }

        if (transform.position.x <= originTransform.x - xOffset)
        {
            dirRight = true;
        }
    }
}
