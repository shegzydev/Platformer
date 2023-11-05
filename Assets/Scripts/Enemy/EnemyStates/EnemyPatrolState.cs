using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class EnemyPatrolState : State
{
    enum AnimState
    {
        Idle,Run
    }

    Enemy enemy;

    AnimState currentState;

    int currentPatrolPoint;

    Vector3 currentPoint;

    bool nexttarget = false;
    public EnemyPatrolState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        currentState = AnimState.Run;
    }

    public override void Update()
    {
        base.Update();
        enemy = (Enemy)player;

        currentPoint = enemy.PatrolPoints[currentPatrolPoint];

        enemy.transform.position = Vector3.MoveTowards(player.transform.position, currentPoint, player.MoveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(enemy.transform.position, currentPoint) < 0.05f)
        {
            enemy.StartCoroutine(approachNextTarget());
        }

        enemy.Animator.Play(System.Enum.GetName(typeof(AnimState), currentState));
        if(Physics2D.OverlapBox(enemy.transform.position + enemy.transform.right * enemy.FarTriggerDistance * 0.5f, new Vector2(enemy.FarTriggerDistance, 0.17f), 0, enemy.playerLayer))
        {
            enemy.EnemyStateMachine.SwitchState(StateType.AggressiveState);
        }
    }
    IEnumerator approachNextTarget()
    {
        if(nexttarget) { yield break; }
        nexttarget = true;

        currentState = AnimState.Idle;

        yield return new WaitForSeconds(2f);

        currentPatrolPoint = 1 - currentPatrolPoint;
        enemy.transform.rotation = Quaternion.Euler(0, (enemy.PatrolPoints[currentPatrolPoint].x - enemy.transform.position.x) > 0 ? 0 : 180, 0);
        
        currentState = AnimState.Run;

        nexttarget = false;
    }

    public override void Exit()
    {
        base.Exit();


    }
}
