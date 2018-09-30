using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRope : MonoBehaviour {

    public GameObject rope;
    private RopeSwing ropeSwing;
    public bool callable;

    private void Awake()
    {
        ropeSwing = rope.GetComponent<RopeSwing>();
        callable = true;
        ropeSwing.Off = true;
        
    }

    private void Update()
    {
        if (ropeSwing.Off)
        {
          callable = false;
          StartCoroutine(CallDelay());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && callable )
        {
            if (Input.GetKeyDown(KeyCode.Z) && ropeSwing.Off)
            {
                Debug.Log("shit");
                rope.SetActive(true);
            }
        }
    }

    IEnumerator CallDelay()
    {
        yield return new WaitForSeconds(1f);
        ropeSwing.grabbable = true;
        callable = true;
    }



}
