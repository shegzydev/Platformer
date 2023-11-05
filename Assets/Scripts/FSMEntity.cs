using System.Collections;
using UnityEngine;

public class FSMEntity : MonoBehaviour
{
    public float JumpHeight = 0.5f;
    public float MoveSpeed = 1.5f;
    public Rigidbody2D RB;
    public Animator Animator;

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
        OnUpdate();
    }

    public bool IsGrounded
    {
        get
        {
            return Physics2D.CircleCast(transform.position, 0.08f, Vector2.down, 0.12f);
        }
    }

    public virtual void OnAwake() { }
    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
}
