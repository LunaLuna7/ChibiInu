using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when touching player, Close the Wall and summon Boss
public class BardBossStartPoint : MonoBehaviour {
	public CharacterController2D playerController;
	public GameObject wallGroup;
	public SpriteRenderer[] wallSprites;
	public GameObject bardBoss;

	private bool hasStarted = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		//when touching player, showing the Boss
		if(other.tag == "Player" && !hasStarted)
		{
			hasStarted = true;
			playerController.m_Paralyzed = true;
			StartCoroutine(BattleStart());
            SoundEffectManager.instance.Play("Boss");
		}
	}

	private IEnumerator BattleStart()
	{
		//show all the walls
		wallGroup.SetActive(true);
		for(float x = 0; x<=1; x += 0.05f)
		{
			SetAllSpritesColor(new Color(1,1,1, x));
			yield return new WaitForSeconds(0.02f);
		}
		SetAllSpritesColor(Color.white);
		//player and bard boss staring at each other with love for a second
		yield return new WaitForSeconds(1);
		//then they start to kill each other
		playerController.m_Paralyzed = false;
		bardBoss.GetComponent<BossWorld2>().StartBattle();
	}

	//reset this trigger, so battle can start again
	public void Reset()
	{
		hasStarted = false;
		//reset Wall
		SetAllSpritesColor(new Color(1,1,1,0));
		wallGroup.SetActive(false);

	}

	//set the color of all wall sprites
	private void SetAllSpritesColor(Color color)
	{
		foreach(SpriteRenderer sprite in wallSprites)
		{
			sprite.color = color;
		}
	}

}
