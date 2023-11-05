using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : FSMEntity, IDamageable
{
    StateMachine BossStateMachine { get; set; }
    public float attackDistance = 0.4f;

    [Header("UI")]
    public Slider HealthBar;
    public int Health = 2000;
    bool dead;

    bool activated;

    public override void OnAwake()
    {
        base.OnAwake();

        BossStateMachine = new StateMachine();
    }

    public override void OnStart()
    {
        base.OnStart();

        BossStateMachine?.Initialize(StateType.NullState, new State(null, null));
        BossStateMachine.AddState(StateType.GroundState, new BossAggressiveState(this, BossStateMachine));
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

    public void Activate()
    {
        activated = true;
        Debug.Log("InGroundState");
        BossStateMachine.SwitchState(StateType.GroundState);
        HealthBar.gameObject.SetActive(true);
    }

    public void TakeDamage()
    {
        if (!activated) return;
        if (dead) return;

        Health -= 20;

        if (HealthBar) HealthBar.value = Health;

        if (Health > 0)
        {
            Animator.SetTrigger("TakeHit");
        }
        else
        {
            Animator.SetTrigger("Death");
            dead = true;
            BossStateMachine.SwitchState(StateType.NullState);
        }
    }
}