using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    public GameObject projectile;
    public Transform projectileSpawn;
    public GameObject mouth;

    public float shootRate;
    public float startDelay = 0f;
    
	// Use this for initialization
	void Start () {
        InvokeRepeating("Shoot", startDelay, shootRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shoot()
    {
        StartCoroutine(OpenMouth());
        Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
    }

    IEnumerator OpenMouth()
    {
        mouth.SetActive(true);
        yield return new WaitForSeconds(.5f);
        mouth.SetActive(false);
    }
}
