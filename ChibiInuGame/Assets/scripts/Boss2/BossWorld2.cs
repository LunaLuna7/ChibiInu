using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWorld2 : MonoBehaviour {

    public float minTime;
    public float maxTime;
    [HideInInspector] public float stateTimeElapsed;

    public bool inState;
	private StateMachine StateMachine = new StateMachine();


    public GameObject spikesLeft;
    public GameObject spikesRight;

    //Attacks Prefabs
    public GameObject sideSpike;
    public GameObject frontalSpike;

    private IState[] states;

    [Header("for FastSpikeState")]
    public GameObject warningBlock;
    public GameObject fastSpike;

    [Header("for FluteSpikeSong")]
    public GameObject fluteSpike;
    [Header("for WindFurry")]
    public GameObject wind;

    private void Awake()
    {
        //create all states
        states = new IState[5];
        states[0] = new IdleState(this);
        states[1] = new SpikeState(this);
        states[2] = new FastSpikeState(this);
        states[3] = new FluteSpikeSongState(this);
        states[4] = new WindFurryState(this);
    }

    private void Start()
    {
        this.StateMachine.ChangeState(new IntroState());
        //time for the animation
        this.StateMachine.ChangeState(new IdleState(this));
    }

    private void Update()
    {
        if (!inState)
        {
            var action = Random.Range(0, 5);
            SwitchToState(action);
        }
        else
            this.StateMachine.ExecuteStateUpdate();
    }

    private void SwitchToState(int num)
    {
        //this.StateMachine.ChangeState(states[4]);
        this.StateMachine.ChangeState(states[num]);
        /* 
        switch (num)
        {
            case 1:
                this.StateMachine.ChangeState(states[0]);
                return;
            case 2:
                this.StateMachine.ChangeState(states[1]);
                return;
        }*/
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

}
