using System.Collections;
using UnityEngine;

public class JumpState : State
{
    public JumpState(Character character) : base(character)
    {
        Debug.Log("flyingUp");

        character.RB.velocity = new Vector2(character.RB.velocity.x, Mathf.Sqrt(2 * 9.81f * character.JumpHeight));
        character.Animator.Play("Jump");
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

        if (character.RB.velocity.y < 0)
        {
            return new FallState(character);
        }
        return base.Update();
    }
}