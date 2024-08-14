using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

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

    [Header("Jump")]
    public GameObject JumpIndicator;
    public Image JumpGauge;

    public float RollBuffer;

    bool onWall;
    State stateCache;

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
        hInput = InputManager.LHInput;

        if (InputManager.AttackPressed)
        {
            State s = StateMachine.GetCurrentState;
            if (s.GetType() != typeof(AttackState))
            {
                stateCache = s;
                StateMachine.ForceState(new AttackState(this));
                Invoke("ToMainState", 0.2f);
            }
        }

        RollBuffer -= Time.deltaTime;
        if (InputManager.RollPressed)
        {
            RollBuffer = 0.25f;
        }

        JumpBuffer -= Time.deltaTime;
        if (InputManager.JumpPressed)
        {
            JumpBuffer = 0.25f;
        }

        JumpIndicator.SetActive(JumpBuffer > 0);
        JumpGauge.fillAmount = JumpBuffer / 0.25f;

        if (IsGrounded) { CoyoteTime = 0.25f; }
        else { CoyoteTime -= Time.deltaTime; }

        if (transform.position.y <= -2.5f)
        {
            transform.position = startPos;
        }

        if (!dead) StateMachine.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            onWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            onWall = false;
        }
    }

    public bool OnWall
    {
        get
        {
            return onWall;
        }
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
        hudManager.UpdateHud(hp);

        if (hp <= 0)
        {
            OnDead.Invoke();
            Animator.Play("Death");
            dead = true;
            Invoke("Reload", 1.5f);
        }

        CamShaker.Shake(2);

        Debug.Log("Damage Taken");
    }

    void Reload()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void SetCollLayer(bool flag)
    {
        if (flag) gameObject.layer = LayerMask.NameToLayer("Player");
        else gameObject.layer = LayerMask.NameToLayer("PlayerNoCol");
    }

    void ToMainState()
    {
        StateMachine.ForceState(stateCache);
    }
}
