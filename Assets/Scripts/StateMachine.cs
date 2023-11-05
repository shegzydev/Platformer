using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    JumpState, GroundState, PatrolState, AggressiveState
}

public class StateMachine
{
    public State CurrentPlayerState { get; set; }
    public State[] StateList { get; set; }

    public void Initialize(StateType stateType, State startState)
    {
        StateList = new State[System.Enum.GetValues(typeof(StateType)).Length];
        
        StateList[(int)stateType] = startState;

        CurrentPlayerState = startState;
        CurrentPlayerState.Enter();
    }

    public void AddState(StateType stateType, State state)
    {
        StateList[(int)stateType] = state;
    }

    public void SwitchState(StateType stateType)
    {
        CurrentPlayerState.Exit();
        CurrentPlayerState = StateList[(int)stateType];
        CurrentPlayerState.Enter();
    }

    public void Update()
    {
        CurrentPlayerState.Update();
    }
}
