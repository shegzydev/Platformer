using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Player : FSMEntity, IDamageable
{
    StateMachine PlayerStateMachine;

    #region groundedvariable
    public PlayerGroundState groundState;
    #endregion

    public FallState fallState;

    #region jumpvariables
    public PlayerJumpState jumpState;
    #endregion

    public float hInput;

    [Header("Projectiles")]
    [SerializeField] GameObject arrow;
    [SerializeField] Transform point;

    HudManager hudManager;
    [SerializeField] int hp = 100;
    bool dead = false;
    public UnityEvent OnDead;

    Vector2 startPos;

    public override void OnAwake()
    {
        startPos = transform.position;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        PlayerStateMachine = new StateMachine();

        groundState = new PlayerGroundState(this, PlayerStateMachine);
        jumpState = new PlayerJumpState(this, PlayerStateMachine);

        PlayerStateMachine.Initialize(StateType.GroundState, groundState);
        PlayerStateMachine.AddState(StateType.JumpState, jumpState);

        hudManager = FindObjectOfType<HudManager>();
    }

    public override void OnUpdate()
    {
        //hInput = Input.GetAxis("Horizontal");

        if(transform.position.y <= -2.5f)
        {
            transform.position = startPos;
        }

        PlayerStateMachine.Update();
    }

    public void Shoot()
    {
        Instantiate(arrow, point.position, point.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, 0.05f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.182f);
    }

    public void TakeDamage()
    {
        if (dead) return;

        hp -= 10;
        hudManager.UpdateHudEntry(HUDData.HP, hp);

        if (hp <= 0)
        {
            OnDead.Invoke();
            Animator.SetTrigger("Death");
            dead = true;
            PlayerStateMachine.SwitchState(StateType.NullState);
            Invoke("Reload", 1.5f);
        }

        Debug.Log("Damage Taken");
    }

    void Reload()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
