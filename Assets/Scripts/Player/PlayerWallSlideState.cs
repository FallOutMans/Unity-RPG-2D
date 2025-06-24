using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }

        rb.velocity = yInput < 0 ? new Vector2(0f, rb.velocity.y) : new Vector2(0f, rb.velocity.y * 0.9f);

        if (xInput != 0 && (xInput * player.FacingDirection) < 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
