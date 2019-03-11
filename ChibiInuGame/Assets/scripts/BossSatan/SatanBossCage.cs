using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossCage : MonoBehaviour {
	private int health;
	public int maxHealth;
	private bool broken = false;
	public SatanBossPhaseManager satanBossPhaseManager;

	void OnEnable()
	{
		Reset();
	}
	
	public void Reset()
	{
		health = maxHealth;
		broken = false;
	}

	public void GetHurt(int amount)
	{
		health -= amount;
		if(health <= 0 && !broken)
		{
			satanBossPhaseManager.GoToNextPhase();
			broken = true;
			gameObject.SetActive(false);
		}
	}

}
