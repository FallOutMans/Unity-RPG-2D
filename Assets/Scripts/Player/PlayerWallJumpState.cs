using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StateTimer = 1f;
        player.SetVelocity(new Vector2(3 * -player.FacingDirection, player.JumpPower));
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer < 0)
        {
            stateMachine.ChangeState(player.AirState);
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
