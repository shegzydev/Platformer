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

        player.Animator.SetTrigger("ToGround");
    }

    public override void Update()
    {
        base.Update();

        hInput = Input.GetAxis("Horizontal");
        facing = (hInput != 0) ? (hInput > 0 ? 0 : 180) : facing;

        player.Animator.SetFloat("Move", Mathf.Abs(hInput));
        player.transform.rotation = Quaternion.Euler(0, facing, 0);

        Vector2 vel = player.RB.velocity;
        vel.x = hInput * player.MoveSpeed;
        player.RB.velocity = vel;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerStateMachine.SwitchState(StateType.JumpState);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            player.Animator.SetTrigger("Shoot");
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.RB.velocity = Vector2.zero;
    }
}
