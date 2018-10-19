using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSlimeShot : MonoBehaviour {

    public GameObject projectile;
    public Transform projectileSpawn;
    public StateController sc;

    public float shootRate;
    public float startDelay = 0f;

    // Use this for initialization
    void Start()
    {
        sc = gameObject.GetComponentInParent<StateController>();
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
        Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
    }
}
