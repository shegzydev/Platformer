using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : FSMEntity, IDamageable
{
    public StateMachine EnemyStateMachine;
    public EnemyPatrolState PatrolState;
    public EnemyAggressiveState AggressiveState;

    //AggresiveVariables
    public float FarTriggerDistance;
    public float NearTriggerDistance;
    public float attackDistance;

    //Patrol Variables
    public float PatrolDistance;

    public Vector3[] PatrolPoints;
    public LayerMask playerLayer;
    public Transform player;

    [Header("Health")]
    public Slider HealthBar;
    public int Health = 100;
    
    bool dead = false;

    public override void OnAwake()
    {
        base.OnAwake();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        PatrolPoints = new Vector3[2];
        PatrolPoints[0] = transform.position + Vector3.right * PatrolDistance / 2;
        PatrolPoints[1] = transform.position - Vector3.right * PatrolDistance / 2;

        EnemyStateMachine = new StateMachine();
    }

    public override void OnStart()
    {
        base.OnStart();

        PatrolState = new EnemyPatrolState(this, EnemyStateMachine);
        AggressiveState = new EnemyAggressiveState(this, EnemyStateMachine);

        EnemyStateMachine.Initialize(StateType.PatrolState, PatrolState);
        EnemyStateMachine.AddState(StateType.AggressiveState, AggressiveState);
        EnemyStateMachine.AddState(StateType.NullState, null);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        EnemyStateMachine?.Update();
    }

    public void TakeDamage()
    {
        if (dead) return;

        Health -= 20;
        HealthBar.value = Health;

        if (Health > 0)
        {
            Animator.SetTrigger("TakeHit");
        }
        else
        {
            Animator.SetTrigger("Death");
            dead = true;
            EnemyStateMachine.SwitchState(StateType.NullState);
        }
    }

    public void Attack()
    {

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + Vector3.right * PatrolDistance / 2,
            transform.position - Vector3.right * PatrolDistance / 2);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.right * FarTriggerDistance * 0.5f, new Vector2(FarTriggerDistance, 0.17f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((transform.position + Vector3.right * PatrolDistance / 2 +
            (transform.position - Vector3.right * PatrolDistance / 2)) / 2, new Vector2(PatrolDistance, 0.5f));
    }
}
