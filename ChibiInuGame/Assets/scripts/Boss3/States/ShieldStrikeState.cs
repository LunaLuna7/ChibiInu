using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStrikeState : IState {
	private KnightBossManager controller;
	

	public ShieldStrikeState(KnightBossManager controller)
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
