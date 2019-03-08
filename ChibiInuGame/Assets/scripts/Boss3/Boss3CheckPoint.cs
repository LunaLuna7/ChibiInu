using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reset the battle detection and boss when player died
public class Boss3CheckPoint : MonoBehaviour {
	public KnightBossStartPoint bossDetection;
	public KnightBossManager knightBoss;

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			knightBoss.Initialize();
			bossDetection.Reset();
        }
	}
}
