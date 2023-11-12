using System.Collections;
using UnityEngine;

public class FallState : State
{
    public FallState(Character character) : base(character)
    {
        character.Animator.Play("Fall");
        Debug.Log("falling");
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override State Update()
    {
        return base.Update();
    }
}