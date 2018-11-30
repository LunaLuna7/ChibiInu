using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {

    public GameObject projectiles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ShootProjectile();
        }
	}

    public void ShootProjectile()
    {
        Instantiate(projectiles, transform.position, transform.rotation);
    }
}
