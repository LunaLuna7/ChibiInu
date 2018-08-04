using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawn : MonoBehaviour {

    public GameObject item;
    public Vector2 boundaries;
    public float spawnWait;
    public float startWait;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-boundaries.x, boundaries.x), Random.Range(-boundaries.y, boundaries.y));
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(item, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
