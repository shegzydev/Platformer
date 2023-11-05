using System.Collections;
using UnityEngine;

public class EnemyAggressiveState : State
{
    enum AnimState
    {
        Idle, Run, Attack
    }

    Enemy enemy;
    float xPos;

    float timeSinceLeftAggressiveZone;
    public EnemyAggressiveState(FSMEntity enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        enemy = (Enemy)fsmEntity;

        xPos = enemy.transform.position.x;
        timeSinceLeftAggressiveZone = 0;
        enemy.Animator.Play("Move");
    }

    public override void Update()
    {
        base.Update();

        if (!Physics2D.OverlapBox((enemy.PatrolPoints[0] + enemy.PatrolPoints[1]) / 2, new Vector2(enemy.PatrolDistance, 0.5f), 0, enemy.playerLayer))
        {
            timeSinceLeftAggressiveZone += Time.deltaTime;
            enemy.Animator.SetFloat("Move", 0);
        }
        else
        {
            enemy.transform.rotation = Quaternion.Euler(0, (enemy.player.position.x - enemy.transform.position.x) > 0 ? 0 : 180, 0);

            float playerDistance = Mathf.Abs(enemy.transform.position.x - enemy.player.position.x) - enemy.attackDistance;

            if (playerDistance > 0)
            {
                float pointToBe = (enemy.player.position.x) + Mathf.Sign(enemy.transform.position.x - enemy.player.position.x) * enemy.attackDistance;
                xPos = Mathf.MoveTowards(xPos, pointToBe, enemy.MoveSpeed * 0.5f * Time.deltaTime);
            }

            enemy.transform.position = new Vector2(xPos, enemy.transform.position.y);

            enemy.Animator.SetFloat("Move", playerDistance);

            if (playerDistance < 0.001f)
            {
                enemy.Animator.SetTrigger("Attack");
            }

            timeSinceLeftAggressiveZone = 0;
        }

        if (timeSinceLeftAggressiveZone > 2f)
        {
            enemy.EnemyStateMachine.SwitchState(StateType.PatrolState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}