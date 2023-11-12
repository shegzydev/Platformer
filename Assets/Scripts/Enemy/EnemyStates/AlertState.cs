using System.Collections;
using UnityEngine;

public class AlertState : State
{
    Enemy enemy;
    StateMachine stateMachine1;

    public AlertState(Character character) : base(character)
    {
        stateMachine1 = new StateMachine(new IdleState(character));
        enemy = (Enemy)character;
    }

    public override State Update()
    {
        enemy.Target = enemy.transform.position + Vector3.right * 0.7f;

        stateMachine1.Update();
        return base.Update();
    }
}