using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    Enemy enemy;
    StateMachine stateMachine1;
    bool switching;
    int currentPoint;
    public PatrolState(Character character) : base(character)
    {
        stateMachine1 = new StateMachine(new IdleState(character));
        enemy = (Enemy)character;

        currentPoint = Random.Range(0, enemy.PatrolPoints.Length);
    }

    public override State Update()
    {
        enemy.Target = enemy.PatrolPoints[currentPoint];
        Vector3 p = enemy.Target - enemy.transform.position;

        if (Mathf.Abs(p.x) < 0.11f)
        {
            enemy.StartCoroutine(SwitchTargets());
        }

        //if (character.JumpBuffer > 0)
        //{
        //    return new JumpingState(character);
        //}
        //if (!character.IsGrounded)
        //{
        //    return new FallingState(character);
        //}

        stateMachine1.Update();

        return base.Update();
    }

    IEnumerator SwitchTargets()
    {
        if (switching) yield break;
        switching = true;
        yield return new WaitForSeconds(2f);
        currentPoint = 1 - currentPoint;
        switching = false;
    }
}
