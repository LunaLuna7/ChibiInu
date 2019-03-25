using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine {

    private IState currentState; //State that is executting
    private IState previousState; //previous state that executted

    //Change state into the param one
    public void ChangeState(IState newState) {

        if(this.currentState != null)
            this.currentState.ExitState();
        
        this.previousState = this.currentState;
        this.currentState = newState;
        this.currentState.EnterState();
    }

    //Triggers the currentState
    public void ExecuteStateUpdate(){
        var runningState = this.currentState;
        if(runningState != null)
            this.currentState.ExecuteState();
    }

    //Makes previous state the current state(may not use at all)
    public void SwitchToPreviousState(){
        this.currentState.ExitState();
        this.currentState = this.previousState;
        this.currentState.EnterState();
    }

    public void Stop()
    {
        if(this.currentState != null)
            this.currentState.ExitState();
        this.previousState = this.currentState;
        this.currentState = null;
    }
}
