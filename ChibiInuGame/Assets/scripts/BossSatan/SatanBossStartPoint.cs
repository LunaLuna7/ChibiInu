using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossStartPoint : MonoBehaviour {
	public CharacterController2D playerController;
	public SatanBossManager satanBoss;

	private bool hasStarted = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		//when touching player, showing the Boss
		if(other.tag == "Player" && !hasStarted)
		{
			hasStarted = true;
			playerController.m_Paralyzed = true;
			StartCoroutine(BattleStart());
		}
	}

	private IEnumerator BattleStart()
	{
		//player and knight boss staring at each other with love for a second
		yield return new WaitForSeconds(1);
		//then they start to kill each other
		playerController.m_Paralyzed = false;
		satanBoss.StartBattle();
	}

	public void Reset()
	{
		hasStarted = false;
	}
}
