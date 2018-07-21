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
        //Transform shake = Mathf.Sin(Time.time * 5) * 10;
        //transform.position = shake.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>().isGrounded())
            StartCoroutine(TempPlatTrigger());
    }

    IEnumerator TempPlatTrigger()
    {
        yield return new WaitForSeconds(.3f);
        platform.SetActive(false);
        yield return new WaitForSeconds(inactiveTime);
        platform.SetActive(true);
    }
}
