using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AggressiveState : State
{
    StateMachine stateMachine1;
    Enemy enemy;
    float timeSinceShaken;
    float attacking = 0;

    MoveState moveState;
    public AggressiveState(Character character) : base(character)
    {
        enemy = (Enemy)character;
        moveState = new MoveState(enemy);
        stateMachine1 = new StateMachine(moveState);
    }
    public override State Update()
    {
        enemy.Target = enemy.player.position;

        if (Mathf.Abs(enemy.player.transform.position.x - enemy.transform.position.x) < enemy.attackDistance)
        {
            if (enemy.AttackTimer >= enemy.TimeBetweenAttacks)
            {
                attacking = enemy.TimeBetweenAttacks;
                enemy.RB.velocity = Vector3.zero;
                stateMachine1.ForceState(new AttackState(enemy));
                enemy.AttackTimer = 0;
            }
        }
        else
        {
            if (attacking <= 0)
            {
                stateMachine1.ForceState(new MoveState(enemy));
            }
        }
        attacking-=Time.deltaTime;
        attacking = Mathf.Clamp(attacking, -.1f, 1);

        if (!enemy.InView)
        {
            timeSinceShaken += Time.deltaTime;
        }
        else
        {
            timeSinceShaken = 0;
        }
        if(timeSinceShaken > 2)
        {
            return new PatrolState(enemy);
        }

        stateMachine1.Update();
        return base.Update();
    }
}