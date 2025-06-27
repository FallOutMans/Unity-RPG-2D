using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2;
    
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (comboCounter > 2 || Time.time - lastTimeAttacked >= comboWindow)
        {
            comboCounter = 0;
        }
        player.Animator.SetInteger("ComboCounter", comboCounter);

        #region Choose attack direction

        float attackDir = player.FacingDirection;

        if (xInput != 0)
        {
            attackDir = xInput;
        }

        #endregion
        
        player.SetVelocity(new Vector2(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y));

        StateTimer = .1f;
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer < 0)
        {
           player.SetZeroVelocity();
        }

        if (TriggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f);
        
        comboCounter++;
        lastTimeAttacked = Time.time;
    }
}
