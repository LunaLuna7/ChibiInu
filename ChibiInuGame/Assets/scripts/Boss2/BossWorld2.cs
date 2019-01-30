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
    public GameObject platformSpike;
    

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
            var action = Random.Range(1, 3);
            RandomState(action);
        }
        else
            this.StateMachine.ExecuteStateUpdate();
    }

    private void RandomState(int num)
    {
        switch (num)
        {
            case 1:
                this.StateMachine.ChangeState(new IdleState(this));
                return;

            case 2:
                this.StateMachine.ChangeState(new SpikeState(this));
                return;
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

}
