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
            RaycastHit2D RCast = Physics2D.CircleCast(transform.position, 0.03f, Vector2.down, GroundCheck, GroundLayer);

            if (!RCast.transform) return false;

            if (RCast.normal.magnitude < 0.5f) return false;
            
            if (Vector2.Dot(Vector2.up, RCast.normal) < 0.97f) return false;
            
            Debug.DrawRay(RCast.point, RCast.normal);

            return true;
        }
    }

    public void ResetJumpBuffer()
    {
        JumpBuffer = 0;
    }


    public virtual void OnAwake() { }
    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
}
