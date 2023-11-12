using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : State
{
    StateMachine stateMachine1;

    public GroundState(Character character) : base(character)
    {
        stateMachine1 = new StateMachine(new IdleState(character));
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override State Update()
    {
        base.Update();

        if(Input.GetMouseButtonDown(1))
        {
            return new RollingState(character);
        }

        if (character.JumpBuffer > 0)
        {
            return new JumpingState(character);
        }

        if (!character.IsGrounded)
        {
            return new FallingState(character);
        }

        stateMachine1.Update();
        return base.Update();
    }

    public override void Exit()
    {
        base.Exit();

    }
}
