using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeedShot : MonoBehaviour {

    public GameObject seed;
    public Transform seedSpawn;

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

    private void OnEnable()
    {
        InvokeRepeating("Shoot", startDelay, shootRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    public void Shoot()
    {

            Instantiate(seed, seedSpawn.position, seedSpawn.rotation);
    }
}
