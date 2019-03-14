using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanCoin : MonoBehaviour {

    public float polymorphTime;
    public GameObject satanCoin;
    public GameObject particleEffect;
    public List<GameObject> enemieSummonPool0;
    public List<GameObject> enemieSummonPool1;
    public List<GameObject> enemieSummonPool2;
    public List<GameObject> enemieSummonPool3;
    public List<List<GameObject>> allEnemiesSummonPool;
    public SatanBossPhaseManager satanBossPhaseManager;
    private int currentPhase;

    private void Awake()
    {
        satanBossPhaseManager = FindObjectOfType<SatanBossPhaseManager>();
        allEnemiesSummonPool = new List<List<GameObject>>();
        currentPhase = satanBossPhaseManager.GetPhaseMap();
        InitAllEnemeyPools();
    }

    void InitAllEnemeyPools()
    {
        allEnemiesSummonPool.Add(enemieSummonPool0);
        allEnemiesSummonPool.Add(enemieSummonPool1);
        allEnemiesSummonPool.Add(enemieSummonPool2);
        allEnemiesSummonPool.Add(enemieSummonPool3);
    }

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

        List<GameObject> currentEnemyPool = GetEnemySummonPool();
        int enemyToSummon = Random.Range(0, currentEnemyPool.Count);
        Instantiate(currentEnemyPool[enemyToSummon], transform.position, Quaternion.identity);
        Destroy(satanCoin);

    }

    List<GameObject> GetEnemySummonPool()
    {
        return allEnemiesSummonPool[currentPhase];
    }
}
