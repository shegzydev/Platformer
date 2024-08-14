using System.Collections;
using UnityEngine;

public class ClimbingingState : State
{
    StateMachine stateMachine1;
    RaycastHit2D wallHit;

    public ClimbingingState(Character character) : base(character)
    {
        Debug.Log("Climb");

        character.Animator.Play("Hang");

        character.RB.velocity = Vector2.zero;
        character.RB.gravityScale = 0;
    }

    public override State Update()
    {
        if (!((Player)character).OnWall || character.IsGrounded)
        {
            character.RB.gravityScale = 1;
            return new GroundState(character);
        }


        RaycastHit2D WallHitLeft = Physics2D.Raycast(character.transform.position, Vector2.left, 0.5f, character.GroundLayer);
        RaycastHit2D WallHitRight = Physics2D.Raycast(character.transform.position, Vector2.right, 0.5f, character.GroundLayer);
        wallHit = new RaycastHit2D();

        if (!WallHitLeft && WallHitRight) wallHit = WallHitRight;
        else if (!WallHitRight && WallHitLeft) wallHit = WallHitLeft;
        else if (WallHitLeft && WallHitRight)
        {
            wallHit = WallHitLeft.distance < WallHitRight.distance ? WallHitLeft : WallHitRight;
        }

        if (wallHit)
        {
            Debug.DrawLine(character.transform.position, wallHit.point);
        }

        character.transform.position += (Vector3)wallHit.normal * (0.055f - wallHit.distance);
        character.facing = wallHit.normal.x < 0 ? 0 : 180;
        
        if (InputManager.JumpPressed && InputManager.LHInput != 0)
        {
            if (Mathf.Sign(InputManager.LHInput) == Mathf.Sign(wallHit.normal.x))
            {
                character.RB.gravityScale = 1;
                return new JumpingState(character);
            }
        }

        character.RB.gravityScale = Mathf.MoveTowards(character.RB.gravityScale, 0.5f, Time.deltaTime * 2);

        stateMachine1.Update();
        return base.Update();
    }
}