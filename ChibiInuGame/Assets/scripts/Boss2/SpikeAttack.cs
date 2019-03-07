using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAttack : MonoBehaviour {

    public float speed;
    public float yOffset;
    public float xOffset;
    Vector3 startPosition;

    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update () {
        transform.position = Vector3.MoveTowards(transform.position,
                 new Vector3(startPosition.x + xOffset, startPosition.y + yOffset, transform.position.z),
                 speed * Time.deltaTime);
    }

    public void SetStartPosition()
    {
        startPosition = transform.position;
    }
    public void ResetPosition()
    {
        transform.position = startPosition;
    }

}
