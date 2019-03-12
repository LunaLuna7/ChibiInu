using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossNoiseState : IState {
	private SatanBossManager controller;
	private Color targetColor = new Color(0,0,0,150f/255);

	public SatanBossNoiseState(SatanBossManager controller)
	{
		this.controller = controller;
	}

	public void EnterState()
	{
		float speed = 1;
		float force = 3000;
		controller.StartCoroutine(Noise(speed, force));
	}

	public void ExecuteState()
	{

	}

	public void ExitState()
	{
		
	}

	public IEnumerator Noise(float speed, float force)
	{
		Color current = new Color(0, 0, 0, 0);
		//make screen darker to half way
		float halfA = targetColor.a/2;
		for(float x = 0; x < halfA; x += speed/100)
		{
			current.a = x;
			controller.noiseImage.color = current;
			yield return new WaitForFixedUpdate();
		}
		//push player in the middle
		current.a = halfA;
		controller.noiseImage.color = current;
		yield return new WaitForFixedUpdate();
		Vector2 direction = (controller.player.transform.position - controller.transform.position).normalized;
		controller.player.GetComponent<Rigidbody2D>().AddForce(direction * force);
		//continue to become darker
		for(float x = halfA; x < targetColor.a; x += speed/100)
		{
			current.a = x;
			controller.noiseImage.color = current;
			yield return new WaitForFixedUpdate();
		}
		//become transparent again
		for(float x = targetColor.a; x > 0; x -= speed/100)
		{
			current.a = x;
			controller.noiseImage.color = current;
			yield return new WaitForFixedUpdate();
		}
		current.a = 0;
		controller.noiseImage.color = current;
		//switch state
		controller.SwitchState();
	}
}
