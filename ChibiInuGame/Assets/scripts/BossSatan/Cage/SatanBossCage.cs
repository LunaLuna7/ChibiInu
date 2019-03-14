using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossCage : MonoBehaviour {
	[SerializeField]private int health;
	public int maxHealth;
	[System.NonSerialized]public float timeTrack;
	private bool broken = false;
	public SatanBossPhaseManager satanBossPhaseManager;
	public float timeBeforeDamageAgain;
	//movement 
	public bool moving;
	public float moveSpeed;
	public Transform[] positionList;
	private int currentPositionIndex;
	

	void OnEnable()
	{
		Reset();
	}

	private float ratio = 0;
	private float totalLength;
	public void Update()
	{
		int next = (currentPositionIndex + 1)% positionList.Length;
		transform.position = Vector3.Lerp(positionList[currentPositionIndex].position, positionList[next].position, ratio);
		//change ratio
		ratio += (moveSpeed * Time.deltaTime)/totalLength;
		if(ratio > 1)
		{
			ratio = 0;
			currentPositionIndex = next;
			next = (currentPositionIndex + 1)% positionList.Length;
			totalLength = Vector2.Distance(positionList[currentPositionIndex].position, positionList[next].position);
		}
	}
	
	public void Reset()
	{
		health = maxHealth;
		broken = false;
		currentPositionIndex = 0;
		ratio = 0;
		if(moving && positionList.Length > 0)
		{
			transform.position = positionList[0].position;
			int next = (currentPositionIndex + 1)% positionList.Length;
			totalLength = Vector2.Distance(positionList[currentPositionIndex].position, positionList[next].position);
		}
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



}
