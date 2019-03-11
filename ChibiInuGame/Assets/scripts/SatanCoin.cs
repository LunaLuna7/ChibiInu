using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanCoin : MonoBehaviour {

    public float polymorphTime;
    public GameObject satanCoin;
    public GameObject particleEffect;
    public List<GameObject> enemieSummonPool;
	// Use this for initialization
	void Start () {
        StartCoroutine(TransformCoin());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Destroy(this.gameObject);
    }

    IEnumerator TransformCoin()
    {
        yield return new WaitForSeconds(polymorphTime - .4f);
        particleEffect.SetActive(true);
        yield return new WaitForSeconds(.4f);
        int enemyToSummon = Random.Range(0, enemieSummonPool.Count);
        Instantiate(enemieSummonPool[enemyToSummon], transform.position, Quaternion.identity);
        Destroy(satanCoin);

    }
}
