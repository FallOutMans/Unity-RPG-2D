using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public bool isBusy { get; private set; }

    [Header("Attack details")] public Vector2[] attackMovement;
    
    [Header("Move Info")]
    [SerializeField] public float MoveSpeed = 12f;
    [SerializeField] public float JumpPower = 5f;
    
    [Header("Dash Info")]
    [SerializeField] public float DashSpeed = 40f;
    [SerializeField] public float DashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 6f;
    private float dashUsageTimer;
    public float DashDir { get; private set; }
    
    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    
    private bool FacingRight = true;
    public int FacingDirection { get; private set; } = 1;
    
    #region Components
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine StateMachine { private set; get; }
    public PlayerIdleState IdleState { private set; get; }
    public PlayerMoveState MoveState { private set; get; }
    public PlayerJumpState JumpState { private set; get; }
    public PlayerAirState AirState { private set; get; }
    public PlayerWallSlideState WallSlideState { private set; get; }
    
    public PlayerWallJumpState WallJumpState { private set; get; }
    public PlayerDashState DashState { private set; get; }
    
    public PlayerPrimaryAttackState PrimaryAttackState { private set; get; }
    #endregion
    
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
    }

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();

        CheckForDashInput();
    }

    public IEnumerator BusyFor(float waitTime)
    {
        isBusy = true;
        
        yield return new WaitForSeconds(waitTime);
        
        isBusy = false;
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
        {
            return;
        }
        
        dashUsageTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCooldown;
            DashDir = Input.GetAxisRaw("Horizontal");

            if (DashDir == 0)
            {
                DashDir = FacingDirection;
            }
            
            StateMachine.ChangeState(DashState);
        }
    }

    #region Velocity

    public void SetVelocity(Vector2 velocity)
    {
        Rigidbody.velocity = velocity;
        FlipController(velocity.x);
    }
    
    public void ZeroVelocity() => Rigidbody.velocity = Vector2.zero;

    #endregion

    #region Collision

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, wallCheckDistance,whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * FacingDirection, wallCheck.position.y));
    }

    #endregion

    #region Flip

    private void FlipController(float x)
    {
        if (x > 0 && !FacingRight)
        {
            Flip();
        }else if (x < 0 && FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        FacingDirection = -FacingDirection;
        transform.Rotate(0f, 180f, 0f);
    }

    #endregion
}
