using System.Collections;
using UnityEngine;

public class FallingState : State
{
    StateMachine stateMachine1;
    public FallingState(Character character) : base(character)
    {
        stateMachine1 = new StateMachine(new FallState(character));
    }

    public override State Update()
    {
        if (character.JumpBuffer > 0 && character.CoyoteTime > 0)
        {
            return new JumpingState(character);
        }

        if (character.IsGrounded)
        {
            return new GroundState(character);
        }

        if (((Player)character).OnWall)
        {
            return new ClimbingingState(character);
        }

        stateMachine1.Update();
        return base.Update();
    }
}