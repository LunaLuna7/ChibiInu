using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBossMovingState : IState {
	private KnightBossManager controller;
	

	public KnightBossMovingState(KnightBossManager controller)
	{
		this.controller = controller;
	}

	public void EnterState()
	{

	}

	public void ExecuteState()
	{

	}

	public void ExitState()
	{
		controller.SwitchState();
	}
}
