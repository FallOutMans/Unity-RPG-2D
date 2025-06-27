using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 0.2f;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = 0.2f;
    [SerializeField] protected LayerMask whatIsGround;
    
    #region Components
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    #endregion
    
    protected bool FacingRight = true;
    public int FacingDirection { get; private set; } = 1;
    
    protected virtual void Awake(){}

    protected virtual void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Update(){}

    public virtual void Damage()
    {
        Debug.Log("Damage");
    }
    
    #region Velocity

    public void SetVelocity(Vector2 velocity)
    {
        Rigidbody.velocity = velocity;
        FlipController(velocity.x);
    }
    
    public void SetZeroVelocity() => Rigidbody.velocity = Vector2.zero;

    #endregion
    
    #region Collision

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, wallCheckDistance,whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * FacingDirection, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion
    
    #region Flip

    protected virtual void FlipController(float x)
    {
        if (x > 0 && !FacingRight)
        {
            Flip();
        }else if (x < 0 && FacingRight)
        {
            Flip();
        }
    }

    public virtual void Flip()
    {
        FacingRight = !FacingRight;
        FacingDirection = -FacingDirection;
        transform.Rotate(0f, 180f, 0f);
    }

    #endregion
}
