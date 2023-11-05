using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FSMEntity player;
    protected StateMachine playerStateMachine;

    public State(FSMEntity player, StateMachine stateMachine)
    {
        this.playerStateMachine = stateMachine;
        this.player = player;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
