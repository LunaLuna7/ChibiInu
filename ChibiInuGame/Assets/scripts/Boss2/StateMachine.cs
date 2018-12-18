using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

    private IState currentState;
    private IState previousState;

    public void ChangeState(IState newState) {

        if(this.currentState != null)
            this.currentState.ExitState();
        
        this.previousState = this.currentState;
        this.currentState = newState;
        this.currentState.EnterState();
    }

    public void ExecuteStateUpdate(){
        var runningState = this.currentState;
        if(runningState != null)
            this.currentState.ExecuteState();
    }

    public void SwitchToPreviousState(){
        this.currentState.ExitState();
        this.currentState = this.previousState;
        this.currentState.EnterState();
    }
}
