using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : State
{
    bool doubleJump;
    float hInput;
    public PlayerJumpState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        doubleJump = false;

        StartJump();
        player.StartCoroutine(Jump());
    }

    public override void Update()
    {
        base.Update();

        player.Animator.SetFloat("JumpFactor", player.RB.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !doubleJump)
        {
            StartJump();
            doubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("ShootInJump");
            player.Animator.SetTrigger("Shoot");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    void StartJump()
    {
        hInput = Input.GetAxis("Horizontal");

        player.transform.position += new Vector3(0f, 0.15f, 0);
        player.RB.velocity = new Vector2(hInput * player.MoveSpeed, Mathf.Sqrt(2 * 9.81f * player.JumpHeight));
        player.Animator.SetTrigger("Jump");
    }

    IEnumerator Jump()
    {
        yield return new WaitUntil(() => player.IsGrounded);
        playerStateMachine.SwitchState(StateType.GroundState);
    }
}
