using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossMovingState : IState {
	private SatanBossManager controller;
	

	public SatanBossMovingState(SatanBossManager controller)
	{
		this.controller = controller;
	}

	public void EnterState()
	{
		controller.StartCoroutine(Move(12));
	}

	public void ExecuteState()
	{

	}

	public void ExitState()
	{
		
	}

	private IEnumerator Move(float speed)
	{
		//get a position to go to
		Transform[] possibleLocations = controller.movementController.possibleLocations;
		Vector3 pos = possibleLocations[Random.Range(0, possibleLocations.Length)].position;
		//loop until get a position that is not the current position
		while(pos == controller.transform.position)
			pos = possibleLocations[Random.Range(0, possibleLocations.Length)].position;
		//switch state after finish moving
		yield return controller.movementController.MoveTo(pos, speed);
		controller.SwitchState();
	}
}
