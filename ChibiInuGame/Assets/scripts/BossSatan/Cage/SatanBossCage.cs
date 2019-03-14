using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossCage : MonoBehaviour {
	[SerializeField]private int health;
	public int maxHealth;
	[System.NonSerialized]public float timeTrack;
	public float timeBeforeDamageAgain;

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

	public void TakeDamage(int amount)
	{
		health -= amount;
		if(health <= 0 && !broken)
		{
			satanBossPhaseManager.GoToNextPhase();
			broken = true;
		}
	}

/* 
	IEnumerator BlinkSprite()
    {
        for (int i = 0; i < 6; ++i)
        {
            yield return new WaitForSeconds(.05f);
            if (m_SpriteRender.enabled == true)
            {
                m_SpriteRender.enabled = false;  //make changes
            }
            else
            {
                m_SpriteRender.enabled = true;   //make changes
            }
        }
    }
*/
}
