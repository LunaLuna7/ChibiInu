using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSlimeShot : MonoBehaviour {

    public GameObject projectile;
    public Transform projectileSpawn;
    public StateController sc;
    
    public float shootRate;
    public float startDelay = 0f;
    public bool facingRight;

    private SatanBossManager satanBossManager;

    private void Awake()
    {
        satanBossManager = FindObjectOfType<SatanBossManager>().GetComponent<SatanBossManager>();
        
    }

    // Use this for initialization
    void Start()
    {
        sc = gameObject.GetComponentInParent<StateController>();
        //InvokeRepeating("Shoot", startDelay, shootRate);
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
        
        if (facingRight)
        {
            GameObject a = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
            if (satanBossManager != null)
                a.transform.SetParent(satanBossManager.skillObjectsGroup);
            Transform trans = a.GetComponent<Transform>();
            trans.localScale = trans.localScale * -1;
        }

        else
        {
            GameObject a = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
            if (satanBossManager != null)
                a.transform.SetParent(satanBossManager.skillObjectsGroup);
        }
    }
}
