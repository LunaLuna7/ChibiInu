using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatanBossManager : MonoBehaviour {
	public KnightBossMovementController movementController;
	private bool hasStarted = false;

	public GameObject player;
	public Transform startPosition;


	[Header("For Skills")]
	private StateMachine stateMachine = new StateMachine();
	private IState[] states;
	public Transform skillObjectsGroup;//transform to put all skill objects, easy for removing objects when reset
	public TimeLineManager afterBattleTimeline;
	[Header("Noise")]
	public Image noiseImage;
    [Header("HellBall")]
    public GameObject hellBall;
	

	void Awake()
	{
		//set states
		states =  new IState[3];
		states[0] = new SatanBossMovingState(this);
		states[1] = new SatanBossNoiseState(this);
        states[2] = new SatanBossHellBallState(this);
	}

	// Use this for initialization
	void Start () {
		Initialize();
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
		this.stateMachine.ChangeState(states[next]);
	}

	public void Initialize()
    {
        //stop using the current skills
        StopAllCoroutines();
		movementController.StopAllCoroutines();
        //destroy all skill objects
        for(int x = 0; x< skillObjectsGroup.childCount; ++x)
        {
            Destroy(skillObjectsGroup.GetChild(x).gameObject);
        }
		//reset phase
		GetComponent<SatanBossPhaseManager>().Reset();
        transform.position = startPosition.position;
        hasStarted = false;

    }

	public void StartBattle()
	{
		hasStarted = true;
		stateMachine.ChangeState(states[0]);
	}

	public void EndBattle()
    {
        //stop using the current skills
        StopAllCoroutines();
		movementController.StopAllCoroutines();
        //destroy all skill objects
        for(int x = 0; x< skillObjectsGroup.childCount; ++x)
        {
            Destroy(skillObjectsGroup.GetChild(x).gameObject);
        }
        afterBattleTimeline.Play();
    }

	public void HidePlayer()
	{
		player.SetActive(false);
	}

}
