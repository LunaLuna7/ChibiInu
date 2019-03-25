using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBossManager : MonoBehaviour {
	public BossHealth bossHealth;
	public KnightBossMovementController movementController;
	private bool hasStarted = false;
	private bool hasEnded = false;
	public SwitchCamera cameraSwitcher;

	public GameObject player;
	public Transform startPosition;
	public GameObject boundary;
	private Vector3 scale;

	[Header("For Skills")]
	private StateMachine stateMachine = new StateMachine();
	private IState[] states;
	public GameObject shieldProjectile;
	public GameObject spikeShieldProjectile;
	public Transform skillObjectsGroup;//transform to put all skill objects, easy for removing objects when reset
	public TimeLineManager afterBattleTimeline;
	void Awake()
	{
		//set states
		states =  new IState[4];
		states[0] = new KnightBossMovingState(this);
		states[1] = new ShieldWaveState(this);
		states[2] = new ShieldRushState(this);
		states[3] = new ShieldStrikeState(this);
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
        //destroy all skill objects
        for(int x = 0; x< skillObjectsGroup.childCount; ++x)
        {
            Destroy(skillObjectsGroup.GetChild(x).gameObject);
        }
        transform.position = startPosition.position;
        bossHealth.health = bossHealth.maxHealth;
        bossHealth.healthBar.fillAmount = bossHealth.health;
        GetComponent<KnightBossPhaseManager>().Reset();
        hasStarted = false;
		transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
		boundary.SetActive(false);
    }

	public void StartBattle()
	{
		if(hasEnded)
			return;
		hasStarted = true;
		boundary.SetActive(true);
		stateMachine.ChangeState(states[0]);
	}

	public void EndBattle()
    {
		hasEnded = true;
		SoundEffectManager.instance.Stop("Boss");
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
		player.transform.position = transform.position;
		player.SetActive(false);
		cameraSwitcher.ChangeCamera(3);
	}

}
