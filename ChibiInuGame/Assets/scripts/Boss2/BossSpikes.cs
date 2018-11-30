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
     
    }

    private void OnEnable()
    {
        StartCoroutine(TurnOff());
        
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
