using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRushState : IState {
	private KnightBossManager controller;
	

	public ShieldRushState(KnightBossManager controller)
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
