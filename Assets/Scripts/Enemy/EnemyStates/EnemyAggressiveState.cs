using System.Collections;
using UnityEngine;

public class EnemyAggressiveState : State
{
    enum AnimState
    {
        Idle, Run, Attack
    }

    Enemy enemy;

    float timeSinceLeftAggressiveZone;
    public EnemyAggressiveState(FSMEntity enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        timeSinceLeftAggressiveZone = 0;
    }

    public override void Update()
    {
        base.Update();
        enemy = (Enemy)player;

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.player.position + (enemy.transform.position - enemy.player.position).normalized * enemy.attackDistance , enemy.MoveSpeed * Time.deltaTime);

        if(!Physics2D.OverlapBox((enemy.PatrolPoints[0] + enemy.PatrolPoints[1]) / 2, new Vector2(enemy.PatrolDistance, 0.5f), 0, enemy.playerLayer))
        {
            timeSinceLeftAggressiveZone += Time.deltaTime;
        }
        else
        {
            timeSinceLeftAggressiveZone = 0;
        }

        if(timeSinceLeftAggressiveZone > 2f)
        {
            enemy.EnemyStateMachine.SwitchState(StateType.PatrolState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}