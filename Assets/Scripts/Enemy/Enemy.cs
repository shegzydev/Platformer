using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FSMEntity, IDamageable
{
    public StateMachine EnemyStateMachine;
    public EnemyPatrolState PatrolState;
    public EnemyAggressiveState AggressiveState;

    //Patrol Variables
    public float FarTriggerDistance;
    public float NearTriggerDistance;

    public float attackDistance;

    public float PatrolDistance;

    //

    public Vector3[] PatrolPoints;

    public LayerMask playerLayer;

    public Transform player;

    public override void OnStart()
    {
        base.OnStart();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        PatrolPoints = new Vector3[2];
        PatrolPoints[0] = transform.position + Vector3.right * PatrolDistance / 2;
        PatrolPoints[1] = transform.position - Vector3.right * PatrolDistance / 2;

        EnemyStateMachine = new StateMachine();

        PatrolState = new EnemyPatrolState(this, EnemyStateMachine);
        AggressiveState = new EnemyAggressiveState(this, EnemyStateMachine);

        EnemyStateMachine.Initialize(StateType.PatrolState, PatrolState);
        EnemyStateMachine.AddState(StateType.AggressiveState, AggressiveState);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        EnemyStateMachine?.Update();
    }

    public void TakeDamage()
    {

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + Vector3.right * PatrolDistance / 2,
            transform.position - Vector3.right * PatrolDistance / 2);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.right * FarTriggerDistance * 0.5f, new Vector2(FarTriggerDistance, 0.17f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((PatrolPoints[0] + PatrolPoints[1]) / 2, new Vector2(PatrolDistance, 0.5f));
    }
}
