using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Boss : Character, IDamageable
{
    StateMachine BossStateMachine { get; set; }
    public float attackDistance = 0.4f;

    [Header("UI")]
    public Slider HealthBar;
    public int Health = 2000;

    public bool Slashing;

    bool dead;
    bool activated;
    public UnityEvent OnDead;

    [Header("")]
    [SerializeField] Transform AttackPoint;
    [SerializeField] LayerMask playerLayer;
    Transform player;

    public override void OnAwake()
    {
        base.OnAwake();

        BossStateMachine = new StateMachine(new IdleState(this));
    }

    public override void OnStart()
    {
        base.OnStart();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        BossStateMachine?.Update();
    }

    public void Die()
    {
        HealthBar.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Attack()
    {
        if (Physics2D.OverlapCircle(AttackPoint.position, 0.4f, playerLayer))
        {
            player.GetComponent<IDamageable>()?.TakeDamage();
        }
    }

    public void Activate()
    {
        activated = true;
        Debug.Log("InGroundState");
        HealthBar.gameObject.SetActive(true);
    }

    public void TakeDamage()
    {
        if (!activated) return;
        if (dead) return;

        Health -= 10;

        if (HealthBar) HealthBar.value = Health;

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
}