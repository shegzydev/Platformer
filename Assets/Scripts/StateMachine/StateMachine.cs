using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; set; }
    public State LastState { get; set; }
    public StateMachine(State initialState)
    {
        CurrentState = initialState;
    }

    public void Update()
    {
        try
        {
            LastState = CurrentState;
            CurrentState = CurrentState.Update();
            if (LastState != CurrentState)
            {
                CurrentState.Enter();
            }
        }
        catch (System.Exception e)
        {

        }
    }
}
