using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatanBossManager : MonoBehaviour {
	public SatanBossMovementController movementController;
    public SatanBossPhaseManager phaseManager;
	private bool hasStarted = false;
    private bool hasEnded = false;
    public SwitchCamera cameraSwitcher;

	public GameObject player;
	public Transform startPosition;
    private Vector3 scale;

	[Header("For Skills")]
	private StateMachine stateMachine = new StateMachine();
	private IState[] states;
	public Transform skillObjectsGroup;//transform to put all skill objects, easy for removing objects when reset
	public TimeLineManager afterBattleTimeline;

    [Header("Noise")]
	public Image noiseImage;

    [Header("HellBall")]
    public GameObject hellBall;

    [Header("Coin")]
    public List<Transform> coinSpawnLocLvlp0;
    public List<Transform> coinSpawnLocLvlp1;
    public List<Transform> coinSpawnLocLvlp2;
    public List<Transform> coinSpawnLocLvlp3;
    public List<List<Transform>> allCoinSpawns;

    public GameObject coinAttack;

    [Header("End Cutscene")]
    public GameObject cameraFocus;
    public Vector3 endingCameraFocusPosition;

    void Awake()
	{
        allCoinSpawns = new List<List<Transform>>();
		//set states
		states =  new IState[3];
		states[0] = new SatanBossMovingState(this);
		//states[1] = new SatanBossNoiseState(this);
        states[1] = new SatanBossHellBallState(this);
        states[2] = new SatanBossCoinState(this);
        scale = transform.localScale;
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
        if(hasEnded)
			return;
        //stop using the current skills
        StopAllCoroutines();
		movementController.StopAllCoroutines();
        CleanSkillObjects();
		//reset phase
		GetComponent<SatanBossPhaseManager>().Reset();
        transform.position = startPosition.position;
        hasStarted = false;
        //face player
        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        InitAllCoinLocations();
    }

    //Creates 2Dlist with all coin phases locations, we separate each set of locations base on index
    void InitAllCoinLocations()
    {
        allCoinSpawns.Add(coinSpawnLocLvlp0);
        allCoinSpawns.Add(coinSpawnLocLvlp1);
        allCoinSpawns.Add(coinSpawnLocLvlp2);
        allCoinSpawns.Add(coinSpawnLocLvlp3);
    }

	public void StartBattle()
	{
        if(hasEnded)
			return;
		hasStarted = true;
		stateMachine.ChangeState(states[0]);
	}

	public void EndBattle()
    {
        hasEnded = true;
        //stop using the current skills
        StopAllCoroutines();
		movementController.StopAllCoroutines();
        CleanSkillObjects();
        //phaseManager.SetEndingPhase();
        afterBattleTimeline.Play();
    }

    public void CleanSkillObjects()
    {
        //destroy all skill objects
        for(int x = 0; x< skillObjectsGroup.childCount; ++x)
        {
            Destroy(skillObjectsGroup.GetChild(x).gameObject);
        }
    }

	public void HidePlayer()
	{
		player.transform.position = transform.position;
		player.SetActive(false);
        cameraSwitcher.ChangeCamera(3);
    }

    public void SetFinalCameraFocus()
    {
        cameraFocus.transform.position = endingCameraFocusPosition;
    }

}
