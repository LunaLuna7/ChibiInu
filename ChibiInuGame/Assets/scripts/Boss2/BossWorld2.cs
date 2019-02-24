using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWorld2 : MonoBehaviour {

    public float minTime;
    public float maxTime;
    [HideInInspector] public float stateTimeElapsed;

    public bool inState;
	private StateMachine StateMachine = new StateMachine();


    //public GameObject spikesButton;


    
  

    private IState[] states;
    public BossHealth bossHealth;
    public BardBossMovementController movementController;
    public BardBossCloud cloudController;
    public Transform skillObjectsGroup;

    [Header("for FastSpikeState")]
    public GameObject warningBlock;
    public GameObject fastSpike;

    [Header("for FluteSpikeSong")]
    public GameObject fluteSpike;
    [Header("for WindFurry")]
    public GameObject wind;
    [Header("for start/restart")]
    private bool hasStarted = false;
    public Transform startPosition;

   

    private void Awake()
    {
        //create all states
        states = new IState[4];
        states[0] = new IdleState(this);
        //states[1] = new SpikeState(this);
        states[1] = new FastSpikeState(this);
        states[2] = new FluteSpikeSongState(this);
        states[3] = new WindFurryState(this);

    }

    void Start()
    {
        Initialize();
    }


    private void Update()
    {
        //if haven't start, do nothing
        if(!hasStarted)
            return;
        if (!inState)
        {
            var action = Random.Range(0, 4);
            SwitchToState(action);
        }
        else
            this.StateMachine.ExecuteStateUpdate();
    }

    private void SwitchToState(int num)
    {
        //this.StateMachine.ChangeState(states[1]);
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

    //====================================================================================================
    //Initialize values and Restart
    //====================================================================================================
    public void StartBattle()
    {
        this.StateMachine.ChangeState(new IntroState());
        //time for the animation
        this.StateMachine.ChangeState(new IdleState(this));
        movementController.StartMoving();
        hasStarted = true;
    }

    public void Initialize()
    {
        //stop using the current skills
        StopAllCoroutines();
        transform.position = startPosition.position;
        //destroy all skill objects
        for(int x = 0; x< skillObjectsGroup.childCount; ++x)
        {
            Destroy(skillObjectsGroup.GetChild(x).gameObject);
        }
        //face left
        GetComponent<SpriteRenderer>().flipX = true;
        bossHealth.health = bossHealth.maxHealth;
        hasStarted = false;
        //movement
        movementController.StopMoving();
        //cloud Color
        cloudController.SetColor(Color.white);
    }
}
