using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        // TODO,判断当前状态是否是本身，不是需要返回，要不然
        
        player.SetVelocity(new Vector2(xInput * player.MoveSpeed, rb.velocity.y));
        
        if (xInput == 0 || player.IsWallDetected())
        {
            stateMachine.ChangeState(player.IdleState);   
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
