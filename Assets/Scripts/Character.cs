using System.Collections;
using UnityEngine;

public enum States
{
    Idle, Jump, Fall, Move
}

public class Character : MonoBehaviour
{
    public StateMachine StateMachine;

    public float MoveSpeed = 1.5f;

    #region jumpvariables
    public float JumpHeight = 0.5f;

    public float GroundCheck = 0.12f;

    public float JumpBuffer = 0.25f;
    public float CoyoteTime = 0.25f;

    #endregion

    public LayerMask GroundLayer;

    public Rigidbody2D RB;
    public Animator Animator;

    public float facing;
    public float hInput;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, facing, 0);
        
        OnUpdate();
    }

    public bool IsGrounded
    {
        get
        {
            return Physics2D.CircleCast(transform.position, 0.08f, Vector2.down, GroundCheck, GroundLayer);
        }
    }

    public virtual void OnAwake() { }
    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
}
