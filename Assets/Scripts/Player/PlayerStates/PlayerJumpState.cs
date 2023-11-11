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
        fsmEntity.StartCoroutine(Jump());
    }

    public override void Update()
    {
        base.Update();

        fsmEntity.Animator.SetFloat("JumpFactor", fsmEntity.RB.velocity.y);

        if (InputManager.Input.Input.Jump.WasPressedThisFrame() && !doubleJump)
        {
            StartJump();
            doubleJump = true;
        }

        if (InputManager.Input.Input.Fire.WasPressedThisFrame())
        {
            Debug.Log("ShootInJump");
            fsmEntity.Animator.SetTrigger("Shoot");
        }

        fsmEntity.RB.velocity = new Vector2(hInput * fsmEntity.MoveSpeed, fsmEntity.RB.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    void StartJump()
    {
        hInput = Input.GetAxis("Horizontal");

        fsmEntity.transform.position += new Vector3(0f, 0.15f, 0);
        fsmEntity.RB.velocity = new Vector2(hInput * fsmEntity.MoveSpeed, Mathf.Sqrt(2 * 9.81f * fsmEntity.JumpHeight));
        
        fsmEntity.Animator.SetTrigger("Jump");
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.3f);
        fsmEntity.Animator.ResetTrigger("Jump");

        yield return new WaitUntil(() => fsmEntity.IsGrounded);
        playerStateMachine.SwitchState(StateType.GroundState);
    }
}
