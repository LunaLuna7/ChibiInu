using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWaveState : IState {
	private KnightBossManager controller;
	

	public ShieldWaveState(KnightBossManager controller)
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
