using System.Collections;
using UnityEngine;

public class BossAggressiveState : State
{
    Boss boss;
    Transform player;
    float xPos;

    public BossAggressiveState(Character character) : base(character)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = (Boss)character;
        xPos = boss.transform.position.x;
    }

    public override State Update()
    {
        base.Update();

        boss.transform.rotation = Quaternion.Euler(0, (player.position.x - boss.transform.position.x) > 0 ? 0 : 180, 0);

        float playerDistance = Mathf.Abs(boss.transform.position.x - player.position.x) - boss.attackDistance;


        if (playerDistance > 0)
        {
            float pointToBe = (player.position.x) + Mathf.Sign(boss.transform.position.x - player.position.x) * boss.attackDistance;
            
            if (!boss.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                xPos = Mathf.MoveTowards(xPos, pointToBe, boss.MoveSpeed * 0.5f * Time.deltaTime);
            }
        }

        boss.transform.position = new Vector2(xPos, boss.transform.position.y);

        boss.Animator.SetFloat("Move", playerDistance);

        if (playerDistance < 0.001f)
        {
            boss.Animator.SetTrigger("Attack");
        }
        return this;
    }

    public override void Exit()
    {
        base.Exit();

    }
}