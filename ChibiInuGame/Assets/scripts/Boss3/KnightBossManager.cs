using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBossManager : MonoBehaviour {
	public BossHealth bossHealth;
	public KnightBossMovementController movementController;
	private bool hasStarted = false;

	public GameObject player;
	

	[Header("For Skills")]
	private StateMachine stateMachine = new StateMachine();
	private IState[] states;
	public GameObject sheildProjectile;
	public Transform skillObjectsGroup;//transform to put all skill objects, easy for removing objects when reset

	void Awake()
	{
		//set states
		states =  new IState[4];
		states[0] = new KnightBossMovingState(this);
		states[1] = new ShieldWaveState(this);
		states[2] = new ShieldRushState(this);
		states[3] = new ShieldStrikeState(this);
	}

	// Use this for initialization
	void Start () {
		hasStarted = true;
		stateMachine.ChangeState(states[0]);
	}
	
	// Update is called once per frame
	void Update () {
		//if haven't start, do nothing
        if(!hasStarted)
            return;
        this.stateMachine.ExecuteStateUpdate();
	}

	public void SwitchState()
	{
		int next = Random.Range(0, states.Length);
		this.stateMachine.ChangeState(states[0]);
	}
}
