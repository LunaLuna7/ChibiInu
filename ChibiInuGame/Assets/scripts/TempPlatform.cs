using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlatform : MonoBehaviour {

    public GameObject platform;
    [HideInInspector] public float inactiveTime;
	// Use this for initialization
	void Start () {
        inactiveTime = 4f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>().isGrounded())
            StartCoroutine(TempPlatTrigger());
    }

    IEnumerator TempPlatTrigger()
    {
        yield return new WaitForSeconds(.2f);
        platform.SetActive(false);
        yield return new WaitForSeconds(inactiveTime);
        platform.SetActive(true);
    }
}
