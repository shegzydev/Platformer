using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : Character, IDamageable
{
    //AggresiveVariables
    public float FarTriggerDistance;
    public float attackDistance;

    //Patrol Variables
    public float PatrolDistance;

    public Vector3[] PatrolPoints;
    public LayerMask playerLayer;
    public Transform player;

    public Vector3 Target;

    [Space]
    [Header("Attacking")]
    public float TimeBetweenAttacks;
    public Transform AttackPoint;
    public bool Slashing;

    [Space]
    [Header("Health")]
    public Slider HealthBar;
    public int Health = 100;


    bool dead = false;
    public UnityEvent OnDead;

    public override void OnAwake()
    {
        base.OnAwake();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        PatrolPoints = new Vector3[2];
        PatrolPoints[0] = transform.position + Vector3.right * PatrolDistance / 2;
        PatrolPoints[1] = transform.position - Vector3.right * PatrolDistance / 2;

        StateMachine = new StateMachine(new PatrolState(this));
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Vector3 p = Target - transform.position;
        float x = Mathf.Clamp(p.x*5, -1, 1);
        hInput = x;

        StateMachine.Update();
    }

    public void TakeDamage()
    {
        if (dead) return;

        Health -= 10;
        HealthBar.value = Health;

        if (Health > 0)
        {
            Animator.SetTrigger("TakeHit");
        }
        else
        {
            OnDead.Invoke();
            Animator.SetTrigger("Death");
            dead = true;
        }
    }

    public void Attack()
    {
        if (Physics2D.OverlapCircle(AttackPoint.position, 0.26f, playerLayer))
        {
            player.GetComponent<IDamageable>()?.TakeDamage();
        }
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
