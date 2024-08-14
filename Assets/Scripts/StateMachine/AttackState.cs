using System.Collections;
using UnityEngine;


public class AttackState : State
{
    public AttackState(Character character) : base(character)
    {
        character.Animator.Play("Attack", 0, 0);

        character.facing = (character.hInput != 0) ? (character.hInput > 0 ? 0 : 180) : character.facing;
    }

    public override State Update()
    {


        return base.Update();
    }
}
