using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Entity
{
    public bool isBusy { get; private set; }

    [Header("Attack details")] 
    public Vector2[] attackMovement;
    
    [Header("Move Info")]
    [SerializeField] public float MoveSpeed = 12f;
    [SerializeField] public float JumpPower = 5f;
    
    [Header("Dash Info")]
    [SerializeField] public float DashSpeed = 40f;
    [SerializeField] public float DashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 6f;
    private float dashUsageTimer;
    public float DashDir { get; private set; }

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
    
    protected override void Awake()
    {
        base.Awake();
        
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

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        
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
}
