using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reset the battle detection and boss when player died
public class SatanBossCheckPoint : MonoBehaviour {
	public SatanBossStartPoint bossDetection;
	public SatanBossManager boss;

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			boss.Initialize();
			bossDetection.Reset();
        }
	}
}
