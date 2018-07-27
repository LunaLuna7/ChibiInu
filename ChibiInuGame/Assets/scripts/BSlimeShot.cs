using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSlimeShot : MonoBehaviour {

    public GameObject projectile;
    public Transform projectileSpawn;

    public float shootRate;
    public float startDelay = 0f;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Shoot", startDelay, shootRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
    }
}
