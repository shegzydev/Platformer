using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class RollingState : State
{
    Vector3 rollDir;
    bool FinishRoll;
    public RollingState(Character character) : base(character)
    {
        character.Animator.Play("Roll", 0, 0);
        rollDir = character.transform.right;
        character.StartCoroutine(FollowRoll());
        ((Player)character).SetCollLayer(false);
    }

    public override State Update()
    {
        if (character.JumpBuffer > 0)
        {
            ((Player)character).SetCollLayer(true);
            return new JumpingState(character);
        }
        else if (!character.IsGrounded)
        {
            ((Player)character).SetCollLayer(true);
            return new FallingState(character);
        }

        character.RB.velocity = new Vector2(rollDir.x * character.MoveSpeed * 1.5f, -1f);
        if (FinishRoll)
        {
            ((Player)character).SetCollLayer(true);
            return new GroundState(character);
        }
        return base.Update();
    }

    IEnumerator FollowRoll()
    {
        yield return new WaitUntil(() => character.Animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"));
        yield return new WaitUntil(() => character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
        FinishRoll = true;
    }
}