using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : State
{
    float facing;
    float hInput;

    public PlayerGroundState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        fsmEntity.Animator.SetTrigger("ToGround");
    }

    public override void Update()
    {
        base.Update();

        hInput = InputManager.Input.Input.Horizontal.ReadValue<float>();
        facing = (hInput != 0) ? (hInput > 0 ? 0 : 180) : facing;

        fsmEntity.Animator.SetFloat("Move", Mathf.Abs(hInput));
        fsmEntity.transform.rotation = Quaternion.Euler(0, facing, 0);

        Vector2 vel = fsmEntity.RB.velocity;
        vel.x = hInput * fsmEntity.MoveSpeed;
        fsmEntity.RB.velocity = vel;

        if (InputManager.Input.Input.Jump.WasPressedThisFrame())
        {
            playerStateMachine.SwitchState(StateType.JumpState);
        }
        if(InputManager.Input.Input.Fire.WasPressedThisFrame())
        {
            fsmEntity.Animator.SetTrigger("Shoot");
        }
    }

    public override void Exit()
    {
        base.Exit();

        fsmEntity.RB.velocity = Vector2.zero;
    }
}
