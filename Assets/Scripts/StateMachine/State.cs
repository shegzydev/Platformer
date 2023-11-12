using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected Character character;
    protected StateMachine stateMachine;

    public State(Character character)
    {
        this.character = character;
    }

    public virtual void Enter() { }
    public virtual State Update() { return this; }
    public virtual void Exit() { }
}
