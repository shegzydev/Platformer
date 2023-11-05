using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FSMEntity fsmEntity;
    protected StateMachine playerStateMachine;

    public State(FSMEntity fSMEntity, StateMachine stateMachine)
    {
        this.playerStateMachine = stateMachine;
        this.fsmEntity = fSMEntity;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
