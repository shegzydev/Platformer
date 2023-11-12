using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    StateMachine stateMachine1;
    bool land;

    public JumpingState(Character character) : base(character)
    {
        land = false;
        stateMachine1 = new StateMachine(new JumpState(character));
        character.StartCoroutine(FollowJump());
    }

    public override void Enter()
    {

    }

    public override State Update()
    {
        if (land)
        {
            Debug.Log("BacktoGround");
            return new GroundState(character);
        }

        stateMachine1.Update();
        return base.Update();
    }


    IEnumerator FollowJump()
    {
        yield return new WaitUntil(() => !character.IsGrounded);
        yield return new WaitUntil(() => character.IsGrounded);
        land = true;
    }
    public override void Exit()
    {

    }
}
