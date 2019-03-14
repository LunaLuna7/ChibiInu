using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reset the battle detection and boss when player died
public class Boss2CheckPoint : MonoBehaviour {
	public BardBossStartPoint bossDetection;
	public BossWorld2 bardBoss;

    public List<GameObject> spikes;
    int countSpike;
    void Start()
    {
        countSpike = 0;
    }
    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
            StopAllCoroutines();
			bardBoss.Initialize();
			bossDetection.Reset();
            StartCoroutine(Delay());
            
        }
	}
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject spike in spikes)
        {
            spike.SetActive(false);
            SpikeAttack temp = spike.GetComponent<SpikeAttack>();
            if (countSpike < 4)
            {
                countSpike++;
                temp.SetStartPosition();
            }
            temp.ResetPosition();
        }
    }
}
