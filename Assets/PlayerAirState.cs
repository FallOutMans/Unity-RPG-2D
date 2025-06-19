using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.WallSlideState);
            return;
        }

        if (xInput != 0)
        {
            player.SetVelocity(new Vector2(xInput * player.MoveSpeed * 0.8f, rb.velocity.y));
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
