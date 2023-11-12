using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Player : Character, IDamageable
{
    public int GroundedState;
    public int JumpingState;

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
        base.OnAwake();

        startPos = transform.position;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        hudManager = FindObjectOfType<HudManager>();

        StateMachine = new StateMachine(new GroundState(this));
    }

    public override void OnUpdate()
    {
        hInput = InputManager.Input.Input.Horizontal.ReadValue<float>();

        JumpBuffer -= Time.deltaTime;
        if (InputManager.Input.Input.Jump.WasPressedThisFrame())
        {
            JumpBuffer = 0.25f;
        }

        if (IsGrounded) { CoyoteTime = 0.25f; }
        else { CoyoteTime -= Time.deltaTime; }

        if (transform.position.y <= -2.5f)
        {
            transform.position = startPos;
        }

        StateMachine.Update();
    }

    public void Shoot()
    {
        Instantiate(arrow, point.position, point.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, 0.05f);

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 0.1f + Vector3.down * (GroundCheck + 0.08f));
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
            //PlayerStateMachine.SwitchState(StateType.NullState);
            Invoke("Reload", 1.5f);
        }

        Debug.Log("Damage Taken");
    }

    void Reload()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void SetCol(bool flag)
    {
        if (flag) gameObject.layer = LayerMask.NameToLayer("Player");
        else gameObject.layer = LayerMask.NameToLayer("PlayerNoCol");
    }
}
