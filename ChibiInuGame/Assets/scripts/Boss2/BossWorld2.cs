using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWorld2 : MonoBehaviour {

    public float minTime;
    public float maxTime;
    [HideInInspector] public float stateTimeElapsed;

    public bool inState;
    private int lastState;
    private bool needPushPlayerAway = false;
	private StateMachine StateMachine = new StateMachine();
    //what states can one state transmit to
    private Dictionary<int, int[]> stateTransitionMap = new Dictionary<int, int[]>()
    {
        {0, new int[]{1, 2}},
        {1, new int[]{0, 1, 2, 3}},
        {2, new int[]{0, 1, 2, 3}},
        {3, new int[]{1, 2}},
    };

    //public GameObject spikesButton;


    
  

    private IState[] states;
    public BossHealth bossHealth;
    public BardBossMovementController movementController;
    public BardBossCloud cloudController;
    public Transform skillObjectsGroup;
    public GameObject player;

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
    public Transform playerCutscenePos;
    public TimeLineManager afterBattleTimeline;

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
            if(needPushPlayerAway && lastState != 3)
            {
                SwitchToState(3);
                needPushPlayerAway = false;
            }else{
                //get a random state the last one can translate to
                int action =  stateTransitionMap[lastState][Random.Range(0, stateTransitionMap[lastState].Length)];
                SwitchToState(action);
            }
        }
        else
            this.StateMachine.ExecuteStateUpdate();
    }

    private void SwitchToState(int num)
    {
        //this.StateMachine.ChangeState(states[1]);
        lastState = num;
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

    public void IndicateToPushPlayer()
    {
        needPushPlayerAway = true;
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

    //player defeated Bard Boss, play end cutscene
    public void EndBattle()
    {
        SoundEffectManager.instance.Stop("Boss");
        //stop using the current skills
        StopAllCoroutines();
        //destroy all skill objects
        for(int x = 0; x< skillObjectsGroup.childCount; ++x)
        {
            Destroy(skillObjectsGroup.GetChild(x).gameObject);
        }
        afterBattleTimeline.Play();
    }

    //functions for end level
    public void HidePlayer()
    {
        //move player to the battle field, just in case player died and return to save point. Use Boss's position can be fine
        player.transform.position = transform.position;
        player.SetActive(false);
    }

    public void Initialize()
    {
        //stop using the current skills
        StopAllCoroutines();
        //destroy all skill objects
        for(int x = 0; x< skillObjectsGroup.childCount; ++x)
        {
            Destroy(skillObjectsGroup.GetChild(x).gameObject);
        }
        transform.position = startPosition.position;
        bossHealth.health = bossHealth.maxHealth;
        bossHealth.healthBar.fillAmount = bossHealth.health;
        hasStarted = false;
        lastState = 0;
        needPushPlayerAway = false;
        //movement
        movementController.StopMoving();
        //cloud Color
        cloudController.SetColor(Color.white);
    }
}
