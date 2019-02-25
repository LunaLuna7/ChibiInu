using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reset the battle detection and boss when player died
public class Boss2CheckPoint : MonoBehaviour {
	public BardBossStartPoint bossDetection;
	public BossWorld2 bardBoss;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			bardBoss.Initialize();
			bossDetection.Reset();
		}
	}
}
