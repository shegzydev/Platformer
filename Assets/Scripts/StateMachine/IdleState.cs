using System.Collections;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Character character) : base(character)
    {
        character.Animator.Play("Idle");
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override State Update()
    {
        character.Animator.Play("Idle");

        character.RB.velocity = Vector2.MoveTowards(character.RB.velocity, Vector2.zero, 100 * Time.deltaTime);
        
        character.facing = (character.hInput != 0) ? (character.hInput > 0 ? 0 : 180) : character.facing;
        
        if (Mathf.Abs(character.hInput) > 0.1f) return new MoveState(character);
        
        return base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
