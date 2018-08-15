using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMask : MonoBehaviour {

    public GameObject player;

    [Range(0.01f, 0.2f)]
    public float flickTime;

    [Range(0.01f, 0.09f)]
    public float addSize;

    [Range(0.01f, 4f)]
    public float itemAddSize;

    public float shrinkRate;

    float timer = 0;
    bool grow = true;
    bool itemOn = false;
	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(0, 0, 1);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.localScale = new Vector3(transform.localScale.x + itemAddSize, transform.localScale.y + itemAddSize, transform.localScale.z);
            itemOn = true;
        }

        if (itemOn)
        {
            SlowlyShrink();
        }
        timer += Time.deltaTime;

        if(timer > flickTime || !itemOn)
        {
            if (grow)
            {
                transform.localScale = new Vector3(transform.localScale.x + addSize, transform.localScale.y + addSize, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x - addSize, transform.localScale.y - addSize, transform.localScale.z);
            }
            timer = 0;
            grow = !grow;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10f);
	}

    void SlowlyShrink()
    {
        if(transform.localScale.x > 0f && transform.localScale.y > 0f)
            transform.localScale = new Vector3(transform.localScale.x - shrinkRate, transform.localScale.y - shrinkRate, transform.localScale.z);
        else
        {
            itemOn = false;
        }
    }

    public void LightUp()
    {
        transform.localScale = new Vector3(transform.localScale.x + itemAddSize, transform.localScale.y + itemAddSize, transform.localScale.z);
        itemOn = true;

    }
}
