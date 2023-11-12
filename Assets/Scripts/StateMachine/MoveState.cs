using System.Collections;
using UnityEngine;

public class MoveState : State
{
    public MoveState(Character character) : base(character)
    {
        character.Animator.Play("Run");
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
        character.facing = (character.hInput != 0) ? (character.hInput > 0 ? 0 : 180) : character.facing;
        character.RB.velocity = new Vector2(character.hInput * character.MoveSpeed, character.RB.velocity.y);

        if (Mathf.Abs(character.hInput) < 0.1f)
        {
            return new IdleState(character);
        }

        return this;
    }
}